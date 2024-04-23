using CruiseVoyage.DbContext;
using CruiseVoyage.Models;
using Microsoft.AspNetCore.Mvc;

namespace CruiseVoyage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CruiseDbContext _dbContext;

    public CustomerController(CruiseDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
        
    [HttpPost("addCustomer")]
    public async Task<IActionResult> AddCustomer(
        [FromBody] AddCustomerRequest request)
    {
        var customer = new Customer();
        customer.Name = request.Name;
        customer.Surname = request.LastName;
        customer.Phone = request.Phone;
        customer.Email = request.Email;
        customer.Password = request.Password;
        customer.IdRole = 1;
        await this._dbContext.Customer.AddAsync(customer);
        await this._dbContext.SaveChangesAsync();
        return this.Ok();
    }

    public class AddCustomerRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}