
namespace AHTS.Core.DTOs;

public class AddNewAssetDTO
{
    public string SerialNumber { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? AssignedToEmployeeId { get; set; }
}