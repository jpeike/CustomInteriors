namespace Core;

public class EmployeeModel
{
    public required int EmployeeId { get; set; }
    public required int AccountId { get; set; }
    public required int EmailId { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
}