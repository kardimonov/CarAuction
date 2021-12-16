using CarAuction.Logic.Commands.Car;
using CarAuction.Logic.Queries.Cars;
using CarAuction.Logic.Queries.Auctions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CarAuction.Web.Controllers
{
    /// <summary>
    /// Controller to manage cars
    /// </summary>
    /// <returns></returns>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get car details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Car/GetById/1
        [HttpGet]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id is not specified");
            }

            var result = await _mediator.Send(new GetCarByIdQuery { Id = (int)id });            
            if (result == null)
            {
                return NotFound($"Car with id: {id} is not found");
            }
            return Ok(result);
        }

        /// <summary>
        /// Calculate recommended price for a vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Car/GetRecommendedPrice/
        [HttpPost]
        public async Task<IActionResult> GetRecommendedPrice(GetRecommendedPriceQuery model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }
            var price = await _mediator.Send(model);
            return Ok(new { RecommendedPrice = price });
        }

        /// <summary>
        /// Add a new car to database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Car/Create/
        [HttpPost]
        public async Task<IActionResult> Create(AddCarCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }
            await _mediator.Send(model);            
            return Ok(new { Result = true });
        }

        /// <summary>
        /// Update details of a car
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT: api/Car/Update/
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            await _mediator.Send(model);
            return Ok(new { Result = true });
        }

        /// <summary>
        /// Delete a car from database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // DELETE: api/Car/Delete/
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCarCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            await _mediator.Send(model);
            return Ok(new { Result = true });
        }

        /// <summary>
        /// Assign a car to an auction
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT: api/Car/AssignToAuction/
        [HttpPut]
        public async Task<IActionResult> AssignToAuction(AssignToAuctionCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            var auction = await _mediator.Send(new GetAuctionByIdQuery { Id = model.AuctionId });
            if (auction == null)
            {
                return NotFound($"Auction with id: {model.AuctionId} is not found");
            }

            if (DateTime.UtcNow >= auction.StartTime)
            {
                return BadRequest("The car cannot be assigned to started auction");
            }

            var response = await _mediator.Send(model);
            if (!response.Result)
            {
                return BadRequest($"{response.Message}");
            }

            return Ok(new { Result = response.Result, Message = response.Message });
        }
    }
}
