using AHTS.Core.Entities;

namespace AHTS.Core.Interfaces.DLL;

public interface IemployeeRepo
{ 
    public Task<Employee?> getEmployee(int id);
    public Task<Dictionary<int, string>> getAllEmployeeNamesAndID();

}