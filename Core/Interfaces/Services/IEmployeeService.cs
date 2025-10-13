namespace Core.Interfaces.Services;

/// <summary>
/// services get called from the controller. they should contain the business logic and interact with repositories
/// </summary>
public interface IEmployeeService
{
    Task<EmployeeModel?> GetEmployeeById(int id);
    Task<IEnumerable<EmployeeModel>> GetAllEmployees();
    Task<EmployeeModel> CreateEmployee(EmployeeModel employee);
    Task UpdateEmployee(EmployeeModel employee);
    Task<bool> DeleteEmployee(int id);
}