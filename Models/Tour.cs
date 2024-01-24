using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ccse_cw1.Models
{
    public class Tour
    {
        [Key]
        public int TourID { get; set; }
        public required string TourName { get; set; }
        public DateTime TourStart { get; set; }
        public int TourPrice { get; set;}
        public int TourSpaces { get; set;}
        public int Duration { get; set;}

        public DateTime TourEnd { get; set; }


    }
}
