using System;

namespace TungaRestaurant.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public  DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
