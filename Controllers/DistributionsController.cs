using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rad.net.Models;

namespace rad.net.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionsController : ControllerBase
    {
        private readonly IRepository repo;

        public DistributionsController(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Post(DistributionsParameter data)
        {
            return Ok(await repo.GetDistributions(data));
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewDistribution(Distribution ditribution)
        {
            await repo.NewDistribution(ditribution);
            return StatusCode(201);
        }

        [HttpPost("details")]
        public async Task<IActionResult> DistributionDetails(DistributionsParameter data)
        {
            return Ok(await repo.GetDistributionDetails(data));
        }
    }
}