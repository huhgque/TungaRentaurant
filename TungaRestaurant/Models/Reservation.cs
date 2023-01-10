using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TungaRestaurant.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int NumberOfGuest { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ReservationAt { get; set; }
        public DateTime ReservationEnd { get; set; }
        public ReservationStatus Status { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public UserInfo User { get; set; }
        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; }
    }
}
