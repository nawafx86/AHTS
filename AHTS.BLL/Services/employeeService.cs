using AHTS.Core.Interfaces.DLL;
using AHTS.Core.DTOs;
using AHTS.Core.Entities;
using AHTS.Core.Interfaces.BLL;

namespace AHTS.BLL.Services;

public class employeeService : IemployeeService
{
    private IemployeeRepo _iemployeeRepo;

    public employeeService(IemployeeRepo iemployeeRepo)
    {
        _iemployeeRepo = iemployeeRepo;
    }
    public async Task<EmployeeDTO>getEmployee(int id)
    {
        Employee? employee = await _iemployeeRepo.getEmployee(id);
        if (employee != null)
        {
            EmployeeDTO employeeDto = new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Department = employee.Department,
                HireDate = employee.HireDate
            };
            return employeeDto;
        }

        return new EmployeeDTO { EmployeeId = -1};
    }

    public async Task<Dictionary<int, string>> getAllEmployeeNamesAndID()
    {
        return await _iemployeeRepo.getAllEmployeeNamesAndID();
    }
}