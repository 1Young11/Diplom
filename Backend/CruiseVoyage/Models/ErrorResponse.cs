namespace CruiseVoyage.Models;

public class ErrorResponse
{
    public string ErrorCode { get; set; } = null!;
    public string? ErrorMsg { get; set; }
}