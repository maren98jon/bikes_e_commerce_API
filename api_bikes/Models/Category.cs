using System;
using System.Collections.Generic;

namespace api_bikes.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BikeType> BikeTypes { get; set; }
    }
}
    