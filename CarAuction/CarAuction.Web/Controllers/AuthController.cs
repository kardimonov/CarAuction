using CarAuction.Logic.Interfaces;
using CarAuction.Logic.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarAuction.Web.Controllers
{
    /// <summary>
    /// Controller to manage users
    /// </summary>
    /// <returns></returns>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// User authorization
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            var audience = Request.GetDisplayUrl();
            var response = await _service.LoginCustomer(model, audience);

            if (!response.Success)
            {                
                return NotFound(new { result = false, message = "The login and password combination entered doesn't match." });
            }

            return Ok(response);
        }

        /// <summary>
        /// User registration 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not correct");
            }

            await _service.AddCustomer(model);
            return Ok(new { Result = true });
        }
    }
}
