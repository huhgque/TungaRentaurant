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
        BOOKED,
        USING,
        EMPTY
    }

    public enum TableType
    {
        PUBLIC,
        PRIVATE
    }
}
