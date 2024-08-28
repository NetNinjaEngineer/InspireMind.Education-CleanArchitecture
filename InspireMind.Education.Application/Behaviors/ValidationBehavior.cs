using FluentValidation;
using MediatR;

namespace InspireMind.Education.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        ).ConfigureAwait(false);

        var validationFailures = validationResults
            .Where(v => v.Errors.Count > 0)
            .SelectMany(e => e.Errors)
            .ToList();

        if (validationFailures.Count > 0)
            throw new ValidationException(validationFailures);

        return await next();
    }
}