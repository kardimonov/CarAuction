using CarAuction.Logic.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

            model.UserId ??= User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            
            var response = await _mediator.Send(model);
            if (!response.Result)
            {
                return BadRequest($"{response.Message}");
            }

            return Ok(new { Result = response.Result, Message = response.Message });
        }
    }
}
