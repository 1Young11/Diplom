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
        var cruises = await this._dbContext.Cruises
            .Include(x => x.CruisePoints)
            .Include(x => x.Ship)
            .OrderBy(x=>x.idCruises)
            .ToListAsync();
        foreach (var cruise in cruises)
        {
            if (cruise.CruisePoints is not null)
            {
                cruise.CruisePoints = cruise.CruisePoints.OrderBy(x=>x.ListNo).ToList();
            }
        }
        return this.Ok(cruises);
    }
    
}