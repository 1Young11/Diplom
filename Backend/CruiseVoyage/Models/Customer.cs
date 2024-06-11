namespace CruiseVoyage.Models;

public class Customer
{
    public int idCustomer { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IdRole { get; set; }
    public int CountPoint { get; set; }
    public string? PhotoURL { get; set; }
}