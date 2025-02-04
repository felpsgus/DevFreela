using DevFreela.Application.Projects.Commands.InsertProject;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;

namespace DevFreela.Test.Unit.Application.Projects;

public class InsertProjectHandlerTest
{
    private const long ProjectId = 1;

    [Fact]
    public async Task ShouldReturnIdWhenProjectIsCreated()
    {
        // Arrange
        var projectRepository = new Mock<IProjectRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        projectRepository
            .Setup(pr => pr.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .Callback((Project project, CancellationToken cancellationToken) => project.Id = ProjectId)
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var insertProjectHandler = new InsertProjectHandler(projectRepository.Object, unitOfWork.Object);
        var insertProjectCommand = new InsertProjectCommand("New Project", "Description", 1, 2, 10000.0m);

        // Act
        var result = await insertProjectHandler.Handle(insertProjectCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(ProjectId);
    }
}