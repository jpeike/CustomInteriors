using System.Collections.Generic;
using System.Threading.Tasks;
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
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetAllEmployees()
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _employeeService.GetAllEmployees());
    }

    [HttpGet("{id:int}", Name = "GetEmployeeById")]
    public async Task<ActionResult<EmployeeModel?>> GetEmployeeById(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _employeeService.GetEmployeeById(id));
    }

    [HttpPost("", Name = "CreateEmployee")]
    public async Task<ActionResult<EmployeeModel>> CreateEmployee([FromBody] EmployeeModel employeeModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await _employeeService.CreateEmployee(employeeModel));
    }

    [HttpPut("", Name = "UpdateEmployee")]
    public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeModel employeeModel)
    {
        if (!ModelState.IsValid) return BadRequest();
        await _employeeService.UpdateEmployee(employeeModel);
        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteEmployee")]
    public async Task<ActionResult<bool>> DeleteEmployee(int id)
    {
        if (id <= 0) return BadRequest();
        return Ok(await _employeeService.DeleteEmployee(id));
    }
}