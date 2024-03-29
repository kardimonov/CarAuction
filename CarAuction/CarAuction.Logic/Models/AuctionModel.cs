﻿using CarAuction.Data.Enums;
using System;

namespace CarAuction.Logic.Models
{
    public class AuctionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }  
        public DateTime EndTime { get; set; }    
        public AuctionStatus Status { get; set; }
    }
}
