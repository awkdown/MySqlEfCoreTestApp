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
    public class ControlController : Controller
    {
        private readonly MyDbContext _context;

        public ControlController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/radius")]
        public ActionResult<Control> GetUpdateGPSRadius([FromQuery(Name = "r")] string radiusString)
        {
            try
            {
                Control cvs = (from c in _context.Controls
                               select c).First();

                cvs.GPSRadius = int.Parse(radiusString);

                _context.SaveChanges();
                return Ok(cvs);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("/api/over")]
        public ActionResult<Control> GetUpdateTHOver([FromQuery(Name = "end")] string overString)
        {
            try
            {
                Control cvs = (from c in _context.Controls
                               select c).First();

                cvs.THOver = bool.Parse(overString);

                _context.SaveChanges();
                return Ok(cvs);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


    }
}
