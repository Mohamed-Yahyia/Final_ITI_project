using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DataContext;
using Project_Api.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly Context _Context;

        public AppointmentsController(Context Context)
        {
            _Context = Context;
        }
        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<Appointments>>> Getappoint()
        {
            if (_Context.appointments == null)
            {
                return NotFound();
            }
            return await _Context.appointments.ToListAsync();
        }
        [HttpGet("GetappointbyId/{id}")]
        public async Task<ActionResult<Appointments>> GetappointbyId(int id)
        {
            if (_Context.appointments == null)
            {
                return NotFound();
            }
            var appoint = await _Context.appointments.FindAsync(id);
            if (appoint == null)
            {
                return NotFound();
            }
            return appoint;
        }
        [HttpPost("Postappoint")]
        public async Task<ActionResult<Appointments>> Postappoint(Appointments appoint)
        {
            _Context.appointments.Add(appoint);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetappointbyId), new { id = appoint.Id }, appoint);
        }
        [HttpPut("Putappoint")]
        public async Task<ActionResult> Putappoint(int id, Appointments appoint)
        {
            //if (id != appoint.Id)
            //{
            //    return BadRequest();
            //}
            _Context.Entry(appoint).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appointExsite(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool appointExsite(int id)
        {
            return (_Context.appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpDelete("Deleteappoint/{id}")]
        public async Task<ActionResult<Appointments>> Deleteappoint(int id)
        {
            if (_Context.appointments == null)
            {
                return NotFound();
            }
            var appoint = await _Context.appointments.FindAsync(id);
            if (appoint == null)
            {
                return NotFound();
            }
            _Context.appointments.Remove(appoint);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}
