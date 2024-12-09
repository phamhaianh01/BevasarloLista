using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BevasarloLista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ListDbContext _dbContext;

        public ItemController(ListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Getall()
        {
            var itemList = await _dbContext.Items.ToListAsync();
            return Ok(itemList);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetId(int id)
        {
            var item = await _dbContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> Put(int Id, [FromBody] Item item)
        {
            var DbItem = await _dbContext.Items.FindAsync(Id);
            if (DbItem == null)
            {
                return NotFound();
            }
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
            var itemList = await _dbContext.Items.ToListAsync();
            return Ok(itemList);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Item>>> Post([FromBody] Item item)
        {
            if (_dbContext.Items.Find(item.Id) != null)
            {
                return BadRequest($"Id {item.Id} already exists");
            }
            if (_dbContext.Users.Find(item.ForId) == null || _dbContext.Users.Find(item.CheckedById) == null)
            {
                return BadRequest($"User with Id: {item.ForId} not found");
            }
            if (_dbContext.Users.Find(item.CheckedById) == null)
            {
                return BadRequest($"User with Id: {item.CheckedById} not found");
            }
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
            var itemList = await _dbContext.Items.ToListAsync();
            return Ok(itemList);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> Delete(int id)
        {
            var item = _dbContext.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
            var itemList = await _dbContext.Items.ToListAsync();
            return Ok(itemList);
        }
    }
}
