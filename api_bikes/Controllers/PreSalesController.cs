using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_bikes.Data;
using api_bikes.Models;

namespace api_bikes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreSalesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PreSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PreSales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreSale>>> GetPreSale()
        {
            return await _context.PreSale.ToListAsync();
        }

        // GET: api/PreSales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PreSale>> GetPreSale(int id)
        {
            var preSale = await _context.PreSale.FindAsync(id);

            if (preSale == null)
            {
                return NotFound();
            }

            return preSale;
        }
        //ESTE PUEDE ESTAR FALLANDO
        // GET: api/PreSales/5/9
        [HttpGet("{userId}/{bikeId}")]
        public async Task<ActionResult<int>> GetPreSaleByData(int userId, int bikeId)
        {
            var preSalesByUserId = await _context.PreSale.Where(x => x.UserId == userId).ToListAsync();

            foreach (PreSale presale in preSalesByUserId)
            {
                if (presale.BikeId == bikeId)
                {
                    return presale.Id;
                }
            }
            return -1;
        }

        // PUT: api/PreSales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreSale(int id, PreSale preSale)
        {
            if (id != preSale.Id)
            {
                return BadRequest();
            }

            _context.Entry(preSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreSaleExists(id))
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

        // POST: api/PreSales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PreSale>> PostPreSale(PreSale preSale)
        {
            _context.PreSale.Add(preSale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPreSale", new { id = preSale.Id }, preSale);
        }

        // DELETE: api/PreSales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreSale(int id)
        {
            var preSale = await _context.PreSale.FindAsync(id);
            if (preSale == null)
            {
                return NotFound();
            }

            _context.PreSale.Remove(preSale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreSaleExists(int id)
        {
            return _context.PreSale.Any(e => e.Id == id);
        }
    }
}
