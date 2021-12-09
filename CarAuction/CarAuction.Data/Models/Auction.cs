using System;
using System.Collections.Generic;

namespace CarAuction.Data.Models
{
    public class Auction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AuctionStatus Status { get; set; } 
        public List<Bid> Bids { get; set; }
        public List<AuctionCar> Assignments { get; set; }
    }
}
