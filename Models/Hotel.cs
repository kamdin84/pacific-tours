using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ccse_cw1.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelLocation { get; set;}
        public int SinglePrice { get; set; }
        public int DoublePrice { get; set; }
        public int FamilyPrice { get; set; }
        public int AvailableSingle {  get; set; }
        public int AvailableDouble { get; set; }
        public int AvailableFamily { get; set;}
    }

    }

