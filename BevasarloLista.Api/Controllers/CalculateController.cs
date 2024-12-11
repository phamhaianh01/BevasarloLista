using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BevasarloLista.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private ListDbContext _dbContext;

        public CalculateController(ListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Double>> Get(int year, int month, int userId)
        {
            if (year < 1 || year > DateTime.MaxValue.Year || month < 1 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }
            if (await _dbContext.Users.FindAsync(userId) == null)
            {
                return BadRequest("Invalid user id.");
            }
            var result = Calculate(
                await _dbContext.Users.FindAsync(userId),
                await _dbContext.Users.CountAsync(),
                _dbContext.Items.AsQueryable(),
                new DateTime(year, month, 1));
            return Ok(result);

        }

        public static double Calculate(User currentUser, int userCount, IQueryable<Item> items, DateTime date)
        {

            // Calculate the amount of money user has to pay or will get back
            // if the sum is positive, the user has to pay
            // if the sum is negative, the user will get back
            double sum = 0;

            sum += items.Where(i => i.ForUserId == currentUser.Id && i.CheckedById != currentUser.Id && i.CheckedById != null
                && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => i.Price * i.Amount).Sum(); //AMI AZ ÖVÉ, DE NEM Ő VETT MEG

            sum += items.Where(i => i.ForUserId == null && i.CheckedById != null && i.CheckedById != currentUser.Id
             && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => (i.Price * i.Amount) / userCount)
                .Sum(); //KÖZÖS

            sum -= items.Where(i => i.ForUserId == null && i.CheckedById == currentUser.Id 
             && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => (i.Price * i.Amount) / userCount).Sum(); // KÖZÖS, DE Ő VETTE MEG

            sum -= items.Where(i => i.ForUserId != currentUser.Id && i.ForUserId != null && i.CheckedById == currentUser.Id 
             && i.PurchaseDate.Month == date.Month && i.PurchaseDate.Year == date.Year)
                .Select(i => i.Price * i.Amount).Sum();
            // MÁSOKÉ, DE Ő VETTE MEG
            return sum;
        }

    }
}
