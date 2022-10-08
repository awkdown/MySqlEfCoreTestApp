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
    [Route("[controller]")]


    public class EnableCategoryController : Controller
    {
        private readonly MyDbContext _context;

        public EnableCategoryController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/enable")]
        public ActionResult GetEnabledCategory([FromQuery(Name = "category")] string catIdString, [FromQuery(Name = "enabled")] string enabledString)
        {
            try
            {
                Category category = (from c in _context.Categories
                                     where c.CategoryId == int.Parse(catIdString)
                                     select c).First();

                category.Enabled = bool.Parse(enabledString);

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
