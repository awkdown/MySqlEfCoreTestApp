using System;
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
    [Route("[controller]")]
    public class GameController : Controller
    {
        private readonly MyDbContext _context;

        public GameController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpPost("/api/game")]
        public ActionResult AddGame([FromBody] QuizGame credentials)
        {
            try
            {
                Player player = new Player();
                player.PlayerName = credentials.
                //credentials.
                //_context.QuizGames.Add(credentials);
                //_context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
