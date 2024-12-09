using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BevasarloLista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private ListDbContext dbContext;

        public ItemController(ListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Getall()
        {
            var itemList = await dbContext.Items.ToListAsync();
            return Ok(itemList);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetId(int id)
        {
            var item = await dbContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int Id, [FromBody] Item item)
        {
            var DbItem = await dbContext.Items.FindAsync(Id);
            if (DbItem == null)
            {
                return NotFound();
            }
            dbContext.Items.Update(item);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Item item)
        {
            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = dbContext.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
