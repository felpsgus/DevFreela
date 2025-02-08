using DevFreela.Application.Abstractions.Interfaces;
using FluentValidation;
using MediatR;

namespace DevFreela.Application.Abstractions.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse> where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults =
            await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
        var failures = validationResults
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .Select(result => new Error(result.PropertyName, result.ErrorMessage))
            .ToList();

        if (failures.Count != 0)
            return CreateValidationResult(failures);

        return await next();
    }

    private static TResponse CreateValidationResult(IEnumerable<Error> errors)
    {
        if (typeof(TResponse) == typeof(Result))
            return ValidationResult.WithErrors(errors) as TResponse;

        return typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResponse).GetGenericArguments()[0])
            .GetMethod(nameof(ValidationResult<object>.WithErrors))
            .Invoke(null, new object[] { errors }) as TResponse;
    }
}