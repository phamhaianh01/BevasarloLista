using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BevasarloLista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ListDbContext _dbContext;

        public UserController(ListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var itemList = await _dbContext.Users.ToListAsync(); 
            return Ok(itemList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetId(int id)
        {

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            var DbUser = await _dbContext.Users.FindAsync(id);
            if (DbUser == null)
            {
                return NotFound();
            }
            DbUser.Username = user.Username;
            DbUser.Password = user.Password;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();

        }

    }
}
