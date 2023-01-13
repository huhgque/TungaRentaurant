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

   
    public enum TableStatus
    {
        EMPTY,
        BOOKED,
        USING
    }
}
