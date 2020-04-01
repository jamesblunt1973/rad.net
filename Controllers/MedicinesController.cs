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
    public class MedicinesController : ControllerBase
    {
        private readonly IRepository repository;

        public MedicinesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetMedicines());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var medicine = await repository.GetMedicine(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(medicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.EditMedicine(medicine);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
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
        public async Task<ActionResult> Post(Medicine medicine)
        {
            await repository.NewMedicine(medicine);

            // return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var medicine = await repository.GetMedicine(id);
            if (medicine == null)
            {
                return NotFound();
            }

            await repository.DeleteMedicine(medicine);

            return Ok();
        }

        [HttpPost("cumulative")]
        public async Task<IActionResult> CumulativeReport(MedicineCumulativeParameter data)
        {
            return Ok(await repository.MedicinesCumulitiveReport(data));
        }

        [HttpPost("details")]
        public async Task<IActionResult> DetailtsReport(MedicineCumulativeResult data)
        {
            return Ok(await repository.MedicineDetailsReport(data));
        }

        private bool MedicineExists(int id)
        {
            return repository.GetMedicine(id) != null;
        }
    }
}