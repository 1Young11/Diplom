using CruiseVoyage.DbContext;
using CruiseVoyage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CruiseVoyage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CruisesController : ControllerBase
{
    private readonly CruiseDbContext _dbContext;

    public CruisesController(CruiseDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpGet("GetCruises")]
    public async Task<IActionResult> GetCruises()
    {
        var cruises = await this._dbContext.Cruises.ToListAsync();
        return this.Ok(cruises);
    }
}