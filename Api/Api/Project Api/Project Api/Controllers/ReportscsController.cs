using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DataContext;
using Project_Api.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportscsController : ControllerBase
    {
        private readonly Context _Context;

        public ReportscsController(Context Context)
        {
            _Context = Context;
        }
        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<MedicalRecords>>> Getreport()
        {
            if (_Context.records != null)
            {
                return await _Context.records.ToListAsync();


            }
            return NotFound();
        }
        [HttpGet("GetReportbyId/{id:int}")]
        public async Task<ActionResult<Reportscs>> GetreportbyId(int id)
        {
            if (_Context.reportscs == null)
            {
                return NotFound();
            }
            var report = await _Context.reportscs.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }
        [HttpGet("GetrecordsPatient/{id:int}")]
        public async Task<ActionResult<MedicalRecords>> GetrecordsPatient(int id)
        {
            //var result = new List <MedicalRecords>();
            var record = _Context.records.Where(e => e.Idpatient == id).ToList();
            if (record != null)
            {
                return Ok(record);
            }
            return NotFound();

        }

        [HttpPost("PostReport")]
        public async Task<ActionResult<Reportscs>> Postreport(Reportscs report)
        {
            _Context.reportscs.Add(report);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetreportbyId), new { id = report.Id }, report);
        }

        [HttpPut("PutReport")]
        public async Task<ActionResult> Putreport(int id, Reportscs report)
        {

            _Context.Entry(report).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reportExsite(id))
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

        private bool reportExsite(int id)
        {
            return (_Context.reportscs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteReport/{id:int}")]
        public async Task<ActionResult<Reportscs>> Deletereport(int id)
        {
            if (_Context.reportscs == null)
            {
                return NotFound();
            }
            var report = await _Context.reportscs.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            _Context.reportscs.Remove(report);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}
