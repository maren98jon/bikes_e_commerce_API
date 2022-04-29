using System;
using System.Collections.Generic;

namespace api_bikes.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string AccountType { get; set; }

        public List<Sale> Sales { get; set; }
        public List<PreSale> Presales { get; set; }
    }
}
