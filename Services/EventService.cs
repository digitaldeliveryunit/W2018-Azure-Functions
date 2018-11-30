﻿using System;
using System.Linq;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using com.petronas.myevents.api.ViewModels;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace com.petronas.myevents.api.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<bool> Bookmark(string eventId, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var _event = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).ToList()
                .FirstOrDefault();
            if (!_event.Bookmarks.Any(x => !x.IsDeleted && x.UserId == userId))
            {
                var bookmark = new Bookmark
                {
                    EventId = eventId,
                    UserId = userId,
                    Id = Guid.NewGuid().ToString()
                };
                _event.Bookmarks.Add(bookmark);
                await _eventRepository.Update(_event);
            }

            return true;
        }

        public async Task<bool> UnBookmark(string eventId, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var _event = _eventRepository.GetAll(x => !x.IsDeleted && x.Id == eventId, feedOptions).ToList()
                .FirstOrDefault();
            for (var i = 0; i < _event.Bookmarks.Count; i++)
                _event.Bookmarks.Remove(_event.Bookmarks.FirstOrDefault(x => x.Id == _event.Bookmarks[i].Id));
            await _eventRepository.Update(_event);
            return true;
        }

        public EventResponse GetById(string id, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            var ev = _eventRepository.GetBatch(x => x.Id == id && !x.IsDeleted, feedOptions, out _, null)
                .FirstOrDefault();
            return ev == null ? null : GetEventResponse(ev, userId);
        }

        public PagedResults<EventResponse> GetFeaturedEvents(string continuationKey, int take, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = take,
                EnableScanInQuery = true
            };
            if (!string.IsNullOrEmpty(continuationKey)) feedOptions.RequestContinuation = continuationKey;
            return _eventRepository.GetBatch(x => !x.IsDeleted && x.IsFeatured, feedOptions, out var continuation,
                x => x.OrderBy(y => y.EventDateFrom)).ToPagedEventResults(continuation, userId);
        }

        public PagedResults<EventResponse> GetPastEvents(string continuationKey, int take, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = take,
                EnableScanInQuery = true
            };
            if (!string.IsNullOrEmpty(continuationKey)) feedOptions.RequestContinuation = continuationKey;
            var sqlQuery = $@"
                SELECT *
                FROM Events c
                WHERE c.IsDeleted = @isDeleted
                    AND c.EventDateTo < '{DateTime.UtcNow.ToString("yyyy-MM-ddThh-mm-ss")}'
                    AND (
                        ARRAY_CONTAINS(c.Members, {{ 'UserId': '{userId}' }}, true)
                        OR ARRAY_CONTAINS(c.Bookmarks, {{ 'UserId': '{userId}' }}, true))
                    ORDER BY c.EventDateFrom ASC";

            var query = new SqlQuerySpec
            {
                QueryText = sqlQuery,
                Parameters = new SqlParameterCollection
                {
                    new SqlParameter("@isDeleted", false)
                }
            };
            return _eventRepository.GetBatch(query, feedOptions, out var continuation).ToPagedEventResults(continuation, userId);
        }

        public PagedResults<EventResponse> GetUpcomingAllEvents(string continuationKey, int take, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = take,
                EnableScanInQuery = true
            };
            if (!string.IsNullOrEmpty(continuationKey)) feedOptions.RequestContinuation = continuationKey;
            return _eventRepository
                .GetBatch(x => !x.IsDeleted && x.EventDateTo > DateTime.UtcNow, feedOptions,
                    out var continuation, x => x.OrderBy(y => y.EventDateFrom))
                .ToPagedEventResults(continuation, userId);
        }

        public PagedResults<EventResponse> GetUpcomingEvents(string continuationKey, int take, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = take,
                EnableScanInQuery = true
            };
            if (!string.IsNullOrEmpty(continuationKey)) feedOptions.RequestContinuation = continuationKey;
            var sqlQuery = $@"
                SELECT *
                FROM Events c
                WHERE c.IsDeleted = @isDeleted
                    AND c.EventDateTo > '{DateTime.UtcNow.ToString("yyyy-MM-ddThh-mm-ss")}'
                    AND (
                        ARRAY_CONTAINS(c.Members, {{ 'UserId': '{userId}' }}, true)
                        OR ARRAY_CONTAINS(c.Bookmarks, {{ 'UserId': '{userId}' }}, true))
                    ORDER BY c.EventDateFrom ASC";

            var query = new SqlQuerySpec
            {
                QueryText = sqlQuery,
                Parameters = new SqlParameterCollection
                {
                    new SqlParameter("@now", DateTime.UtcNow),
                    new SqlParameter("@isDeleted", false),
                    new SqlParameter("@userId", userId)
                }
            };
            return _eventRepository.GetBatch(query, feedOptions, out var continuation).ToPagedEventResults(continuation, userId);
        }

        public PagedResults<EventResponse> Search(string searchKey, string continuationKey, int take, string userId)
        {
            var feedOptions = new FeedOptions
            {
                MaxItemCount = take,
                EnableScanInQuery = true
            };
           if (!string.IsNullOrEmpty(continuationKey)) feedOptions.RequestContinuation = continuationKey;
            var sqlQuery = $@"
                SELECT *
                FROM Events c
                WHERE c.IsDeleted = @isDeleted
                    AND CONTAINS(LOWER(c.EventName), @searchKey) 
                    AND (
                        ARRAY_CONTAINS(c.Members, {{ 'UserId': '{userId}' }}, true)
                        OR ARRAY_CONTAINS(c.Bookmarks, {{ 'UserId': '{userId}' }}, true))
                    ORDER BY c.EventDateFrom ASC";

            var query = new SqlQuerySpec
            {
                QueryText = sqlQuery,
                Parameters = new SqlParameterCollection
                {
                    new SqlParameter("@isDeleted", false),
                    new SqlParameter("@searchKey", searchKey.ToLower())
                }
            };
            return _eventRepository.GetBatch(query, feedOptions, out var continuation).ToPagedEventResults(continuation, userId);
        }

        private EventResponse GetEventResponse(Event _event, string userId)
        {
            return new EventResponse
            {
                EventId = _event.Id,
                ImageUrl = _event.EventImageUrl,
                EventDescription = _event.EventDescription,
                EventStatus = _event.EventStatus,
                EventName = _event.EventName,
                EventLocation = _event.Location,
                EventType = _event.EventType,
                DateTo = _event.EventDateTo,
                DateFrom = _event.EventDateFrom,
                Venue = _event.Venue.VenueName,
                IsFeatured = _event.IsFeatured,
                SurveyUrl = _event.SurveyUrl,
                SurveyResultUrl = _event.SurveyResultUrl,
                IsBookmark = _event.Bookmarks != null && _event.Bookmarks.Any(x => !x.IsDeleted && x.UserId == userId),
                UserStatus = _event.Members != null && _event.Members.Any(x => !x.IsDeleted && x.UserId == userId)
                    ? _event.Members.FirstOrDefault(x => !x.IsDeleted && x.UserId == userId)?.EventMemberStatus
                    : UserStatus.NEW.ToString()
            };
        }
    }
}