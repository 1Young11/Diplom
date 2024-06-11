using CruiseVoyage.DbContext;
using CruiseVoyage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CruiseVoyage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly CruiseDbContext _dbContext;

    public OrderController(CruiseDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    
    [HttpPost("addOrder")]
    public async Task<IActionResult> AddCustomer(
        [FromBody] AddOrderRequest request)
    {
        var order = new Order();
        order.idCruise = request.idCruise;
        order.Price = request.Price;
        order.CountBonus = request.CountBonus;
        order.StatusPayment = 0;
        order.idCustomer = request.idCustomer;
        await this._dbContext.Order.AddAsync(order);
        await this._dbContext.SaveChangesAsync();
        foreach (var item in request.OrderCabinBed)
        {
            var cabinbed = await this._dbContext.CabinBed.FirstOrDefaultAsync(x=>x.idCabinbed == item.idCabinBed);
            if (cabinbed is not null)
            {
                cabinbed.Name = item.Name;
                cabinbed.LastName = item.LastName;
                cabinbed.PhoneNumber = item.PhoneNumber;
            }
            var orderCabinBed = new OrderCabinBed()
            {
                idCabinBed = item.idCabinBed,
                idOrder = order.idOrder,
 
            };
            await this._dbContext.OrderCabinBed.AddAsync(orderCabinBed);
        }
        await this._dbContext.SaveChangesAsync();
        return this.Ok();
    }
    [HttpGet("GetOrder/{idCustomer}")]
    public async Task<IActionResult> GetOrder([FromRoute] int idCustomer)
    {
        var order = await this._dbContext.Order
            .Include(x => x.OrderCabinBed)
            .ThenInclude(x=>x.CabinBed)
            .ThenInclude(x=>x.Cabin)
            .ThenInclude(x=>x.CabinType)
            .Include(x=>x.Cruises)
            .ThenInclude(x=>x.CruisePoints)
            .Where(x=>x.idCustomer == idCustomer)
            .ToListAsync();
        return this.Ok(order);
    }
    
    public class AddOrderRequest
    {
        public int idCruise { get; set; }
        public int Price { get; set; }
        public int CountBonus { get; set; }
        public List<CabinBedDTO> OrderCabinBed { get; set; } = new List<CabinBedDTO>();
        public int idCustomer { get; set; }
    }

    public class CabinBedDTO
    {
        public int idCabinBed { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class CabinBedWithCabin
    {
        public CabinBed CabinBed { get; set; }
        public Cabin Cabin { get; set; }
    }
}