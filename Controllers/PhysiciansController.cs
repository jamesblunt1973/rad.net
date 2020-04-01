using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rad.net.Data;
using rad.net.Models;

namespace rad.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysiciansController : ControllerBase
    {
        private readonly IRepository repository;

        public PhysiciansController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetPhysicians());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var physician = await repository.GetPhysician(id);

            if (physician == null)
            {
                return NotFound();
            }

            return Ok(physician);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Physician physician)
        {
            if (id != physician.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.EditPhysician(physician);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicianExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Physician physician)
        {
            await repository.NewPhysician(physician);

            // return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var physician = await repository.GetPhysician(id);
            if (physician == null)
            {
                return NotFound();
            }

            await repository.DeletePhysician(physician);

            return Ok();
        }

        private bool PhysicianExists(int id)
        {
            return repository.GetPhysician(id) != null;
        }
    }
}