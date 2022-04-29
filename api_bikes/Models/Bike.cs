using System;
using System.Collections.Generic;

namespace api_bikes.Models
{
    public class Bike
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Size { get; set; }

        public int WheelSize { get; set; }

        public string Photo { get; set; }

        public int OriginalPrice { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; } 

        public string Description { get; set; }

        public  List<BikeType> BikeTypes { get; set; }

        public List<PreSale> PreSales { get; set; }

        public List<Sale> Sales { get; set; }
    }
}
