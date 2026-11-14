using AHTS.Core.DTOs;
namespace AHTS.Core.Interfaces.BLL;

public interface IemployeeService
{
    public Task<EmployeeDTO> getEmployee(int id);

    public Task<Dictionary<int, string>> getAllEmployeeNamesAndID();

}