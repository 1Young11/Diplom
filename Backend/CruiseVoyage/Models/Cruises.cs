namespace CruiseVoyage.Models;

public class Cruises
{
    public int idCruises { get; set; }
    public string arrivedFrom { get; set; }
    public DateTime arrivedFromDate { get; set; }
    public string arrivedTo { get; set; }
    public DateTime arrivedToDate { get; set; }
    public float Rating { get; set; } 
    public int CountPoint { get; set; }
    public float TravelDistance { get; set; }
}