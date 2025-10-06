using Core;

namespace Infrastructure;

public class EmployeeService :  IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<EmployeeModel?> GetEmployeeById(int id)
    {
        Employee? employee = await  _employeeRepository.GetEmployeeById(id);
        if (employee == null)
        {
            return null;
        }

        return employee.ToModel();
    }

    public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
    {
        IEnumerable<Employee> employees = await _employeeRepository.GetAllEmployees();
        return employees.ToModels();
    }

    public async Task<EmployeeModel> AddEmployee(Employee employee)
    {
        Employee toReturn = await _employeeRepository.AddEmployee(employee);
        return toReturn.ToModel();
    }

    public async Task UpdateEmployee(EmployeeModel employee)
    {
        await _employeeRepository.UpdateEmployee(employee.ToEntity());
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        return await  _employeeRepository.DeleteEmployee(id);
    }
}