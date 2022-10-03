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

        // GET: /Category
        // READ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            // Async ensures mutual exclusion
            return await _context.Categories.ToListAsync();
        }

        // GET: /Category/5
        // READ
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id); //async ensures mutual exclusion

            if (category == null)
            {
                return NotFound();  //404 status
            }

            return category;
        }

        // PUT: /Category/5
        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)  //check ids match
            {
                return BadRequest();    //400 status
            }

            if (!CategoryExists(id))
            {
                return NotFound();  //404 status
            }
            _context.Entry(category).State = EntityState.Modified;  //update whole record

            try
            {
                await _context.SaveChangesAsync();  //save changes to database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();  //404 status
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //204 success code
        }

        // POST: /Category
        // UPDATE
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);  //add new record
            await _context.SaveChangesAsync();  //async ensures mutual exclusion

            //return result of GetCategory 
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        private bool CategoryExists(int id)
        {
            //check if category record with primary id field exists
            return _context.Categories.Any(e => e.CategoryId == id);
        }

    }
}
