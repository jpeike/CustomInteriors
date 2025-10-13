using Core;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("GetEmployees")]
    public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
    {
        return await _employeeService.GetAllEmployees();
    }

    [HttpGet("GetEmployee/{id:int}")]
    public async Task<EmployeeModel?> GetEmployeeById(int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }

    [HttpPost("CreateEmployee")]
    public async Task<EmployeeModel> CreateEmployee([FromBody] EmployeeModel employeeModel)
    {
        return await _employeeService.CreateEmployee(employeeModel);
    }

    [HttpPut("UpdateEmployee")]
    public async Task UpdateEmployee([FromBody] EmployeeModel employeeModel)
    {
        await _employeeService.UpdateEmployee(employeeModel);
    }

    [HttpDelete("DeleteEmployee/{id:int}")]
    public async Task<bool> DeleteEmployee(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}