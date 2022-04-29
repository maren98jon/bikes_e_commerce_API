using System;
namespace api_bikes.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BikeId { get; set; }

        public DateTime SaleDate { get; set; }

        //public User User{ get; set; }
        
        //public Bike Bike { get; set; }
    }
}   
