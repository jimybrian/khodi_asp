using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    //Specify Block(If it exists),House Number, Deposit, Rent Amount, No of rooms, Drop Down House Type(Ensuite, Studio/Bedsitter, 1,2,3,4,5 Bedroom
    public class Units
    {
        [Key]
        public Guid unitId { get; set; }
        public float maxRent { get; set; }
        public float minRent { get; set; }
        public float deposit { get; set; }
        public int numOfRooms { get; set; }
        public string blockNumber { get; set; }
        public int numOfBedrooms { get; set; }
        public string unitNnumber { get; set; }
        public string amenities { get; set; }
        public string inventory { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; } 

    }
}