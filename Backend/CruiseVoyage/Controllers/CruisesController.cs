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
    [HttpGet("GetCabinsByCruise/{cruiseId}")]
    public async Task<IActionResult> GetCabinsByCruise(int cruiseId)
    {
        var cruise = await this._dbContext.Cruises
            .Include(x => x.Ship)
            .ThenInclude(ship => ship.Cabins)
            .ThenInclude(cabin => cabin.CabinBeds)
            .FirstOrDefaultAsync(x => x.idCruises == cruiseId);

        if (cruise == null)
        {
            return NotFound(new { message = "Cruise not found" });
        }

        var cabins = cruise.Ship.Cabins;
        return Ok(cabins);
    }
    [HttpGet("GetCabinType")]
    public async Task<IActionResult> GetCabinType()
    {
        var cabinTypes = await this._dbContext.CabinType.ToListAsync();
        return this.Ok(cabinTypes);
    }
    
    [HttpGet("Test")]
    public async Task<IActionResult> Test()
    {
        var random = new Random();
        for (int i = 1; i <= 72; i++)
        {
            int randomCount = random.Next(2, 5);
            for (int j = 0; j < randomCount; j++)
            {
                var cabinbed = new CabinBed();
                cabinbed.idCabin = i;
                await this._dbContext.CabinBed.AddAsync(cabinbed);
                await this._dbContext.SaveChangesAsync();
            }
        }
        return Ok();
    }
}