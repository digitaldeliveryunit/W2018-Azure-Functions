namespace com.petronas.myevents.api.Constants
{
    internal enum QueueType
    {
        JOIN,
        UN_JOIN,
        BOOKMARK,
        UNBOOKMARK
    }

    internal enum UserStatus
    {
        NEW,
        CHECKEDIN,
        JOINED,
        INVITED
    }

    internal enum EventStatus
    {
        PUBLISHED,
        DRAFT
    }

    internal enum EventType
    {
        PUBLIC,
        PRIVATE
    }
}