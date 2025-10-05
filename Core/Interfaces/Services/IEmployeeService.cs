namespace Core;

public interface IEmployeeService
{
    Task<EmployeeModel?> GetEmployeeById(int id);
    Task<IEnumerable<EmployeeModel>> GetAllEmployees();
    Task<EmployeeModel> AddEmployee(Employee employee);
    Task UpdateEmployee(EmployeeModel employee);
    Task<bool> DeleteEmployee(int id);
}