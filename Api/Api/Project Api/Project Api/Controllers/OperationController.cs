using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Api.DataContext;
using Project_Api.Models;

namespace Project_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : Controller
    {
        private readonly Context _Context;

        public OperationController(Context Context)
        {
            _Context = Context;
        }
        [HttpGet("Getall")]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperations()
        {
            if (_Context.operations == null)
            {
                return NotFound();
            }
            return await _Context.operations.ToListAsync();
        }
        [HttpGet("GetOperationsbyId/{id:int}")]
        public async Task<ActionResult<Operation>> GetOperationsId(int id)
        {
            if (_Context.operations == null)
            {
                return NotFound();
            }
            var op = await _Context.operations.FindAsync(id);
            if (op == null)
            {
                return NotFound();
            }
            return op;
        }

        [HttpGet("GetOperationsByName/{name:alpha}")]
        public async Task<IActionResult> GetOperationsByName(string name)
        {
            Operation op = await _Context.operations.FirstOrDefaultAsync(d => d.operation_type == name);

            if (op != null)
            {
                return Ok(op);
            }
            else
            {
                return NotFound("operations Not Found");
            }
        }
        [HttpPost("PostOperations")]
        public async Task<ActionResult<Operation>> PostOperations(Operation op)
        {
            _Context.operations.Add(op);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperationsId), new { id = op.Id }, op);
        }
        [HttpPut("PutOperations")]
        public async Task<ActionResult> PutOperations(Operation op)
        {

            _Context.Entry(op).State = EntityState.Modified;
            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!operationExsite(op.Id))
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

        private bool operationExsite(int id)
        {
            return (_Context.operations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteOperations/{id:int}")]
        public async Task<ActionResult<Operation>> DeleteOperations(int id)
        {
            if (_Context.operations == null)
            {
                return NotFound();
            }
            var op = await _Context.operations.FindAsync(id);
            if (op == null)
            {
                return NotFound();
            }
            _Context.operations.Remove(op);
            await _Context.SaveChangesAsync();
            return Ok();
        }
    }
}
