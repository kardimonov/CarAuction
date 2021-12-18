using System.Collections.Generic;

namespace CarAuction.Data.Models
{
    public class AuctionCar
    {
        public int Id { get; set; }
        public int AuctionPrice { get; set; }
        public int AuctionId { get; set; }        
        public Auction Auction { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public List<Bid> Bids { get; set; }
    }
}
