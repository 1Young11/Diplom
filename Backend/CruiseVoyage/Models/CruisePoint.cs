namespace CruiseVoyage.Models;

public class CruisePoint
{
    public int idCruisePoint { get; set; }
    public string PointName { get; set; }
    public int ListNo { get; set; }
    public int idCruise { get; set; }
    public DateTime dateArrived { get; set; }
}