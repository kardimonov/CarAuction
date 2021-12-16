using CarAuction.Data.Enums;
using CarAuction.Data.Models;
using System.Collections.Generic;

namespace CarAuction.Logic.Models
{
    public class AuctionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AuctionStatus Status { get; set; }
        public List<Car> Cars { get; set; }
    }
}
