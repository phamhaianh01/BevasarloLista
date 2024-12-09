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
        public async Task<ActionResult<IEnumerable<Item>>> Get()
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
        public async Task<ActionResult> Put([FromBody] Item item)
        {
            dbContext.Items.Update(item);
            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Item item)
        {

            dbContext.Items.Add(item);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = dbContext.Items.Find(id);
            if (item != null)
            {
                dbContext.Items.Remove(item);
                return Ok();
            }

            return NotFound();
            
        }



    }
}
