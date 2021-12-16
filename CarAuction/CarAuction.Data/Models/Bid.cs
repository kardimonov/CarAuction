using System;

namespace CarAuction.Data.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }
        public bool WinResult { get; set; } = false;
        public AuctionCar AuctionCar { get; set; }
        public int AuctionCarId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
