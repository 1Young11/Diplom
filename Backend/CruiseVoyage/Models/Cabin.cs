namespace CruiseVoyage.Models;

public class Cabin
{
    public int idCabin { get; set; }
    public int idShip { get; set; }
    public int idCabintype { get; set; }
    public int Floor { get; set; }

    public virtual CabinType CabinType { get; set; } = null!;
    public virtual List<CabinBed> CabinBeds { get; set; } = new List<CabinBed>();
}