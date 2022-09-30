using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySqlEfCore.Data;
using MySqlEfCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlEfCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("/api/people")]
        public ActionResult<IEnumerable<Person>> GetAllPeople()
        {
            try
            {
                List<Person> people =
                    (from p in _context.person
                     select p).ToList();
                return Ok(people);
            }
            catch(Exception ex) when (ex is FormatException || ex is ArgumentException)
            {
                return Conflict();
            }
        }

        [HttpPost("/api/people")]
        public ActionResult AddPerson([FromBody] Person credentials)
        {
            try
            {
                _context.person.Add(credentials);
                _context.SaveChanges();
                return Ok();
            } 
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        public IActionResult Index()
        {
            var people = _context.person.ToList();
            return View(people);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
