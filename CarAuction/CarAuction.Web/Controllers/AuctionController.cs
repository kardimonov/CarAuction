using CarAuction.Data.Enums;
using CarAuction.Logic.Commands.Auction;
using CarAuction.Logic.Queries.Auctions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarAuction.Web.Controllers
{
    /// <summary>
    /// Controller to manage auctions
    /// </summary>
    /// <returns></returns>
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuctionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get main info about an auction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Auction/GetById/1
        [HttpGet]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id is not specified");
            }

            var result = await _mediator.Send(new GetAuctionByIdQuery { Id = (int)id });
            if (result == null)
            {
                return NotFound($"Auction with id:{id} is not found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get list of auctions with main info
        /// </summary>        
        /// <returns></returns>
        // GET: api/Auction/GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {            
            var result = await _mediator.Send(new GetAllAuctionsQuery());
            if (result == null)
            {
                return NotFound($"Auctions are not found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get an auction details with assigned cars and their bids
        /// </summary>        
        /// <returns></returns>
        // GET: api/Auction/GetDetailsById
        [HttpGet]
        public async Task<IActionResult> GetDetailsById(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id is not specified");
            }

            var auction = await _mediator.Send(new GetDetailsByIdQuery { Id = (int)id });
            if (auction == null)
            {
                return NotFound($"The Auction is not found");
            }

            return Ok(auction);
        }

        /// <summary>
        /// Add a new auction to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Auction/Create/
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AddAuctionCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            await _mediator.Send(model);
            return Ok(new { Result = true });
        }

        /// <summary>
        /// Update details of an auction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT: api/Auction/Update/
        //[Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuctionCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            var response = await _mediator.Send(model);

            if (response == null)
            {
                return NotFound($"Auction with id: {model.Id} is not found");
            }
            if (!response.Result)
            {
                return BadRequest(response.Message);
            }

            return Ok(new { response.Result, response.Message });
        }

        /// <summary>
        /// Delete an auction from database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // DELETE: api/Auction/Delete/
        //[Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteAuctionCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            var auction = await _mediator.Send(new GetAuctionByIdQuery { Id = model.Id });
            if (auction == null)
            {
                return NotFound($"Auction with id: {model.Id} is not found");
            }
            if (auction.Status != AuctionStatus.Planned)
            {
                return BadRequest("You cannot remove the auction, which has started or completed");
            }

            await _mediator.Send(model);
            return Ok(new { Result = true });
        }
    }
}
