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

    public class ScoreBoardController : Controller
    {
        private readonly MyDbContext _context;    //database context

        public ScoreBoardController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/scoreboard")]
        public ActionResult<IEnumerable<ScoreboardInfo>> GetAllEntries()
        {
            try
            {
                List<ScoreboardInfo> result = new List<ScoreboardInfo>();

                List<QuizGame> gameList =
                    (from g in _context.QuizGames
                     select g).ToList();

                foreach (var g in gameList)
                {
                    ScoreboardInfo si = new ScoreboardInfo();
                    si.Score = g.Score;
                    si.Name = g.PlayerName;

                    result.Add(si);
                }

                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
