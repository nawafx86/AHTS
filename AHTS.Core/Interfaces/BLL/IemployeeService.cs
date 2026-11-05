using AHTS.Core.DTOs;
namespace AHTS.Core.Interfaces.BLL;

public interface IemployeeService
{
    public Task<EmployeeDTO> getEmployee(int id);

}