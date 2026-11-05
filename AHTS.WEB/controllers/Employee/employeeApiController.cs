using AHTS.Core.DTOs;
using AHTS.Core.Interfaces.BLL;
using Microsoft.AspNetCore.Mvc;

namespace AHTS.WEB.controllers.Employee;


[ApiController]
[Route("/employeeApiController")]
public class employeeApiController : ControllerBase
{
    private readonly IemployeeService _iemployeeService;

    public employeeApiController(IemployeeService iemployeeService)
    {
        _iemployeeService = iemployeeService;
    }
    
    [HttpGet("{id}/getEmployee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async public Task<ActionResult<EmployeeDTO>> getEmplloyee(int id)
    {
        EmployeeDTO employeeDto = await _iemployeeService.getEmployee(id);
        return Ok(employeeDto);
    }
}