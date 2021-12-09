namespace CarAuction.Data.Models
{
    public class AuctionCar
    {
        public int AuctionId { get; set; }
        public Auction Auction { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
