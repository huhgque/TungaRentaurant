namespace TungaRestaurant.Models
{
    public enum UserStatus
    {
        NORMAL,
        SUSPEND,
        BAN
    }
    public enum FoodStatus
    {
        AVAILABLE,
        UNAVAILABLE
    }
    public enum ReservationStatus
    {
        USING,
        END
    }

    public enum TableType
    {
        PUBLIC,
        PRIVATE
    }
    public enum TableStatus
    {
        EMPTY,
        BOOKED,
        USING
    }
    public enum Sex
    {
        FEMALE,
        MALE,
        OTHER
    }
}
