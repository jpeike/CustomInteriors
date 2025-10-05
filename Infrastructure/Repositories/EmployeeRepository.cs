using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Employee?> GetEmployeeById(int id)
    {
        return await _dbContext.Employees.FindAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        // set to 0 so ef core treats it as a new row
        employee.EmployeeId = 0;
        
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();
        return employee;
    }

    public async Task UpdateEmployee(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        Employee? employee = await _dbContext.Employees.FindAsync(id);
        if (employee != null)
        {
            _dbContext.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}