using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeById(int id);
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> AddEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(int id);
}