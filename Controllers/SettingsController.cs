using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rad.net.Data;
using rad.net.Models;

namespace rad.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IRepository repo;
        private readonly IAuthRepository auth;

        public SettingsController(IRepository repository, IAuthRepository auth)
        {
            repo = repository;
            this.auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            return Ok(await repo.Settings());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSetting(Setting setting)
        {
            await repo.Settings(setting);
            return Ok();
        }

        [HttpPost("password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordParameter data)
        {
            var nameId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(nameId))
                return Unauthorized();
            var userId = Convert.ToInt32(nameId);
            var user = await auth.GetUser(userId);
            if (user == null)
                return NotFound();
            try
            {
                await auth.UpdatePassword(user, data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}