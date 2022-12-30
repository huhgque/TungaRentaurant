using System.ComponentModel.DataAnnotations.Schema;

namespace TungaRestaurant.Models
{
    public class ReservationDetail
    {
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        [ForeignKey("Food")]
        public int FoodId { get;set; }
        public int Quantity { get; set; }
        public Food Food { get; set; }
        public Reservation Reservation { get; set; }
    }
}
