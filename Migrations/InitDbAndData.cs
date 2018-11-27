using System;
using System.Collections.Generic;
using System.Linq;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Services;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using com.petronas.myevents.api.Configurations;
using System.Net;
namespace com.petronas.myevents.api.Migrations
{
    public class InitDbAndData
    {
        private readonly CosmosDBOptions _options;
        private readonly DocumentClient _client;

        public InitDbAndData()
        {
            _options = new CosmosDBOptions()
            {
                EndpointUri = Environment.GetEnvironmentVariable(AppSettings.DbEndpoint),
                DatabaseId = Environment.GetEnvironmentVariable(AppSettings.DbName),
                PrivateKey = Environment.GetEnvironmentVariable(AppSettings.DbAuthKey)
            };

            _client = new DocumentClient(new Uri(_options.EndpointUri), _options.PrivateKey);
        }
        public void InitDbAndDataSeending()
        {
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = _options.DatabaseId }).Wait();

            var collections = typeof(CollectionNameConstant).GetFields();
            foreach (var c in collections)
            {
                var collection = new DocumentCollection();
                collection.Id = c.GetValue(null).ToString();
                _client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(_options.DatabaseId),
                    collection,
                    new RequestOptions { OfferThroughput = 400 }
                ).Wait();
            }
            #region user
            var Users = new List<Models.User>(){
                new Models.User(){ Discriminator="User", Id = "010E08F1-44F4-4B7F-AA22-5F4611F84CD0", DisplayName = "Taylor Swift", Email = "taylor.swift@petronas.com.my", Username = "taylor.swift", Department = "DD-PM/PET ICT", Company = "PETRONAS ICT Sdn Bhd", ThumbnailPhoto = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/2c29cc2c-f5eb-48ba-96ef-b831325c39c9-1-taylor-1427121683146-thumbnail.jpg" }
                };

            #endregion

            #region location
            var Locations = new List<Location>(){
                new Location(){ Discriminator = "Location", Id = Guid.NewGuid().ToString(), LocationName = "Sepang", Latitude = 2.7568664m, Longitude = 101.6463927m },
                new Location(){ Discriminator = "Location", Id = Guid.NewGuid().ToString(), LocationName = "Suria KLCC, Kuala Lumpur", Latitude = 3.157951m, Longitude = 101.711623m },
                new Location(){ Discriminator = "Location", Id = Guid.NewGuid().ToString(), LocationName = "PETRONAS Twin Towers, Kuala Lumpur", Latitude = 3.157797m, Longitude = 101.711959m },
                new Location(){ Discriminator = "Location", Id = Guid.NewGuid().ToString(), LocationName = "Etiqa Twin Towers, Kuala Lumpur", Latitude = 3.154262m, Longitude = 101.711194m },
                new Location(){ Discriminator = "Location", Id = Guid.NewGuid().ToString(), LocationName = "Traders Hotel, Kuala Lumpur", Latitude = 3.153958m, Longitude = 101.71403m }
            };

            #endregion

            #region venue
            var Venues = new List<Venue>(){
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Entrance", LocationId = Locations.Skip(0).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Pit 1 - 3", LocationId = Locations.Skip(0).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Paddock Club", LocationId = Locations.Skip(0).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Pit 4 - 6", LocationId = Locations.Skip(0).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Sepang Race Track", LocationId = Locations.Skip(0).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Be Amazing-Level 21", LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id },
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Be Proactive-Level 21", LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id },
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Focused Execution-Level 21", LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id },
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Result Matter-Level 21", LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Shared Success-Level 21", LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Collab 2-Level 22", LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Room-Level 22", LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Anggerik Room-Level 22", LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Ballroom 1", LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Entrance", LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Entrance", LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Room-Level 21", LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Room-Level 22", LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Room-Level 23", LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id },
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Room-Level 24", LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Hall", LocationId = Locations.Skip(4).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Conference Hall-Level 3", LocationId = Locations.Skip(4).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Ballroom Hall 3", LocationId = Locations.Skip(4).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Hall 6", LocationId = Locations.Skip(4).Take(1).FirstOrDefault().Id},
                new Venue(){ Discriminator = "Venue", Id = Guid.NewGuid().ToString(), VenueName = "Main Hall", LocationId = Locations.Skip(4).Take(1).FirstOrDefault().Id}
            };
            #endregion

            #region event
            var Events = new List<Event>(){
                //2 past events
                //suria KLCC
                new Event(){
                    Discriminator = "Event",
                     Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "MRCSB OE R2 Power Up Session",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,7,2,18,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,7,1,8,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/836a52bc-943f-4fa7-83f0-b3640ef3df6a-oe-r2-poster-1280x720.png",
                    EventDescription = "<p><u>Details agenda</u></p><p><br></p><p><strong>8.30 - 9.00 a.m. &nbsp;:&nbsp;</strong><strong>Registration</strong></p><p><strong>9:00 -&nbsp;</strong><strong>9:10 a.m. &nbsp;:&nbsp;</strong><strong>Safety Moment, Doa</strong></p><p><strong>9:15 &nbsp;- 9:30 a.m. &nbsp;:&nbsp;</strong><strong>PCB Moment</strong></p><p><strong>9:30 - 9.45 a.m. &nbsp; :&nbsp;</strong><strong>Welcoming Remarks</strong></p><p><strong>9.45 - 10.15 a.m. &nbsp;:&nbsp;</strong><strong>Context Setting</strong></p><p><strong>10.15 - 10.30 a.m.:&nbsp;</strong><strong>Living the PETRONAS Cultural Beliefs</strong></p><p><strong>10.30 - 11.00 a.m.:&nbsp;</strong><strong>Feedback Preparation</strong></p><p><strong>11.00 - 11.15 a.m. :&nbsp;</strong><strong>Tea Break</strong></p><p><strong>11.15 - 11.30 a.m. :&nbsp;</strong><strong>Feedback Exchange Round 1</strong></p><p><strong>11.30 - 11.45 a.m. :&nbsp;</strong><strong>Feedback Exchange Round 2</strong></p><p><strong>11.45 - 12.00 p.m.:&nbsp;</strong><strong>Feedback Exchange Round 3</strong></p><p><strong>12.00 - 12.15 p.m.:&nbsp;</strong><strong>Feedback Exchange Round 4</strong></p><p><strong>12.15 - 12.45 p.m. :&nbsp;</strong><strong>Changing Beliefs Preparation</strong></p><p><strong>12.45 - 2.30 p.m. &nbsp;:&nbsp;</strong><strong>Lunch &amp; Prayers</strong></p><p><strong>&nbsp;2.30 - 2.50 p.m. &nbsp;:&nbsp;</strong><strong>Changing Beliefs Conversation Group 1</strong></p><p><strong>2.50 - 3.10 p.m. &nbsp; :&nbsp;</strong><strong>Changing Beliefs Conversation Group 2</strong></p><p><strong>3.10 – 3.30 p.m. &nbsp;:&nbsp;</strong><strong>Changing Beliefs Conversation Group 3</strong></p><p><strong>3.30 – 3.45 p.m. &nbsp;:&nbsp;</strong><strong>Tea time</strong></p><p><strong>3.45 - 4.05 p.m. &nbsp; :&nbsp;</strong><strong>Changing Beliefs Conversation Group 4</strong></p><p><strong>4.25 - 4.45 p.m. &nbsp; :&nbsp;</strong><strong>Changing Beliefs Conversation Group 5</strong></p><p><strong>4.45 - 5.00 p.m. &nbsp; :&nbsp;</strong><strong>Key Take Away &amp; Expectation</strong></p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(5).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(5).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(1).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },
                //traders
                new Event(){
                    Discriminator = "Event",
                     Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Tender Committee Secretaries Round Table Meeting No 2",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,9,3,17,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,9,2,8,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/397e9a18-475e-47e6-b3fb-471841e4ab8a-picture2-1280x720.png",
                    EventDescription = "<p style=\"text-align: center;\">Dear Esteemed TC Secretaries,</p><p style=\"text-align: center;\"><br></p><p style=\"text-align: center;\">We are pleased to invite you to the 2nd TC Secretaries Round Table Meeting in 2018</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(24).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(4).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(24).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(4).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },


                //2 featured events
                //sepang
                new Event(){
                    Discriminator = "Event",
                    Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Formula 1 PETRONAS Malaysia Grand Prix 2018",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,27,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,26,8,0,0),
                    EventImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/b831b4f2-05a3-47b0-b37f-56c5dde380ae-1-906845.jpg",
                    EventDescription = "<p>The Malaysian Grand Prix is a round of the Formula One World Championship. It has been held at the Sepang International Circuit since 1999, although FIA-sanctioned racing in Malaysia has existed since the 1960s. Since 2011, the race has been officially known as the Malaysia Grand Prix.</p>",
                    IsFeatured = true,
                    VenueId = Venues.Skip(0).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(0).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(0).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(0).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },
                //suria klcc
                new Event(){
                    Discriminator = "Event",
                    Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "BAMup Strategy 2019 & PUReCOP#4 2018",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,31,18,30,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,30,8,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/6747e91a-4e26-479c-8645-4d8337c7840d-capture-1280x720.png",
                    EventDescription = "<p>Bad Actor Upstream Strategy 2019 &amp; PETRONAS Upstream Reliability COP 2018&nbsp;</p>",
                    IsFeatured = true,
                    VenueId = Venues.Skip(5).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(1).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(5).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(1).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },


                //4 upcoming events
                //twins
                new Event(){
                    Discriminator = "Event",
                         Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Tafseer 1.0 [Sessions to bring us closer to Al-Quran]",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,25,19,30,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,25,8,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/ae5ea49a-0a8c-4af9-aac1-fe0b3d1f2f73-edu-tafseer-ig-0-1280x720.png",
                    EventDescription = "<p>Join PETRONITA from <strong>Fri,</strong>\n<strong>12 Oct - Fri, 30 Nov 2018</strong> to learn &amp; understand the meaning of specific Quran verses and its application.</p><p><br></p><p>By participating in this<strong>&nbsp;8 weekly sessions</strong> , we hope that our members will ;</p><ol><li>Be able to understand, relate and practice the knowledge obtained in day to day life as a person and leader in an organisation</li><li>Increase in self-motivation to be a better person from all aspects including leadership</li><li>Be able to appreciate and embrace PETRONAS cultural beliefs across all situations</li></ol><p><strong>Program Fee</strong> (8 sessions):</p><ul><li>Member: &nbsp;RM 50</li><li>Non Member: RM 65</li></ul><p><strong>Payment:</strong></p><ul><li>By cash to PETRONITA Office; or</li><li>Online transfer to Maybank 5644 9041 1972</li></ul><p data-empty=\"true\"><strong>To Register;&nbsp;</strong></p><ul><li data-empty=\"true\">Click on <strong>Join&nbsp;</strong>to register your interest</li><li data-empty=\"true\">Proof of payment of fees is required prior to confirmation of your seat.</li></ul><p data-empty=\"true\"><br></p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },
                //etiqa
                new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "HSE Day",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,25,17,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,21,9,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/83b92f2d-e882-4c1a-b771-a17870acd2bc-hse-myevent-1280x720.png",
                    EventDescription = "<p>The key of this event is to deliver the message about health and safety awareness to PETRONAS ICT staff.</p><p>The event will involve free health check, booth exhibition, staff price promotion on selected items, etc.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(15).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(15).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(3).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },
                //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },
                //etiqa
                new Event(){
                    Discriminator = "Event",
                         Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "In It 2 Win It Session 8",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,27,18,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,26,8,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/fda59b1d-5409-415e-8991-98c289dacdd9-banners2-1280x720.png",
                    EventDescription = "<p>PCFSSB Company Away Day 2018</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(15).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(15).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(3).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },  //twins
                new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                      Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                         Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                      Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                    Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }, //twins
                new Event(){
                    Discriminator = "Event",
                       Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "PETRONAS ICT Core Academy Foundation for Executives",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,28,16,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,23,8,30,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/112ee790-5610-4a62-8c6f-cf6d5b543238-ffe-14-nov-1280x720.png",
                    EventDescription = "<p>The PETRONAS ICT Core Academy: Foundation for Executives programme is our very own in-house academy which aims to provide common trainings across the organization to increase the levels of business knowledge. This is a great opportunity for you to network with your colleagues from other divisions.</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(14).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(2).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(14).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(2).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                },
                    new Event(){
                    Discriminator = "Event",
                        Members = new List<EventMember>(),
                    Bookmarks = new List<Bookmark>(),
                    Medias= new List<Media>(),
                    Spotlights = new List<Spotlight>(),
                    Id = Guid.NewGuid().ToString(),
                    EventName = "In It 2 Win It Session 1",
                    EventType = "Public",
                    EventDateTo = new DateTime(2018,12,27,18,0,0),
                    EventStatus = "Published",
                    EventDateFrom = new DateTime(2018,12,26,8,0,0),
                    EventImageUrl = "https://fileservice.petronas.com/api/v1/view/image/fda59b1d-5409-415e-8991-98c289dacdd9-banners2-1280x720.png",
                    EventDescription = "<p>PCFSSB Company Away Day 2018</p>",
                    IsFeatured = false,
                    VenueId = Venues.Skip(15).Take(1).FirstOrDefault().Id,
                    LocationId = Locations.Skip(3).Take(1).FirstOrDefault().Id,
                    Venue = Venues.Skip(15).Take(1).FirstOrDefault(),
                    Location = Locations.Skip(3).Take(1).FirstOrDefault(),
                    SurveyUrl = null,
                    SurveyResultUrl = null
                }
            };
            #endregion

            #region session + subsession
            var Sessions = new List<Session>() { 
                //2 sessions for F1
                    new Session(){ SubSessions = new List<SubSession>(), Members = new List<EventMember>(), EventId = Events.Skip(2).Take(1).FirstOrDefault().Id, AgendaName = "Formula 1", Discriminator = "Session", Id = Guid.NewGuid().ToString(), Venue = Venues.Skip(4).Take(1).FirstOrDefault(), VenueId = Venues.Skip(4).Take(1).FirstOrDefault().Id, Day = 2},
                new Session(){ SubSessions = new List<SubSession>(), Members = new List<EventMember>(), EventId = Events.Skip(2).Take(1).FirstOrDefault().Id, AgendaName = "Paddock Club", Discriminator = "Session", Id = Guid.NewGuid().ToString(), Venue = Venues.Skip(2).Take(1).FirstOrDefault(), VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, Day = 1},

                 //1 session for hse
                new Session(){ SubSessions = new List<SubSession>(), Members = new List<EventMember>(), EventId = Events.Skip(5).Take(1).FirstOrDefault().Id, AgendaName = "Day 1", Discriminator = "Session", Id = Guid.NewGuid().ToString(), Venue = Venues.Skip(15).Take(1).FirstOrDefault(), VenueId = Venues.Skip(15).Take(1).FirstOrDefault().Id, Day = 1}
            };

            var SubSessions = new List<SubSession>() { 
                //f1 1
                new SubSession(){ SessionId = Sessions.Skip(0).Take(1).FirstOrDefault().Id, AgendaName = "Pit Lane Walk", Venue = Venues.Skip(1).Take(1).FirstOrDefault(), VenueId = Venues.Skip(1).Take(1).FirstOrDefault().Id, TimeFrom ="09:00", TimeTo="09:30", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(0).Take(1).FirstOrDefault().Id, AgendaName = "Autograph Session", Venue = Venues.Skip(2).Take(1).FirstOrDefault(), VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, TimeFrom ="11:00", TimeTo="11:30", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(0).Take(1).FirstOrDefault().Id, AgendaName = "Truck Tour", Venue = Venues.Skip(2).Take(1).FirstOrDefault(), VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, TimeFrom ="14:00", TimeTo="15:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(0).Take(1).FirstOrDefault().Id, AgendaName = "Pit Lane Walk", Venue = Venues.Skip(3).Take(1).FirstOrDefault(), VenueId = Venues.Skip(3).Take(1).FirstOrDefault().Id, TimeFrom ="17:00", TimeTo="18:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                                                                                           
              //f1 2                                                                      
                new SubSession(){ SessionId = Sessions.Skip(1).Take(1).FirstOrDefault().Id, AgendaName = "F1 Drivers Arrival", Venue = Venues.Skip(2).Take(1).FirstOrDefault(),  VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, TimeFrom ="09:00", TimeTo="10:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(1).Take(1).FirstOrDefault().Id, AgendaName = "Team Pit Stop Practice", Venue = Venues.Skip(2).Take(1).FirstOrDefault(), VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, TimeFrom ="10:00", TimeTo="10:30", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(1).Take(1).FirstOrDefault().Id, AgendaName = "Practice 3", Venue = Venues.Skip(2).Take(1).FirstOrDefault(), VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, TimeFrom ="14:00", TimeTo="15:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(1).Take(1).FirstOrDefault().Id, AgendaName = "Qualifying 1", Venue = Venues.Skip(2).Take(1).FirstOrDefault(), VenueId = Venues.Skip(2).Take(1).FirstOrDefault().Id, TimeFrom ="17:00", TimeTo="18:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                                                                                           
              //hse 1                                                                    
                new SubSession(){ SessionId = Sessions.Skip(2).Take(1).FirstOrDefault().Id, AgendaName = "Arrival of Participants", Venue = Venues.Skip(15).Take(1).FirstOrDefault(), VenueId = Venues.Skip(15).Take(1).FirstOrDefault().Id, TimeFrom ="14:00", TimeTo="19:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(2).Take(1).FirstOrDefault().Id, AgendaName = "Ice Breaking Dinner", Venue = Venues.Skip(16).Take(1).FirstOrDefault(), VenueId = Venues.Skip(16).Take(1).FirstOrDefault().Id, TimeFrom ="18:30", TimeTo="19:45", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(2).Take(1).FirstOrDefault().Id, AgendaName = "Context Setting", Venue = Venues.Skip(16).Take(1).FirstOrDefault(), VenueId = Venues.Skip(16).Take(1).FirstOrDefault().Id, TimeFrom ="20:00", TimeTo="22:00", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() },
                new SubSession(){ SessionId = Sessions.Skip(2).Take(1).FirstOrDefault().Id, AgendaName = "Supper & Networking", Venue = Venues.Skip(17).Take(1).FirstOrDefault(), VenueId = Venues.Skip(17).Take(1).FirstOrDefault().Id, TimeFrom ="22:00", TimeTo="22:30", Discriminator = "SubSession", Id = Guid.NewGuid().ToString() }
            };

            //add subsession to session
            Sessions.Skip(0).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(0).Take(1).FirstOrDefault());
            Sessions.Skip(0).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(1).Take(1).FirstOrDefault());
            Sessions.Skip(0).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(2).Take(1).FirstOrDefault());
            Sessions.Skip(0).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(3).Take(1).FirstOrDefault());

            Sessions.Skip(1).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(4).Take(1).FirstOrDefault());
            Sessions.Skip(1).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(5).Take(1).FirstOrDefault());
            Sessions.Skip(1).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(6).Take(1).FirstOrDefault());
            Sessions.Skip(1).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(7).Take(1).FirstOrDefault());

            Sessions.Skip(2).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(8).Take(1).FirstOrDefault());
            Sessions.Skip(2).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(9).Take(1).FirstOrDefault());
            Sessions.Skip(2).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(10).Take(1).FirstOrDefault());
            Sessions.Skip(2).Take(1).FirstOrDefault().SubSessions.Add(SubSessions.Skip(11).Take(1).FirstOrDefault());
            #endregion

            #region bookmark
            var Bookmarks = new List<Bookmark>() {
                new Bookmark(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(1).Take(1).FirstOrDefault().Id, Discriminator = "Bookmark", Id = Guid.NewGuid().ToString() },
                new Bookmark(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(2).Take(1).FirstOrDefault().Id, Discriminator = "Bookmark", Id = Guid.NewGuid().ToString() },
                new Bookmark(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(6).Take(1).FirstOrDefault().Id, Discriminator = "Bookmark", Id = Guid.NewGuid().ToString() },
                new Bookmark(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(5).Take(1).FirstOrDefault().Id, Discriminator = "Bookmark", Id = Guid.NewGuid().ToString() }
            };

            Events.Skip(1).Take(1).FirstOrDefault().Bookmarks.Add(Bookmarks.Skip(0).Take(1).FirstOrDefault());
            Events.Skip(2).Take(1).FirstOrDefault().Bookmarks.Add(Bookmarks.Skip(1).Take(1).FirstOrDefault());
            Events.Skip(6).Take(1).FirstOrDefault().Bookmarks.Add(Bookmarks.Skip(2).Take(1).FirstOrDefault());
            Events.Skip(5).Take(1).FirstOrDefault().Bookmarks.Add(Bookmarks.Skip(3).Take(1).FirstOrDefault());
            #endregion

            #region member
            var Members = new List<EventMember>(){
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(0).Take(1).FirstOrDefault().Id, EventMemberStatus = "CHECKEDIN", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(1).Take(1).FirstOrDefault().Id, EventMemberStatus = "CHECKEDIN", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(2).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, SessionId = Sessions.Skip(0).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(6).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(8).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(9).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(10).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(11).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(12).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(13).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(14).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(15).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(16).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(17).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(18).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(19).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(20).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(21).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() },
                                    new EventMember(){ UserId = Users.FirstOrDefault().Id, EventId = Events.Skip(22).Take(1).FirstOrDefault().Id, EventMemberStatus = "JOINED", Discriminator = "EventMember", Id = Guid.NewGuid().ToString() }

            };
            Sessions.Skip(0).Take(1).FirstOrDefault().Members.Add(Members.Skip(3).Take(1).FirstOrDefault());

            Events.Skip(0).Take(1).FirstOrDefault().Members.Add(Members.Skip(0).Take(1).FirstOrDefault());
            Events.Skip(1).Take(1).FirstOrDefault().Members.Add(Members.Skip(1).Take(1).FirstOrDefault());
            Events.Skip(2).Take(1).FirstOrDefault().Members.Add(Members.Skip(2).Take(1).FirstOrDefault());

            Events.Skip(6).Take(1).FirstOrDefault().Members.Add(Members.Skip(4).Take(1).FirstOrDefault());
            Events.Skip(8).Take(1).FirstOrDefault().Members.Add(Members.Skip(5).Take(1).FirstOrDefault());
            Events.Skip(9).Take(1).FirstOrDefault().Members.Add(Members.Skip(6).Take(1).FirstOrDefault());
            Events.Skip(10).Take(1).FirstOrDefault().Members.Add(Members.Skip(7).Take(1).FirstOrDefault());
            Events.Skip(11).Take(1).FirstOrDefault().Members.Add(Members.Skip(8).Take(1).FirstOrDefault());
            Events.Skip(12).Take(1).FirstOrDefault().Members.Add(Members.Skip(9).Take(1).FirstOrDefault());
            Events.Skip(13).Take(1).FirstOrDefault().Members.Add(Members.Skip(10).Take(1).FirstOrDefault());
            Events.Skip(14).Take(1).FirstOrDefault().Members.Add(Members.Skip(11).Take(1).FirstOrDefault());
            Events.Skip(15).Take(1).FirstOrDefault().Members.Add(Members.Skip(12).Take(1).FirstOrDefault());
            Events.Skip(16).Take(1).FirstOrDefault().Members.Add(Members.Skip(13).Take(1).FirstOrDefault());
            Events.Skip(17).Take(1).FirstOrDefault().Members.Add(Members.Skip(14).Take(1).FirstOrDefault());
            Events.Skip(18).Take(1).FirstOrDefault().Members.Add(Members.Skip(15).Take(1).FirstOrDefault());
            Events.Skip(19).Take(1).FirstOrDefault().Members.Add(Members.Skip(16).Take(1).FirstOrDefault());
            Events.Skip(20).Take(1).FirstOrDefault().Members.Add(Members.Skip(17).Take(1).FirstOrDefault());
            Events.Skip(21).Take(1).FirstOrDefault().Members.Add(Members.Skip(18).Take(1).FirstOrDefault());
            Events.Skip(22).Take(1).FirstOrDefault().Members.Add(Members.Skip(19).Take(1).FirstOrDefault());


            #endregion

            #region spotlight
            var Spotlights = new List<Spotlight>();

            foreach (var item in Events)
            {
                SeedingHelpers.GetSpotlights(item.Spotlights, item);
            }

            #endregion

            #region media
            var Medias = new List<Media>();

            foreach (var item in Events)
            {
                SeedingHelpers.GetMedias(item.Medias, item);
            }

            #endregion

            #region call api to add data
            if (!CheckExist(CollectionNameConstant.COLLECTION_USER))
            {

                foreach (var user in Users)
                {
                    _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_USER), user).Wait();
                }
            }

            if (!CheckExist(CollectionNameConstant.COLLECTION_LOCATION))
            {
                foreach (var item in Locations)
                {
                    _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_LOCATION), item).Wait();
                }
            }

            if (!CheckExist(CollectionNameConstant.COLLECTION_VENUE))
            {
                foreach (var item in Venues)
                {
                    _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_VENUE), item).Wait();
                }
            }

            if (!CheckExist(CollectionNameConstant.COLLECTION_EVENT))
            {
                foreach (var item in Events)
                {
                    _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_EVENT), item).Wait();
                }
            }

            if (!CheckExist(CollectionNameConstant.COLLECTION_SESSION))
            {

                foreach (var session in Sessions)
                {
                    _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_SESSION), session).Wait();
                }
            }

            if (!CheckExist(CollectionNameConstant.COLLECTION_SUBSESSION))
            {

                foreach (var item in SubSessions)
                {
                    _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_SUBSESSION), item).Wait();
                }
            }

            //if (!CheckExist(CollectionNameConstant.COLLECTION_BOOKMARK))
            //{

            //    foreach (var item in Bookmarks)
            //    {
            //        _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_BOOKMARK), item).Wait();
            //    }
            //}

            //if (!CheckExist(CollectionNameConstant.COLLECTION_EVENT_MEMBER))
            //{

            //    foreach (var item in Members)
            //    {
            //        _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_EVENT_MEMBER), item).Wait();
            //    }
            //}

            //if (!CheckExist(CollectionNameConstant.COLLECTION_SPOTLIGHT))
            //{

            //    foreach (var item in Spotlights)
            //    {
            //        _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_SPOTLIGHT), item).Wait();
            //    }
            //}

            //if (!CheckExist(CollectionNameConstant.COLLECTION_MEDIA))
            //{

            //    foreach (var item in Medias)
            //    {
            //        _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, CollectionNameConstant.COLLECTION_MEDIA), item).Wait();
            //    }
            //}
            #endregion




        }
        public bool CheckExist(string collectionId)
        {
            return _client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, collectionId)).Take(1).Count() > 0;
        }
    }

    public static class SeedingHelpers
    {
        public static void GetSpotlights(List<Spotlight> list, Event _event)
        {
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Lewis Carl Davidson Hamilton MBE (born 7 January 1985) is a British racing driver who races in Formula One for Mercedes AMG Petronas. A five-time Formula One World Champion, he is often considered the best driver of his generation and widely regarded as one of the greatest drivers in the history of the sport.[note 1] He won his first World Championship title with McLaren in 2008, then moved to Mercedes where he won back-to-back titles in 2014 and 2015 before winning back-to-back titles again in 2017 and 2018. Statistically the most successful British driver in the history of the sport, Hamilton has more World Championship titles (5) and more race victories (73) than any other British driver in Formula One. He also holds records for the all-time most career points (3,018), the most wins at different circuits (26), the all-time most pole positions (83) and the most grand slams in a season (3).", SpotlightName = "Lewis Hamilton", SpotlightTitle = "F1 Driver", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/ddb20aa4-4d29-4a43-8c05-fb174d8d9eb6-1-440px-lewis-hamilton-2016-malaysia-2.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Kimi-Matias Räikkönen[2] (Finnish pronunciation: [ˈkimi ˈmɑtiɑs ˈræikːønen]; born 17 October 1979), nicknamed 'Iceman', is a Finnish racing driver currently driving in Formula One for Sauber. He won the 2007 FIA Formula One World Championship, in his first season at Scuderia Ferrari. After nine seasons racing in Formula One, he competed in the World Rally Championship in 2010 and 2011, then returning to Formula One from 2012. Besides his title, Räikkönen also finished second overall in 2003 and 2005 and third in 2008, 2012 and 2018. Prior to the 2019 season, Räikkönen has won 21 Grands Prix, making him the Finnish Formula One driver with the most race wins. He is the only driver to take a race win in the V10, V8 and the hybrid V6 engine eras.", SpotlightName = "Kimi Räikkönen", SpotlightTitle = "F1 Driver", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/349630b9-2ebd-4b18-b5f8-5628c2f2f704-1-rai-at-2017-russian-grand-prix-podium-cropped.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Fernando Alonso Díaz About this soundaudio (help·info) (born 29 July 1981) is a Spanish racing driver and former Formula One racing driver.[2] He is a two-time Formula One World Champion, and is often regarded as one of the greatest Formula One drivers in the history of the sport.[3][4][5][6][7][8][9][10] He has contested 17 seasons of Formula One. Outside Formula One, Alonso is currently leading the 2018–19 FIA World Endurance Championship with Toyota Gazoo Racing. He won the 2018 24 Hours of Le Mans on his first attempt with co-drivers Kazuki Nakajima and Sébastien Buemi.[11] He contested the 2017 Indianapolis 500[12] and the 2018 24 Hours of Daytona.", SpotlightName = "Fernando Alonso", SpotlightTitle = "F1 Driver", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/a16b6151-ce40-4a40-962b-e6061ecea52e-1-alonso-2016.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Céline Marie Claudette Dion CC OQ ChLD (/ˈdiːɒn/;[2] French: [selin djɔ̃]; born 30 March 1968) is a Canadian vocalist. Born into a large family from Charlemagne, Quebec, Dion emerged as a teen star in her homeland with a series of French-language albums during the 1980s. Dion first gained international recognition by winning both the 1982 Yamaha World Popular Song Festival and the 1988 Eurovision Song Contest where she represented Switzerland. After learning to speak English, she signed on to Epic Records in the United States. In 1990, Dion released her debut English-language album, Unison, establishing herself as a viable pop artist in North America and other English-speaking areas of the world.", SpotlightName = "Celine Dion", SpotlightTitle = "Singer", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/453a74fa-581a-4fb7-93e1-75a7a4cb0bad-1-440px-celine-dion-live-2017.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Il Divo (Italian pronunciation: [il ˈdiːvo]) is a multi-national classical crossover vocal group. The male quartet originated in the United Kingdom in December 2003,[1] bringing together singers Urs Bühler (Switzerland), Carlos Marín (Spain), David Miller (USA), and Sébastien Izambard (France).", SpotlightName = "Il Divo", SpotlightTitle = "Band", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/7a16367f-6bd5-40e8-8fcb-4aef031e1396-1-440px-il-divo-inhk.JPG" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Natalie Portman (born Neta-Lee Hershlag;[a] June 9, 1981) is a film actress, producer, and director with dual Israeli and American citizenship. She is the recipient of various accolades, including an Academy Award and two Golden Globe Awards", SpotlightName = "Natalie Portman", SpotlightTitle = "Actress", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/bfdc1ebb-f229-454a-ad9d-ac0b778bb0b0-1-440px-natalie-portman-cannes-2015-5-cropped.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Emma Charlotte Duerre Watson (born 15 April 1990)[3] is an English actress, model, and activist.[4] Born in Paris and brought up in Oxfordshire, Watson attended the Dragon School and trained as an actress at the Oxford branch of Stagecoach Theatre Arts. As a child artist, she rose to prominence after landing her first professional acting role as Hermione Granger in the Harry Potter film series, having acted only in school plays previously.[5] Watson appeared in all eight Harry Potter films from 2001 to 2011, earning worldwide fame, critical accolades, and around $60 million", SpotlightName = "Emma Watson", SpotlightTitle = "Actress", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/694c6e85-35e4-4ea5-8349-a14b60515fed-1-440px-emma-watson-2013.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "William Henry Gates III (born October 28, 1955) is an American business magnate, investor, author, philanthropist, humanitarian, and principal founder of Microsoft Corporation.[2][3] During his career at Microsoft, Gates held the positions of chairman, CEO and chief software architect, while also being the largest individual shareholder until May 2014. In 1975, Gates and Paul Allen launched Microsoft, which became the world's largest PC software company.[4][a] Gates led the company as chief executive officer until stepping down in January 2000, but he remained as chairman and created the position of chief software architect for himself.[7] In June 2006, Gates announced that he would be transitioning from full-time work at Microsoft to part-time work and full-time work at the Bill & Melinda Gates Foundation, which was established in 2000.[8] He gradually transferred his duties to Ray Ozzie and Craig Mundie.[9] He stepped down as chairman of Microsoft in February 2014 and assumed a new post as technology adviser to support the newly appointed CEO Satya Nadella.", SpotlightName = "Bill Gates", SpotlightTitle = "Microsoft CEO", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/2b71b121-ff49-4851-b996-14b3f9e11842-1-440px-bill-gates-2018.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Warren Edward Buffett (/ˈbʌfɪt/; born August 30, 1930)[2] is an American business magnate, investor, speaker and philanthropist who serves as the chairman and CEO of Berkshire Hathaway. He is considered one of the most successful investors in the world[3][4] and has a net worth of US$84.4 billion as of November 1, 2018, making him the third-wealthiest person in the world", SpotlightName = "Warren Buffett", SpotlightTitle = "Berkshire Hathaway CEO", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/b09f08b8-6aed-47d7-8365-f509a7525799-1-440px-warren-buffett-ku-visit.jpg" });
            list.Add(new Spotlight() { EventId = _event.Id, Discriminator = "Spotlight", Id = Guid.NewGuid().ToString(), SpotlightDescription = "Lawrence Edward Page[5] (born March 26, 1973) is an American computer scientist and Internet entrepreneur who co-founded Google with Sergey Brin", SpotlightName = "Larry Page", SpotlightTitle = "Google CEO", ImageUrl = "https://stagingfileservice.petronas.com/api/v2/r/p/ccb3ce26-af6f-42e9-a9e4-4d009f29a7bc/e0c48375-1e5f-4c56-9e3f-bd5c111afa75-1-440px-larry-page-in-the-european-parliament-1.jpg" });

        }
        public static void GetMedias(List<Media> list, Event _event)
        {
            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "Cartoon 1.mp4", EventId = _event.Id, MediaType = "Video", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/stream/video/6af05ae2-58b7-4653-9bb6-7b0ae4f2316b-25-an-private-event-1-20180425092949874-b9bb9-web-compressed.mp4", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/6af05ae2-58b7-4653-9bb6-7b0ae4f2316b-25-an-private-event-1-20180425092949874-b9bb9-thumbnail.jpg" });
            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "Introduction.mp4", EventId = _event.Id, MediaType = "Video", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/stream/video/74fa0d89-0b37-4c58-baaf-a0c7910686bd-25-an-private-event-1-20180425092958046-downl-web-compressed.mp4", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/74fa0d89-0b37-4c58-baaf-a0c7910686bd-25-an-private-event-1-20180425092958046-downl-thumbnail.jpg" });

            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "Quote.jog", EventId = _event.Id, MediaType = "Image", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/view/image/28714d53-3ff4-491f-ba91-1e117391404c-25-an-private-event-1-20180425092942889-15655.jpg", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/28714d53-3ff4-491f-ba91-1e117391404c-25-an-private-event-1-20180425092942889-15655-thumbnail.jpg" });
            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "F1 Warmup 1.jpg", EventId = _event.Id, MediaType = "Image", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/view/image/9bc41d89-9028-434c-9151-2660b902fbee-25-an-private-event-1-20180425092942968-30708.jpg", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/9bc41d89-9028-434c-9151-2660b902fbee-25-an-private-event-1-20180425092942968-30708-thumbnail.jpg" });
            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "F1 Warmup 2.jpg", EventId = _event.Id, MediaType = "Image", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/view/image/2b48c648-5473-4bc5-82c5-fbd650b9b16e-25-an-private-event-1-20180425092944202-image.jpg", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/2b48c648-5473-4bc5-82c5-fbd650b9b16e-25-an-private-event-1-20180425092944202-image-thumbnail.jpg" });

            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "Race.xlsx", EventId = _event.Id, MediaType = "Document", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/get/file/a7e2ab8b-3673-456e-8101-5b7622ddce42-25-an-private-event-1-20180830034858258-xlsx-.xlsx", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/xls.png" });
            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "Sepang map 1.pdf", EventId = _event.Id, MediaType = "Document", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/get/file/a7f16f0b-de84-4f72-bf84-4fc97d7d20d4-25-an-private-event-1-20180425093539437-pathw.pdf", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/pdf.png" });
            list.Add(new Media() { CreateDate = DateTime.UtcNow, FileName = "Sepang map 2.pdf", EventId = _event.Id, MediaType = "Document", Discriminator = "Media", Id = Guid.NewGuid().ToString(), Url = "https://fileservice.petronas.com/api/v1/get/file/e813b0ad-4c91-426c-ac42-d1c6e5063b30-25-an-private-event-1-20180425093529062-opm13.pdf", ThumbUrl = "https://fileservice.petronas.com/api/v1/view/image/pdf.png" });
        }


    }
}
