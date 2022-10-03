using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MySqlEfCore.Data;
using MySqlEfCore.Models;

using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MySqlEfCore.Controllers
{
    [ApiController]
    [Route("[controller]")] //default uri is /category

    public class LeaderboardController : Controller
    {
        private readonly MyDbContext _context;

        public LeaderboardController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/leaderboard")]
        public ActionResult<IEnumerable<LeaderboardEntry>> GetAllEntries()
        {
            try
            {
                List<LeaderboardEntry> entries =
                    (from e in _context.LeaderboardEntries
                     select e).ToList();
                return Ok(entries);
            }
            catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
