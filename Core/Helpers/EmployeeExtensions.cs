namespace Core;

public static class EmployeeExtensions
{
    public static EmployeeModel ToModel(this Employee employee) => new EmployeeModel
    {
        EmployeeId = employee.EmployeeId,
        AccountId = employee.AccountId,
        EmailId = employee.EmailId,
        Name = employee.Name,
        Role = employee.Role
    };

    public static IEnumerable<EmployeeModel> ToModels(this IEnumerable<Employee> employees)
    {
        return employees.Select(u => u.ToModel());
    }

    public static Employee ToEntity(this EmployeeModel model) => new Employee
    {
        EmployeeId = model.EmployeeId,
        AccountId = model.AccountId,
        EmailId = model.EmailId,
        Name = model.Name,
        Role = model.Role
    };
}