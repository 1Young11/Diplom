namespace CruiseVoyage.Models;

public class Order
{
    public int idOrder { get; set; }
    public int idCruise { get; set; }
    public int Price { get; set; }
    public int CountBonus { get; set; }
    public int StatusPayment { get; set; }
    public virtual List<OrderCabinBed>? OrderCabinBed { get; set; } 
}