using System.Collections.Generic;

namespace CarAuction.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";
        public List<Bid> Bids { get; set; }
    }
}
