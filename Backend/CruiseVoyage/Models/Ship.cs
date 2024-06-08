namespace CruiseVoyage.Models;

public class Ship
{
    public int idShip { get; set; }
    public string NameShip { get; set; }

    public virtual List<Cabin> Cabins { get; set; } = new List<Cabin>();
    public virtual List<CabinType> CabinTypes { get; set; } = new List<CabinType>();
    
}