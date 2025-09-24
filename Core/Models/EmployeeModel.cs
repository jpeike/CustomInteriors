namespace Core.Models;

/// <summary>
/// model used for the front end. Any info not to be used for the front end should be omitted.
/// </summary>
public class EmployeeModel
{
    public required int EmployeeId { get; set; }
    public required int AccountId { get; set; }
    public required int EmailId { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
}