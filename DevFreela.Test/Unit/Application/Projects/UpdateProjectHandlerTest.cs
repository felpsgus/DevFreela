using DevFreela.Application.Projects.Commands.UpdateProject;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Projects;

public class UpdateProjectHandlerTest
{
    private const long ProjectId = 1;

    [Fact]
    public async Task ShouldUpdateProjectWhenProjectIsUpdated()
    {
        // Arrange
        var projectRepository = new Mock<IProjectRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var project = FakeDataHelper.GetFakeProject();

        projectRepository
            .Setup(pr => pr.GetByIdAsync(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var updateProjectHandler = new UpdateProjectHandler(projectRepository.Object, unitOfWork.Object);
        var updateProjectCommand = new UpdateProjectCommand
        {
            Id = ProjectId,
            Title = "New Title",
            Description = "Description test",
            TotalCost = 10000.0m
        };

        // Act
        var result = await updateProjectHandler.Handle(updateProjectCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        project.Title.Should().Be(updateProjectCommand.Title);
        project.Description.Should().Be(updateProjectCommand.Description);
    }
}