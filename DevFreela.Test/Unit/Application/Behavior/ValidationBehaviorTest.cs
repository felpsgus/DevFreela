using DevFreela.Application.Abstractions;
using DevFreela.Application.Abstractions.Behaviors;
using DevFreela.Test.Fakes;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DevFreela.Test.Unit.Application.Behavior;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task Should_ReturnValidationResult_When_ValidationFails()
    {
        // Arrange
        var validators = new List<IValidator<TestCommand>>
        {
            new TestCommandValidator()
        };

        var behavior = new ValidationBehavior<TestCommand, Result>(validators);

        var request = new TestCommand { Name = "" };
        var validationFailures = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        };

        var mockNext = new Mock<RequestHandlerDelegate<Result>>();
        mockNext.Setup(x => x()).ReturnsAsync(Result.Success());

        // Act
        var result = await behavior.Handle(request, mockNext.Object, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.First().Message.Should().Be("Name is required");
        mockNext.Verify(x => x(), Times.Never);
    }

    [Fact]
    public async Task Should_CallNext_When_ValidationSucceeds()
    {
        // Arrange
        var validators = new List<IValidator<TestCommand>>
        {
            new TestCommandValidator()
        };

        var behavior = new ValidationBehavior<TestCommand, Result>(validators);

        var request = new TestCommand { Name = "Valid Name" };
        var mockNext = new Mock<RequestHandlerDelegate<Result>>();
        mockNext.Setup(x => x()).ReturnsAsync(Result.Success());

        // Act
        var result = await behavior.Handle(request, mockNext.Object, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        mockNext.Verify(x => x(), Times.Once);
    }
}

// Exemplo de comando e validador para teste