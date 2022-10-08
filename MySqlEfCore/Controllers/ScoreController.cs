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

    public class ScoreController : Controller
    {
        private readonly MyDbContext _context;    //database context

        public ScoreController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/score")]
        public ActionResult<ScoreInfo> GetCurrentScore([FromQuery(Name = "id")] string gameIdString)
        {
            try
            {
                ScoreInfo result = new ScoreInfo();

                result.Score = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g.Score).First();

                return Ok(result);
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
