using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BevasarloLista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private ListDbContext dbContext;

        public CalculateController(ListDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Double>> Get(int year, int month, int userId)
        {
            var date = new DateTime(year, month, 1);
            var user = dbContext.Users.Find(userId);

            var result = Calculate(user, dbContext.Users.Count(), dbContext.Items.AsQueryable(), date);
            return Ok(result);
            
        }

        public static double Calculate(User currentUser, int userCount, IQueryable<Item> items, DateTime date)
        {
            double sum = 0;

            sum += items.Where(i => i.For == currentUser && i.CheckedBy != currentUser
                && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => i.Price * i.Amount).Sum(); //AMI AZ ÖVÉ, DE NEM Ő VETT MEG

            sum += items.Where(i => i.For == null
             && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => i.Price * i.Amount / userCount)
                .Sum(); //KÖZÖS

            sum -= items.Where(i => i.For == null && i.CheckedBy == currentUser
             && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => i.Price * i.Amount / userCount).Sum(); // KÖZÖS, DE Ö VETTE MEG

            return sum;
        }

    }
}
