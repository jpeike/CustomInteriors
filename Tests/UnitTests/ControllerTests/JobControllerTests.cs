using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class JobControllerTests
{
    private JobModel Job { get; } = new()
    {
        JobId = 1,
        CustomerId = 1,
        StartDate = DateTime.Now,
        EndDate = DateTime.Now,
        JobDescription = "Job Description",
        Status = "Status",
    };

    private readonly Mock<IJobService> _service;
    private readonly JobController _controller;

    public JobControllerTests()
    {
        _service = new Mock<IJobService>();
        _controller = new JobController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic job
        _service.Setup(s => s.GetAllJobs())
            .ReturnsAsync(new List<JobModel>());

        var result = await _controller.GetAllJobs();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJobById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetJobById(It.IsAny<int>()))
            .ReturnsAsync(Job);

        var result = await _controller.GetJobById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJobById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetJobById(It.IsAny<int>()))
            .ReturnsAsync(Job);

        var result = await _controller.GetJobById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateJob_ReturnsOk()
    {
        _service.Setup(s => s.CreateJob(It.IsAny<JobModel>()))
            .ReturnsAsync(Job);

        var result = await _controller.CreateJob(Job);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateJob_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateJob(It.IsAny<JobModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateJob(Job);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteJob_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteJob(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteJob(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteJob_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteJob(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteJob(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteJob_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteJob(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteJob(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}