using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BevasarloLista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ListDbContext dbContext;

        public UserController(ListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var itemList = await dbContext.Users.ToListAsync();
            return Ok(itemList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetId(int id)
        {

            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] User user)
        {
            dbContext.Users.Update(user);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {

            dbContext.Users.Add(user);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = dbContext.Users.Find(id);
            if (user != null)
            {
                dbContext.Users.Remove(user);
                return Ok();
            }

            return NotFound();

        }
       
    }
}
