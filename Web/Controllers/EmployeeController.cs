using Core;
using Core.Entities;
using Core.Interfaces.Services;
using Core.Models;
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

    [HttpGet()]
    public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
    {
        return await _employeeService.GetAllEmployees();
    }

    [HttpGet("{id:int}")]
    public async Task<EmployeeModel?> GetEmployeeById(int id)
    {
        return await _employeeService.GetEmployeeById(id);
    }

    [HttpPost("Create")]
    public async Task<EmployeeModel> CreateEmployee([FromBody] Employee employeeModel)
    {
        return await _employeeService.AddEmployee(employeeModel);
    }

    [HttpPut("Update")]
    public async Task UpdateUser([FromBody] EmployeeModel employeeModel)
    {
        await _employeeService.UpdateEmployee(employeeModel);
    }

    [HttpDelete("Delete/{id:int}")]
    public async Task<bool> DeleteUser(int id)
    {
        return await _employeeService.DeleteEmployee(id);
    }
}