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

        [HttpGet("/api/position")]
        public ActionResult UpdateLocation([FromQuery(Name = "id")]        string gameIdString, //[FromBody] PositionInfo newPosition)
                                           [FromQuery(Name = "latitude")]  string latitudeString,
                                           [FromQuery(Name = "longitude")] string longitudeString)
        {
            try
            {
                QuizGame game = (from g in _context.QuizGames
                                 where g.QuizGameId == Guid.Parse(gameIdString)
                                 select g).First();

                game.PlayerLatitude = double.Parse(latitudeString); // newPosition.Latitude;
                game.PlayerLongitude = double.Parse(longitudeString); //newPosition.Longitude;

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
