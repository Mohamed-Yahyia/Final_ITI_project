using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DataContext;
using Project_Api.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Context _Context;

        public EmployeeController(Context Context)
        {
            _Context = Context;
        }

        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_Context.employees == null)
            {
                return NotFound();
            }
            return await _Context.employees.ToListAsync();
        }
        [HttpGet("GetEmployeebyId/{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployeebyId(int id)
        {
            if (_Context.employees == null)
            {
                return NotFound();
            }
            var emp = await _Context.employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return emp;
        }

        [HttpGet("GetEmployeeByName/{name:alpha}")]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
            Employee emp = await _Context.employees.FirstOrDefaultAsync(d => d.Name == name);

            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound("Employee Not Found");
            }
        }
        [HttpPost("PostEmployee")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee emp)
        {
            _Context.employees.Add(emp);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeebyId), new { id = emp.Id }, emp);
        }
        [HttpPut("PutEmployee")]
        public async Task<ActionResult> PutEmployee(Employee emp)
        {
            //if (id != emp.Id)
            //{
            //    return BadRequest();
            //}
            _Context.Entry(emp).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeExsite(emp.Id))
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

        private bool employeeExsite(int id)
        {
            return (_Context.employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteEmployee/{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            if(_Context.employees==null)
            {
                return NotFound();
            }
            var emp = await _Context.employees.FindAsync(id);
            if (emp == null) 
            {
                return NotFound();
            }
            _Context.employees.Remove(emp);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}