
namespace AHTS.Core.Entities;

public class Asset
{
    public int AssetId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? AssignedToEmployeeId { get; set; }

    public virtual Employee? AssignedToEmployee { get; set; }
}