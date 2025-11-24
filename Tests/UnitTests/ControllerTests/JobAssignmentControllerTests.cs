using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class JobAssignmentControllerTests
{
    private JobAssignmentModel JobAssignment { get; } = new()
    {
        JobId = 1,
        JobAssignmentId = 1,
        AssignmentDate = DateTime.Now,
        RoleOnJob = "Role On Job",
    };

    private readonly Mock<IJobAssignmentService> _service;
    private readonly JobAssignmentController _controller;

    public JobAssignmentControllerTests()
    {
        _service = new Mock<IJobAssignmentService>();
        _controller = new JobAssignmentController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic jobAssignment
        _service.Setup(s => s.GetAllJobAssignments())
            .ReturnsAsync(new List<JobAssignmentModel>());

        var result = await _controller.GetAllJobAssignments();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJobAssignmentById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetJobAssignmentById(It.IsAny<int>()))
            .ReturnsAsync(JobAssignment);

        var result = await _controller.GetJobAssignmentById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJobAssignmentById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetJobAssignmentById(It.IsAny<int>()))
            .ReturnsAsync(JobAssignment);

        var result = await _controller.GetJobAssignmentById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateJobAssignment_ReturnsOk()
    {
        _service.Setup(s => s.CreateJobAssignment(It.IsAny<JobAssignmentModel>()))
            .ReturnsAsync(JobAssignment);

        var result = await _controller.CreateJobAssignment(JobAssignment);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateJobAssignment_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateJobAssignment(It.IsAny<JobAssignmentModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateJobAssignment(JobAssignment);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteJobAssignment_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteJobAssignment(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteJobAssignment(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteJobAssignment_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteJobAssignment(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteJobAssignment(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteJobAssignment_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteJobAssignment(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteJobAssignment(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}