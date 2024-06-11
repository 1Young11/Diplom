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
        await this._dbContext.Order.AddAsync(order);
        await this._dbContext.SaveChangesAsync();
        foreach (var item in request.OrderCabinBed)
        {
            var orderCabinBed = new OrderCabinBed()
            {
                idCabinBed = item,
                idOrder = order.idOrder,
            };
            await this._dbContext.OrderCabinBed.AddAsync(orderCabinBed);
        }
        await this._dbContext.SaveChangesAsync();
        return this.Ok();
    }
    
    public class AddOrderRequest
    {
        public int idCruise { get; set; }
        public int Price { get; set; }
        public int CountBonus { get; set; }
        public List<int> OrderCabinBed { get; set; } = new List<int>();
    }
}