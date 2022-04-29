using System;
using System.Collections.Generic;

namespace api_bikes.Models
{
    public class BikeType
    {
        public int Id { get; set; }
        
        public int BikeId { get; set; }
        public int CategoryId { get; set; }

        //public Bike Bike { get; set; }
        //public Category Category { get; set; }
    }   
}
