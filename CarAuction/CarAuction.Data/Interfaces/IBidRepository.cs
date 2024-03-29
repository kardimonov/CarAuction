﻿using CarAuction.Data.Models;
using System.Threading.Tasks;

namespace CarAuction.Data.Interfaces
{
    public interface IBidRepository : IRepository
    {
        Task Create(Bid bid);
        Task Update(Bid bid);
        int GetMaxBid(int auctionCarId);
    }
}
