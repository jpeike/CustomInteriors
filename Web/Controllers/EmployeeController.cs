using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("", Name = "GetAllEmployees")]
    public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
    {
        return await _employeeService.GetAllEmployees();
    }

    [HttpGet("{id:int}", Name = "GetEmployeeById")]
    public async Task<EmployeeModel?> GetEmployeeById(int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }

    [HttpPost("", Name = "CreateEmployee")]
    public async Task<EmployeeModel> CreateEmployee([FromBody] EmployeeModel employeeModel)
    {
        return await _employeeService.CreateEmployee(employeeModel);
    }

    [HttpPut("", Name = "UpdateEmployee")]
    public async Task UpdateEmployee([FromBody] EmployeeModel employeeModel)
    {
        await _employeeService.UpdateEmployee(employeeModel);
    }

    [HttpDelete("{id:int}", Name = "DeleteEmployee")]
    public async Task<bool> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}