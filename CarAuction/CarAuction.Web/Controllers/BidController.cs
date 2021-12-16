using CarAuction.Logic.Commands;
using CarAuction.Logic.Queries.Auctions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarAuction.Web.Controllers
{
    /// <summary>
    /// Controller to manage bids
    /// </summary>
    /// <returns></returns>
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BidController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add a new bid to specific car to an opened auction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Bid/Create/
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AddBidCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            var auction = await _mediator.Send(new GetByAuctionCarIdQuery() { AuctionCarId = model.AuctionCarId });
            if (DateTime.Now < auction.StartTime || DateTime.Now >= auction.EndTime)
            {
                return BadRequest("The bids are accepted only while the auction is open");
            }

            model.UserId = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            await _mediator.Send(model);

            return Ok(new { Result = true });
        }
    }
}
