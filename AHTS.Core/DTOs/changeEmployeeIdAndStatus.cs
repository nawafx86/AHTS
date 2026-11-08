namespace AHTS.Core.DTOs;

public class changeEmployeeIdAndStatus
{
    public int AssetID { get; set; }
    public int? EmployeeID { get; set; }
    public string Status { get; set; } = null!;
}