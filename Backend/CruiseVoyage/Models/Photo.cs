namespace CruiseVoyage.Models;

public class Photo
{
    public int idPhoto { get; set; }
    public int idCustomer { get; set; }
    public string UrlPath { get; set; }
    public DateTime DateDownload { get; set; }
    public string PhotoName { get; set; }
}