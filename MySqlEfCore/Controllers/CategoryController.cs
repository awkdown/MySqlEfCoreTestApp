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
    public class CategoryController : Controller
    {
        private readonly MyDbContext _context;    //database context

        public CategoryController(MyDbContext context)    //constructor
        {
            _context = context;
        }

        [HttpGet("/api/categories")]
        public ActionResult<IEnumerable<Category>> GetAllEntries()
        {
            try
            {
                List<CategoryInfo> result = new List<CategoryInfo>();
 
                List<Category> categories =
                    (from c in _context.Categories
                     select c).ToList();

                foreach (var c in categories)
                {
                    CategoryInfo ci = new CategoryInfo();
                    ci.CategoryName = c.CategoryName;
                    ci.Options = (from l in _context.QuizCategoryLengths
                                    where l.CategoryId == c.CategoryId
                                    select l.NumberOfQuestions).ToList();

                    result.Add(ci);
                }

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
