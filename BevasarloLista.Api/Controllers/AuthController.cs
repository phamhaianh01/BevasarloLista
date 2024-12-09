using BevasarloLista.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BevasarloLista.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private ListDbContext _dbContext;

    public AuthController(ListDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get(string username, string password)
    {
      var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
      if (user == null)
      {
        return Unauthorized();
      }
      return Ok(user);
    }
  }
}
