using AHTS.DAL.Contexts;
using AHTS.Core.Interfaces.DLL;
using AHTS.Core.Entities;

namespace AHTS.DAL.Repos.Employee;

public class employeeRepo : IemployeeRepo
{
    private readonly AhtsContext _dbAHTS;

    public employeeRepo(AhtsContext dbAHTS)
    {
        _dbAHTS = dbAHTS;
    }
    public async Task<Core.Entities.Employee?> getEmployee(int id)
    {
        try
        {
            Core.Entities.Employee? employee = await _dbAHTS.Employees.FindAsync(id);
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
}