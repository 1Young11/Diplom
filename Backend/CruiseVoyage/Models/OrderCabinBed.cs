namespace CruiseVoyage.Models;

public class OrderCabinBed
{
    public int idOrderCabinbed { get; set; }
    public int idOrder { get; set; }
    public int idCabinBed { get; set; }
    public virtual CabinBed CabinBed { get; set; } = null!;
}