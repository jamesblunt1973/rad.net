using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using rad.net.Data;
using rad.net.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace rad.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;

        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterParameter data)
        {
            var userName = data.UserName.ToLower();
            if (await repo.UserExists(userName))
                return BadRequest("User name already exists.");
            var user = new User() { UserName = userName };
            var createdUser = await repo.Register(user, data.Password);
            // return CreatedAtRoute();
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginParameter data)
        {
            var user = await repo.Login(data.Username, data.Password);
            if (user == null)
                return Unauthorized();

            return Ok(new { 
                token = Helpers.GenerateJwtToken(user.Id.ToString()),
                name = user.UserName
            });
        }

        [HttpGet("checkUser")]
        public async Task<IActionResult> CheckUser()
        {
            var nameId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(nameId))
                return Ok(false);
            var userId = Convert.ToInt32(nameId);
            var user = await repo.GetUser(userId);
            if (user == null)
                return NotFound();
            return Ok(new
            {
                name = user.UserName
            });
        }
    }
}