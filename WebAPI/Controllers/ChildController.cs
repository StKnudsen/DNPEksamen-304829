using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using WebAPI.DataAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ChildController : ControllerBase
    {
        private KinderGardenContext dbContext;

        public ChildController(KinderGardenContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult> AddChildAsync([FromBody] Child child)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await dbContext.Children.AddAsync(child);
                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Child>>> GetChildrenAsync()
        {
            try
            {
                return await dbContext.Children.Include(child => child.Toys).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}