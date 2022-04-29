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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.Include(s => s.Sales).Include(ps => ps.Presales).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/Mail/maren@maren.com
        [HttpGet("Mail/{mail}")]
        public async Task<ActionResult<bool>> CheckUserByMail(string mail)
        {
            var users = await _context.User.ToListAsync();
            bool find = false;
            if (users != null)
            {
                foreach (User user in users)
                {
                    if (user.Mail == mail)
                    {
                        find = true;
                    } 
                }
            }
            return find; 
        }

        // GET: api/Users/getByMail/maren@maren.com
        [HttpGet("getByMail/{mail}")]
        public async Task<ActionResult<User>> GetUserByMail(string mail)
        {
            var users = await _context.User.ToListAsync();
            if (users != null)
            {
                foreach (User user in users)
                {
                    if (user.Mail == mail)
                    {
                        return user;
                    }
                }
            }
            return NotFound();
        }

        // GET: api/Users/getIdByMail/maren@maren.com
        [HttpGet("getIdByMail/{mail}")]
        public async Task<ActionResult<int>> GetUserIdByMail(string mail)
        {
            var users = await _context.User.ToListAsync();
            if (users != null)
            {
                foreach (User user in users)
                {
                    if (user.Mail == mail)
                    {
                        return user.Id;
                    }
                }
            }
            return NotFound();
        }



        // GET: api/Users/PreSale/6
        [HttpGet("PreSale/{id}")]
        public ActionResult<IEnumerable<Bike>> GetUserPreSales(int id)
        {
            var preSales = _context.PreSale.Where(x => x.UserId == id).ToList();
            var bikes = new List<Bike>();

            foreach (PreSale presale in preSales)
            {
                Bike bike = _context.Bike.Where(x => x.Id == presale.BikeId).FirstOrDefault();
                bikes.Add(bike);
            }
            if (bikes == null)
            {
                return NotFound();
            }
            return bikes;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/checkPass
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("checkPass")]
        public async Task<ActionResult<bool>> CheckPassword(User user)
        {
            var users = await _context.User.ToListAsync();
            foreach (var userDb in users)
            {
                if (userDb.Mail==user.Mail)
                {
                    if (userDb.Password == user.Password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
