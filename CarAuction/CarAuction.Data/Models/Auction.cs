using CarAuction.Data.Enums;
using System;
using System.Collections.Generic;

namespace CarAuction.Data.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }  // Utc time
        public DateTime EndTime { get; set; }    // Utc time
        public AuctionStatus Status { get; set; } 
        public List<AuctionCar> Assignments { get; set; }
    }
}
