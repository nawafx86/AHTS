using AHTS.DAL.Contexts;
using AHTS.Core.Interfaces.DLL;
using Microsoft.EntityFrameworkCore;

namespace AHTS.DAL.Repos.Employee;

public class employeeRepo : IemployeeRepo
{
    private readonly AhtsContext _context;

    public employeeRepo(AhtsContext context)
    {
        _context = context;
    }
    public async Task<Core.Entities.Employee?> getEmployee(int id)
    {
        try
        {
            Core.Entities.Employee? employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }
            return employee;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Dictionary<int, string>> getAllEmployeeNamesAndID()
    {
        try
        {
            var employees = await _context.Employees.Select(e => new
            {
                e.EmployeeId,
                e.Name
            }).OrderBy(e => e.EmployeeId).ToDictionaryAsync(e => e.EmployeeId, e=> e.Name);
            return employees;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }   
    }
}