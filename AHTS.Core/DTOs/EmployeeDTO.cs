namespace AHTS.Core.DTOs;

public class EmployeeDTO
{
    public int EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Department { get; set; }

    public DateOnly HireDate { get; set; }
}