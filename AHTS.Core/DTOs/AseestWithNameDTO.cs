
namespace AHTS.Core.DTOs;

public class AseestWithNameDTO
{
    public int AssetId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SerialNumber { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? AssignedToEmployeeId { get; set; }
}