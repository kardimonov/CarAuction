namespace CarAuction.Data.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public Auction Auction { get; set; }
        public int AuctionId { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public bool WinResult { get; set; } = false;
    }
}
