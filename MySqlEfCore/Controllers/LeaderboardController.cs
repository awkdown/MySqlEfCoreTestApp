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

                List<LeaderboardRow> rows = new List<LeaderboardRow>();
                foreach (var e in entries)
                {
                    LeaderboardRow r = new LeaderboardRow();
                    r.TeamCode = e.TeamCode;
                    r.TeamName = e.TeamName;
                    r.Score = e.Score;
                    r.League = e.League;
                    rows.Add(r);
                }
                return Ok(rows);
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
