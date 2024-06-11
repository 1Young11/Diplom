using CruiseVoyage.DbContext;
using CruiseVoyage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        customer.CountPoint = 0;
        await this._dbContext.Customer.AddAsync(customer);
        await this._dbContext.SaveChangesAsync();
        return this.Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var customer = await this._dbContext.Customer.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (customer is null)
        {
            var customerNotFound = new ErrorResponse()
            {
                ErrorCode = "EMAIL_NOT_FOUND"
            };
            return this.BadRequest(customerNotFound);
        }

        if (customer.Password != request.Password) 
        {
            var customerNotFound = new ErrorResponse()
            {
                ErrorCode = "INVALID_PASSWORD"
            };
            return this.BadRequest(customerNotFound);
        }

        return this.Ok(customer);
    }


    public class AddCustomerRequest
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int CountPoint { get; set; }
    }
    
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}