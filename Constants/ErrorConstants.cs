using System;
namespace com.petronas.myevents.api.Constants
{
    public static class ErrorMessageCodes
    {
        public static readonly string ValidationFailed = "1";
        public static readonly string ValidationFailedMessage = "Validations Failed.";

        public static readonly string NoRecordsFound = "2";
        public static readonly string NoRecordsFoundMessage = "No records found.";

        public static readonly string PostEventBookmarkError = "3";
        public static readonly string PostEventBookmarkErrorMessage = "Error occured in PostEventBookmark";

        public static readonly string DeleteEventAgendaError = "4";
        public static readonly string DeleteEventAgendaMessage = "Error occured in DeleteEventAgenda";

        public static readonly string PutEventAgendaError = "5";
        public static readonly string PutEventAgendaMessage = "Error occured in PutEventAgenda";

        public static readonly string PostEventAgendaError = "6";
        public static readonly string PostEventAgendaMessage = "Error occured in PostEventAgenda";

        public static readonly string GetCategoryError = "7";
        public static readonly string GetCategoryMessage = "Error occured in GetCategory";

        public static readonly string GetEventCategoryError = "8";
        public static readonly string GetEventCategoryMessage = "Error occured in GetEventCategory";

        public static readonly string GetEventAgendaError = "9";
        public static readonly string GetEventAgendaMessage = "Error occured in GetEventAgenda";

        public static readonly string GetEventMediaError = "10";
        public static readonly string GetEventMediaMessage = "Error occured in GetEventMedia";

        public static readonly string PostEventCategoryError = "11";
        public static readonly string PostEventCategoryMessage = "Error occured in PostEventCategory";

        public static readonly string PostEventOrganizerError = "12";
        public static readonly string PostEventOrganizerMessage = "Error occured in PostEventOrganizer";

        public static readonly string GetEventOrganizerError = "13";
        public static readonly string GetEventOrganizerMessage = "Error occured in GetEventOrganizer";

        public static readonly string GetEventByIdError = "14";
        public static readonly string GetEventByIdMessage = "Error occured in GetEventById";

        public static readonly string SearchEventError = "15";
        public static readonly string SearchEventMessage = "Error occured in SearchEvent";

        public static readonly string PostEventError = "16";
        public static readonly string PostEventMessage = "Error occured in PostEvent";

        public static readonly string PutEventError = "17";
        public static readonly string PutEventMessage = "Error occured in PutEvent";

        public static readonly string GetEventSpotlightError = "18";
        public static readonly string GetEventSpotlightMessage = "Error occured in GetEventSpotlight";

        public static readonly string GetEventSpotlightByTypeError = "19";
        public static readonly string GetEventSpotlightByTypeMessage = "Error occured in GetEventSpotlightByType";

        public static readonly string PostEventSpotlightError = "20";
        public static readonly string PostEventSpotlightMessage = "Error occured in PostEventSpotlight";

        public static readonly string PutEventSpotlightError = "21";
        public static readonly string PutEventSpotlightMessage = "Error occured in PutEventSpotlight";

        public static readonly string PostInviteUserEventError = "22";
        public static readonly string PostInviteUserEventMessage = "Error occured in PostInviteUserEvent";

        public static readonly string PostEventUserApproveError = "23";
        public static readonly string PostEventUserApproveMessage = "Error occured in PostEventUserApprove";

        public static readonly string PostEventUserDeclineError = "24";
        public static readonly string PostEventUserDeclineMessage = "Error occured in PostEventUserDecline";

        public static readonly string GetTicketByAgendaError = "25";
        public static readonly string GetTicketByAgendaMessage = "Error occured in GetTicketByAgenda";

        public static readonly string GetTicketByEventError = "26";
        public static readonly string GetTicketByEventMessage = "Error occured in GetTicketByEvent";

        public static readonly string GetAttendeesCountByEventError = "27";
        public static readonly string GetAttendeesCountByEventMessage = "Error occured in GetAttendeesCountByEvent";

        public static readonly string GetAttendeesAgendaCountByEventError = "28";
        public static readonly string GetAttendeesAgendaCountByEventMessage = "Error occured in GetAttendeesAgendaCountByEvent";

        public static readonly string GetAttendeesByEventError = "29";
        public static readonly string GetAttendeesByEventMessage = "Error occured in GetAttendeesByEvent";

        public static readonly string GetAttendeesByAgendaError = "30";
        public static readonly string GetAttendeesByAgendaMessage = "Error occured in GetAttendeesByAgenda";

        public static readonly string GetWaitListByEVentError = "31";
        public static readonly string GetWaitListByEVentMessage = "Error occured in GetWaitListByEVent";

        public static readonly string PostEventBatchjoinError = "32";
        public static readonly string PostEventBatchjoinMessage = "Error occured in PostEventBatchjoin";

        public static readonly string PostEventBatchUnjoinError = "33";
        public static readonly string PostEventBatchUnjoinMessage = "Error occured in PostEventBatchUnjoin";

        public static readonly string PostAgendaBatchjoinError = "34";
        public static readonly string PostAgendaBatchjoinMessage = "Error occured in PostAgendaBatchjoin";

        public static readonly string PostAgendaBatchUnjoinError = "35";
        public static readonly string PostAgendaBatchUnjoinMessage = "Error occured in PostAgendaBatchUnjoin";

        public static readonly string PostEventjoinError = "36";
        public static readonly string PostEventjoinMessage = "Error occured in PostEventjoin";

        public static readonly string PostEventUnjoinError = "37";
        public static readonly string PostEventUnjoinMessage = "Error occured in PostEventUnjoin";

        public static readonly string PostAgendaUnjoinError = "38";
        public static readonly string PostAgendaUnjoinMessage = "Error occured in PostAgendaUnjoin";

        public static readonly string CheckInEventError = "39";
        public static readonly string CheckInEventMessage = "Error occured in CheckInEvent";

        public static readonly string CheckInAgendaError = "40";
        public static readonly string CheckInAgendaMessage = "Error occured in CheckInAgenda";

        public static readonly string PostAgendaJoinError = "41";
        public static readonly string PostAgendaJoinMessage = "Error occured in PostAgendaJoin";

        public static readonly string PostExternalLoginError = "42";
        public static readonly string PostExternalLoginMessage = "Error occured in PostExternalLogin";

        public static readonly string GetEventLocationError = "43";
        public static readonly string GetEventLocationMessage = "Error occured in GetEventLocation";

        public static readonly string GetAllLocationError = "44";
        public static readonly string GetAllLocationMessage = "Error occured in GetAllLocation";

        public static readonly string GetLocationByIdError = "45";
        public static readonly string GetLocationByIdMessage = "Error occured in GetLocationById";

        public static readonly string PostEventLocationError = "46";
        public static readonly string PostEventLocationMessage = "Error occured in PostEventLocation";

        public static readonly string NoAccessTokenError = "47";
        public static readonly string NoAccessTokenMessage = "No Access Token";

        public static readonly string InvalidPlatformError = "48";
        public static readonly string InvalidPlatformMessage = "Error occured in Invalid Platform";

        public static readonly string GetMobileVersionError = "49";
        public static readonly string GetMobileVersionMessage = "Error occured in GetMobileVersion";

        public static readonly string EventIdNotExistErrorCode = "50";
        public static readonly string EventIdNotExistErrorMessage = "EventId not exist";

        public static readonly string AgendaIdNotExistErrorCode = "51";
        public static readonly string AgendaIdNotExistErrorMessage = "AgendaIdNotExist";

        public static readonly string AgendaNameAlreadyExistsErrorCode = "52";
        public static readonly string AgendaNameAlreadyExistsErrorMessage = "AgendaName is already exists! Try with different agendaname";

        public static readonly string TimeFromShouldBeCorrectFormatErrorCode = "53";
        public static readonly string TimeFromShouldBeCorrectFormatErrorMessage = "TimeFrom must be in correct format";

        public static readonly string TimeToShouldBeCorrectFormatErrorCode = "54";
        public static readonly string TimeToShouldBeCorrectFormatErrorMessage = "TimeTo must be in correct format";

        public static readonly string FromdateShouldBeLessThanTodateErrorCode = "55";
        public static readonly string FromdateShouldBeLessThanTodateErrorMessage = "Fromdate must be less than todate";

        public static readonly string CategoryIdNotExistErrorCode = "56";
        public static readonly string CategoryIdNotExistErrorMessage = "CategoryId not exist";

        public static readonly string MediaNotExistErrorCode = "57";
        public static readonly string MediaNotExistErrorMessage = "Media Not Exist";

        public static readonly string OrganizerUserIdNotExistErrorCode = "58";
        public static readonly string OrganizerUserIdNotExistErrorMessage = "OrganizerUserId not exist";

        public static readonly string UserIdNotExistErrorCode = "59";
        public static readonly string UserIdNotExistErrorMessage = "UserId not exist";

        public static readonly string EventNameAlreadyExistErrorCode = "60";
        public static readonly string EventNameAlreadyExistErrorMessage = "EventName is already exists! Try with different name";

        public static readonly string DateToMustBeGreaterthanDateFromErrorCode = "61";
        public static readonly string DateToMustBeGreaterthanDateFromErrorMessage = "DateTo must be greater than DateFrom";

        public static readonly string SeatTotalMustNotBeNegativeErrorCode = "62";
        public static readonly string SeatTotalMustNotBeNegativeErrorMessage = "SeatTotal must not be negative";

        public static readonly string SpotlighIdNotExistErrorCode = "63";
        public static readonly string SpotlighIdNotExistErrorMessage = "SpotlighId not exist";

        public static readonly string NoSpotlightErrorCode = "64";
        public static readonly string NoSpotlightErrorMessage = "No spotlight";

        public static readonly string SpotlightNameAlreadyExistsErrorCode = "65";
        public static readonly string SpotlightNameAlreadyExistsErrorMessage = "Spotlight name already exists! Try with different name";

        public static readonly string NotTheOrganizersErrorCode = "66";
        public static readonly string NotTheOrganizersErrorMessage = "You are not the organizers for this event";

        public static readonly string TicketNotExistErrorCode = "67";
        public static readonly string TicketNotExistErrorMessage = "Ticket not exist";

        public static readonly string NoPermissionToCheckinErrorCode = "68";
        public static readonly string NoPermissionToCheckinErrorMessage = "You dont have permission to checkin";

        public static readonly string UserCannotJoinAgendaErrorCode = "69";
        public static readonly string UserCannotJoinAgendaErrorMessage = "User cannot join agenda if he/she didn't join the event";

        public static readonly string NotJoinprivateEventWithoutInvitationErrorCode = "70";
        public static readonly string NotJoinprivateEventWithoutInvitationErrorMessage = "You cannot join this private event without invitation";

        public static readonly string RemoveYourselfErrorCode = "71";
        public static readonly string RemoveYourselfErrorMessage = "You cannot remove yourself from Organizer";

        public static readonly string GetBeaconError = "72";
        public static readonly string GetBeaconMessage = "Error occured in GetBeacon";

        public static readonly string GetBeaconByIdError = "73";
        public static readonly string GetBeaconByIdMessage = "Error occured in GetBeaconById";

        public static readonly string GetSessionVenueError = "74";
        public static readonly string GetSessionVenueMessage = "Error occured in GetSessionVenue";

        public static readonly string GetVenueError = "75";
        public static readonly string GetVenueMessage = "Error occured in GetAllVenu";

        public static readonly string GetVenueByIdError = "76";
        public static readonly string GetVenueByIdMessage = "Error occured in GetVenueById";

        public static readonly string PostBeaconError = "77";
        public static readonly string PostBeaconMessage = "Error occured in PostBeacon";

        public static readonly string PutBeaconError = "78";
        public static readonly string PutBeaconMessage = "Error occured in PutBeacon";

        public static readonly string DeleteBeaconError = "79";
        public static readonly string DeleteBeaconMessage = "Error occured in DeleteBeacon";

        public static readonly string PostVenueError = "80";
        public static readonly string PostVenueMessage = "Error occured in PostVenue";

        public static readonly string PutVenueError = "81";
        public static readonly string PutVenueMessage = "Error occured in PutVenue";

        public static readonly string DeleteVenueError = "82";
        public static readonly string DeleteVenueMessage = "Error occured in DeleteVenue";

        public static readonly string PostSessionVenueError = "83";
        public static readonly string PostSessionVenueMessage = "Error occured in PostSessionVenue";

        public static readonly string PutSessionVenueError = "83";
        public static readonly string PutSessionVenueMessage = "Error occured in PutSessionVenue";

        public static readonly string PostBeaconVenueError = "84";
        public static readonly string PostBeaconVenueMessage = "Error occured in PostBeaconVenue";

        public static readonly string PutBeaconVenueError = "85";
        public static readonly string PutBeaconVenueMessage = "Error occured in PutBeaconVenue";

        public static readonly string DeleteBeaconVenuError = "86";
        public static readonly string DeleteBeaconVenuMessage = "Error occured in DeleteBeaconVenu";

        public static readonly string GetBeaconVenueError = "87";
        public static readonly string GetBeaconVenueMessage = "Error occured in GetBeaconVenue";

        public static readonly string PostLocationError = "88";
        public static readonly string PostLocationMessage = "Error occured in PostLocation";

        public static readonly string PutLocationError = "89";
        public static readonly string PutLocationMessage = "Error occured in PutLocation";

        public static readonly string DeleteLocationError = "90";
        public static readonly string DeleteLocationMessage = "Error occured in DeleteLocation";

        public static readonly string PostBeaconLocationError = "91";
        public static readonly string PostBeaconLocationMessage = "Error occured in PostBeaconLocation";

        public static readonly string PutBeaconLocationError = "92";
        public static readonly string PutBeaconLocationMessage = "Error occured in PutBeaconLocation";

        public static readonly string DeleteBeaconLocationError = "93";
        public static readonly string DeleteBeaconLocationMessage = "Error occured in DeleteBeaconLocation";

        public static readonly string GetBeaconLocationError = "94";
        public static readonly string GetBeaconLocationMessage = "Error occured in GetBeaconLocation";

        public static readonly string LocationNameAlreadyExistErrorCode = "95";
        public static readonly string LocationNameAlreadyExistMessage = "Location name is already exists! Try with different name";

        public static readonly string VenueNameAlreadyExistErrorCode = "96";
        public static readonly string VenueNameAlreadyExistMessage = "Venue name is already exists! Try with different name";

        public static readonly string PostVenueBeaconError = "97";
        public static readonly string PostVenueBeaconMessage = "Error occured in PostVenuBeacon";

        public static readonly string PutVenueBeaconError = "98";
        public static readonly string PutVenueBeaconMessage = "Error occured in PutVenuBeacon";

        public static readonly string GetAllBeaconTypeError = "99";
        public static readonly string GetAllBeaconTypeErrorMessage = "Error occured in GetAllBeaconType";

        public static readonly string GetAvailableVenueError = "100";
        public static readonly string GetAvailableVenueErrorMessage = "Error occured in GetAvailableVenue";

        public static readonly string GetAvailableBeaconError = "101";
        public static readonly string GetAvailableBeaconErrorMessage = "Error occured in GetAvailableBeacon";

        public static readonly string BeaconNameAlreadyExistErrorCode = "102";
        public static readonly string BeaconNameAlreadyExistMessage = "Beacon name is already exists! Try with different name";

        public static readonly string BeaconsLocationAlreadyExistErrorCode = "103";
        public static readonly string BeaconsLocationAlreadyExistMessage = "Beacon id is already assign! Try with different beacon id";

        public static readonly string LocationSearchError = "104";
        public static readonly string LocationSearchErrorMessage = "Error occured in location search";

        public static readonly string BeaconSearchError = "105";
        public static readonly string BeaconSearchErrorMessage = "Error occured in beacon search";

        public static readonly string VenueSearchError = "106";
        public static readonly string VenueSearchErrorMessage = "Error occured in Venue search";

        public static readonly string GetVenueByLocationIdError = "107";
        public static readonly string GetVenueByLocationIdMessage = "Error occured in GetVenueByLocationId";

        public static readonly string AvailableLocationsWithoutVenueError = "108";
        public static readonly string AvailableLocationsWithoutVenueErrorMessage = "Error occured in AvailableLocationsWithoutVenue";

        public static readonly string AvailableLocationsWithoutBeaconError = "109";
        public static readonly string AvailableLocationsWithoutBeaconErrorMessage = "Error occured in AvailableLocationsWithoutBeacon";

        public static readonly string CheckNameError = "110";
        public static readonly string CheckNameErrorMessage = "Error occured in CheckName";

        public static readonly string VenueImageUploadFailedError = "111";
        public static readonly string VenueImageUploadFailedErrorMessage = "Error occured in VenueImageUpload";

        public static readonly string EventAlreadyExistInThisDurationErrorCode = "112";
        public static readonly string EventAlreadyExistInThisDurationErrorMessage = "Event is already exists in this duration! Try with different date/time";

        public static readonly string AgendaAlreadyExistInThisDurationErrorCode = "113";
        public static readonly string AgendaAlreadyExistInThisDurationErrorMessage = "Agenda is already exists in this duration! Try with different date/time";

        public static readonly string AgendaShouldBeEventTimeRangeErrorCode = "114";
        public static readonly string AgendaShouldBeEventTimeRangeErrorMessage = "Agenda should be in event time range";

        public static readonly string VenueDateShouldBeEventDateRangeErrorCode = "115";
        public static readonly string VenueDateShouldBeEventDateRangeErrorMessage = "Venue date should be in event date range";

        public static readonly string NoRecordsFoundWithLocationIdError = "116";
        public static readonly string NoRecordsFoundWithLocationIdErrorMessage = "No records found";

        public static readonly string UserNotFoundInAdErrorCode = "117";
        public static readonly string UserNotFoundInAdErrorMessage = "User(s) not found in Active Directory with provided name.";

        public static readonly string EmailAddressInvalidFormatErrorCode = "118";
        public static readonly string EmailAddressInvalidFormatErrorMessage = "Email Address in invalid format.";

        public static readonly string GetEventsError = "119";
        public static readonly string GetEventsMessage = "Error occured in GetEvents";

        public static readonly string UnAuthenticatedUserMessage = "User is not Authenicated";
        public static readonly string AccessDeniedMessage = "Access denied.Please contact the event organizer.";
        public static readonly string AuthenticationCredentialsInvalidMessage = "Authentication credentials were missing or incorrect";
         internal static readonly string GetUserProfileError = "";
        internal static readonly string GetUserProfileMessage = "";
        internal static readonly string GetAllUpcomingEventsError = "";
        internal static readonly string GetAllUpcomingEventsMessage = "";
        internal static readonly string GetUpcomingEventsError = "";
        internal static readonly string GetUpcomingEventsMessage = "";
        internal static readonly string GetPastEventsError = "";
        internal static readonly string GetPastEventsMessage = "";
        //internal static readonly DateTime PostEventJoinError;
        internal static string GetFeaturedEventsMessage = "";

        public static int GetFeaturedEventsError { get; internal set; }
    }
}
