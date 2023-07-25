using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DataContext;
using Project_Api.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly Context _Context;

        public MedicalRecordsController(Context Context)
        {
            _Context = Context;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<MedicalRecords>>> Getrecord()
        {
            if (_Context.records == null)
            {
                return NotFound();
            }
            return await _Context.records.ToListAsync();
        }

        [HttpGet("GetAllPatientIds")]
        public async Task<ActionResult<IEnumerable<int>>> GetAllPatientIds()
        {
            // Query the Patient table and retrieve all patient IDs.
            var patientIds = await _Context.patients
                .Select(p => p.Id)
                .ToListAsync();

            return patientIds;
        }

        //[HttpGet("Getallprecords")]
        //public async Task<ActionResult<IEnumerable<MedicalRecords>>> Getrecordforp()
        //{
        //    if (_Context.records != null)
        //    {
        //        return await _Context.records.ToListAsync();


        //    }
        //    return NotFound();
        //}
        //GetMedicalbyId
        [HttpGet("GetMedicalbyId/{id:int}")]
        public async Task<ActionResult<MedicalRecords>> GetrecordbyId(int id)
        {
            if (_Context.records == null)
            {
                return NotFound();
            }
            var record = await _Context.records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return record;
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

        [HttpPost("PostMedical")]
        public async Task<ActionResult<MedicalRecords>> Postrecord(MedicalRecords record)
        {
            _Context.records.Add(record);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetrecordbyId), new { id = record.Id }, record);
        }

        [HttpPut("PutMedical")]
        public async Task<ActionResult> Putrecord(int id, MedicalRecords record)
        {

            _Context.Entry(record).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!recordExsite(id))
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

        private bool recordExsite(int id)
        {
            return (_Context.records?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpDelete("DeleteMedical/{id:int}")]
        public async Task<ActionResult<MedicalRecords>> Deleterecord(int id)
        {
            if (_Context.records == null)
            {
                return NotFound();
            }
            var record = await _Context.records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            _Context.records.Remove(record);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}