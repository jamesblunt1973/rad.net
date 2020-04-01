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
    public class PatientsController : ControllerBase
    {
        private readonly IRepository repository;

        public PatientsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await repository.GetPatients());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await repository.GetPatient(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            try
            {
                await repository.EditPatient(patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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
        public async Task<ActionResult> Post(Patient patient)
        {
            await repository.NewPatient(patient);

            // return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var patient = await repository.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }

            await repository.DeletePatient(patient);

            return Ok();
        }

        [HttpPost("cumulative")]
        public async Task<IActionResult> CumulativeReport(PatientCumulativeParameter data)
        {
            return Ok(await repository.PatientsCumulitiveReport(data));
        }

        [HttpPost("details")]
        public async Task<IActionResult> DetailtsReport(PatientCumulativeResult data)
        {
            return Ok(await repository.PatientDetailsReport(data));
        }

        private bool PatientExists(int id)
        {
            return repository.GetPatient(id) != null;
        }
    }
}