using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MySqlEfCore.Data;
using MySqlEfCore.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MySqlEfCore.Controllers
{
    [ApiController]
    [Route("[controller]")] //default uri is /position

    public class PositionController : Controller
    {
        private readonly MyDbContext _context;

        public PositionController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpPost("/api/position")]
        public ActionResult UpdateLocation([FromQuery(Name = "id")] string gameIdString, [FromBody] PositionInfo newPosition)
        {
            try
            {
                QuizGame game = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g).First();

                game.PlayerLatitude = newPosition.Latitude;
                game.PlayerLongitude = newPosition.Longitude;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
