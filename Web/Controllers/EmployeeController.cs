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

    [HttpGet("")]
    public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
    {
        return await _employeeService.GetAllEmployees();
    }

    [HttpGet("{id:int}")]
    public async Task<EmployeeModel?> GetEmployeeById(int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }

    [HttpPost("")]
    public async Task<EmployeeModel> CreateEmployee([FromBody] Employee employeeModel)
    {
        return await _employeeService.AddEmployee(employeeModel);
    }

    [HttpPut("")]
    public async Task UpdateEmployee([FromBody] EmployeeModel employeeModel)
    {
        await _employeeService.UpdateEmployee(employeeModel);
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}