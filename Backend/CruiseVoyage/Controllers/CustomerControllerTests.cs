using CruiseVoyage.Controllers;
using CruiseVoyage.DbContext;
using CruiseVoyage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class CustomerControllerTests
{
    private readonly DbContextOptions<CruiseDbContext> _dbContextOptions;

    public CustomerControllerTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<CruiseDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Fact]
    public async Task AddCustomer_ValidRequest_ReturnsOk()
    {
        // Arrange
        var request = new CustomerController.AddCustomerRequest
        {
            Name = "John",
            LastName = "Doe",
            Phone = "1234567890",
            Email = "john.doe@example.com",
            Password = "password123"
        };
        
        using var context = new CruiseDbContext(_dbContextOptions);

        var controller = new CustomerController(context);

        // Act
        var result = await controller.AddCustomer(request);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        var customer = await context.Customer.FirstOrDefaultAsync(x => x.Email == request.Email);
        Assert.NotNull(customer);
        Assert.Equal(request.Name, customer.Name);
        Assert.Equal(request.LastName, customer.Surname);
        Assert.Equal(request.Phone, customer.Phone);
        Assert.Equal(request.Email, customer.Email);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "John",
            Surname = "Doe",
            Phone = "1234567890",
            Email = "john.doe@example.com",
            Password = "password123",
            IdRole = 1,
            CountPoint = 0,
            PhotoURL = ""
        };

        using var context = new CruiseDbContext(_dbContextOptions);
        await context.Customer.AddAsync(customer);
        await context.SaveChangesAsync();

        var controller = new CustomerController(context);

        var request = new CustomerController.LoginRequest
        {
            Email = "john.doe@example.com",
            Password = "password123"
        };

        // Act
        var result = await controller.Login(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCustomer = Assert.IsType<Customer>(okResult.Value);
        Assert.Equal(customer.Email, returnedCustomer.Email);
    }

    [Fact]
    public async Task Login_InvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        using var context = new CruiseDbContext(_dbContextOptions);

        var controller = new CustomerController(context);

        var request = new CustomerController.LoginRequest
        {
            Email = "invalid.email@example.com",
            Password = "password123"
        };

        // Act
        var result = await controller.Login(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
        Assert.Equal("EMAIL_NOT_FOUND", errorResponse.ErrorCode);
    }

    [Fact]
    public async Task Login_InvalidPassword_ReturnsBadRequest()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "John",
            Surname = "Doe",
            Phone = "1234567890",
            Email = "john.doe@example.com",
            Password = "password123",
            IdRole = 1,
            CountPoint = 0,
            PhotoURL = ""
        };

        using var context = new CruiseDbContext(_dbContextOptions);
        await context.Customer.AddAsync(customer);
        await context.SaveChangesAsync();

        var controller = new CustomerController(context);

        var request = new CustomerController.LoginRequest
        {
            Email = "john.doe@example.com",
            Password = "wrongpassword"
        };

        // Act
        var result = await controller.Login(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
        Assert.Equal("INVALID_PASSWORD", errorResponse.ErrorCode);
    }
}
