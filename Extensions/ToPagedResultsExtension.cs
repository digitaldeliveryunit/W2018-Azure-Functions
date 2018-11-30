using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Helpers;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.ViewModels;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace com.petronas.myevents.api.Services
{
    public static class ToPagedResultsExtension
    {
        public static async Task<PagedResults<T>> ToPagedResults<T>(this IQueryable<T> source)
        {
            var documentQuery = source.AsDocumentQuery();
            var results = new PagedResults<T>();

            try
            {
                var queryResult = await documentQuery.ExecuteNextAsync<T>();
                if (!queryResult.Any())
                {
                    return results;
                }
                results.ContinuationToken = queryResult.ResponseContinuation?.Crypt();
                results.Results.AddRange(queryResult);
            }
            catch
            {
                //documentQuery.ExecuteNextAsync throws an Exception if there are no results
                return results;
            }

            return results;
        }

        public static PagedResults<EventResponse> ToPagedEventResults(this IEnumerable<Event> source, string continuationToken, string currentUserId)
        {
            var results = new PagedResults<EventResponse>();

            try
            {
                if (!source.Any())
                {
                    return results;
                }
                results.ContinuationToken = continuationToken?.Crypt();
                results.Results.AddRange(source.Select(_event => new EventResponse
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
                    IsBookmark = _event.Bookmarks != null && _event.Bookmarks.Any(x => !x.IsDeleted && x.UserId == currentUserId),
                    UserStatus = _event.Members != null && _event.Members.Any(x => !x.IsDeleted && x.UserId == currentUserId)
                    ? _event.Members.FirstOrDefault(x => !x.IsDeleted && x.UserId == currentUserId)?.EventMemberStatus
                    : UserStatus.NEW.ToString()
                }).ToList());
            }
            catch
            {
                //documentQuery.ExecuteNextAsync throws an Exception if there are no results
                return results;
            }

            return results;
        }
    }
}
