using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pcstore_api.Models;



namespace pcstore_api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoredbContext _context;

        public ProductsController(StoredbContext context)
        {
            _context = context;
        }

        // POSTx: api/Search
        [HttpPost("Search")]
        public async Task<ProductData> GetComputers([FromBody]Filters filters)
        {
            System.Console.WriteLine(filters);
            var query = _context.Computers.AsQueryable();

            if (filters.StorageId?.Any() == true)
            {
                query = query.Where(c => filters.StorageId.Contains(c.StorageId));
            }
            if (filters.GraphicsCardId?.Any() == true)
            {
                query = query.Where(c => filters.GraphicsCardId.Contains(c.GraphicsCardId));
            }
            if (filters.PsuId?.Any() == true)
            {
                query = query.Where(c => filters.PsuId.Contains(c.PsuId));
            }           
            if (filters.ProcessorId?.Any() == true)
            {
                query = query.Where(c => filters.ProcessorId.Contains(c.ProcessorId));
            }
            if (filters.MemoryId?.Any() == true)
            {
                query = query.Where(c => filters.MemoryId.Contains(c.MemoryId));
            }
            var productList = await query
                .Include(x => x.GraphicsCard)
                .Include(x => x.Psu)
                .Include(x => x.Storage)
                .Include(x => x.Processor).ToListAsync();

            return new ProductData{ 
                Count = productList.Count, 
                Computers=productList
            };

        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Computer>> GetComputer(int id)
        {
            var computer = await _context.Computers
                .Where(p => p.ComputerId == id)
                .Include(x => x.GraphicsCard)
                .Include(x => x.Psu)
                .Include(x => x.Storage)
                .Include(x => x.Processor).FirstOrDefaultAsync();

            if (computer == null)
            {
                return NotFound();
            }         
            return computer;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputer(int id, Computer computer)
        {
            if (id != computer.ComputerId)
            {
                return BadRequest();
            }

            _context.Entry(computer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Computer>> PostComputer(Computer computer)
        {
            _context.Computers.Add(computer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Create Computer", new { id = computer.ComputerId }, computer);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }

            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputerExists(int id)
        {
            return _context.Computers.Any(e => e.ComputerId == id);
        }
    }

    public class ProductData
    {
        public int Count { get; set; }
        public List<Computer>? Computers { get; set; }
    }

    public class Filters
    {
        public int[]? ProcessorId { get; set;}
        public  int[]?BrandId { get; set;}
        public  int[]? GraphicsCardId { get; set;}
        public  int[]? PsuId { get; set;}
        public  int[]? StorageId { get; set;}
        public  int[]? MemoryId { get; set;}
        
    }
}
