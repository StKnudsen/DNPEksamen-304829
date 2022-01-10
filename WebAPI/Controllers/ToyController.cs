using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using WebAPI.DataAccess;

namespace WebAPI.Controllers
{
    [ApiController]
    
    public class ToyController : ControllerBase
    {
        private KinderGardenContext dbContext;

        public ToyController(KinderGardenContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Toy/owner/{ownerId:int}")]
        public async Task<ActionResult> AddToyAsync([FromRoute] int ownerId, [FromBody] Toy toy)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Child toyOwner = await dbContext.Children
                    .Include(child => child.Toys)
                    .FirstAsync(child => child.Id == ownerId);
                
                toyOwner.Toys.Add(toy);
                dbContext.Toys.Add(toy);

                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("Child/Toy/{id:int}")]
        public async Task<ActionResult> DeleteToyAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Toy toy = await dbContext.Toys.FindAsync(id);

                dbContext.Toys.Remove(toy);

                await dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}