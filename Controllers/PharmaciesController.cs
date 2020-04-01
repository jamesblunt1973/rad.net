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
    public class PharmaciesController : ControllerBase
    {
        private readonly IRepository repository;

        public PharmaciesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetPharmacies());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pharmacy = await repository.GetPharmacy(id);

            if (pharmacy == null)
            {
                return NotFound();
            }

            return Ok(pharmacy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Pharmacy pharmacy)
        {
            if (id != pharmacy.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.EditPharmacy(pharmacy);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyExists(id))
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
        public async Task<ActionResult> Post(Pharmacy pharmacy)
        {
            await repository.NewPharmacy(pharmacy);

            // return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
            return StatusCode(201);
        }

        [HttpPost("sms")]
        public async Task<ActionResult> SendPassword(Pharmacy pharmacy)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            var password = rnd.Next(100000, 999999).ToString();
            pharmacy.Password = Helpers.Encrypt(pharmacy.Code, password);
            await repository.EditPharmacy(pharmacy);
            return Ok(new { password });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pharmacy = await repository.GetPharmacy(id);
            if (pharmacy == null)
            {
                return NotFound();
            }

            await repository.DeletePharmacy(pharmacy);

            return Ok();
        }

        private bool PharmacyExists(int id)
        {
            return repository.GetPharmacy(id) != null;
        }
    }
}