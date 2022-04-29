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
    public class BikeTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BikeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BikeTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeType>>> GetBikeType()
        {
            return await _context.BikeType.ToListAsync();
        }

        // GET: api/BikeTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BikeType>> GetBikeType(int id)
        {
            var bikeType = await _context.BikeType.FindAsync(id);

            if (bikeType == null)
            {
                return NotFound();
            }

            return bikeType;
        }

        // PUT: api/BikeTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBikeType(int id, BikeType bikeType)
        {
            if (id != bikeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(bikeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeTypeExists(id))
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

        // POST: api/BikeTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BikeType>> PostBikeType(BikeType bikeType)
        {
            _context.BikeType.Add(bikeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBikeType", new { id = bikeType.Id }, bikeType);
        }

        // DELETE: api/BikeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBikeType(int id)
        {
            var bikeType = await _context.BikeType.FindAsync(id);
            if (bikeType == null)
            {
                return NotFound();
            }

            _context.BikeType.Remove(bikeType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BikeTypeExists(int id)
        {
            return _context.BikeType.Any(e => e.Id == id);
        }
    }
}
