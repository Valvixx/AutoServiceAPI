﻿using AutoServiceAPI.Models;

namespace AutoService.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public List<Car> Cars { get; set; }
    }
}