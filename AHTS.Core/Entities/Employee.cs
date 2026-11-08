
namespace AHTS.Core.Entities;

public class Employee
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = string.Empty!;

    public string? Department { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}