using FluentValidation;
using MediatR;

namespace InspireMind.Education.Application.Behaviors;
public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
                                  RequestHandlerDelegate<TResponse> next,
                                  CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(x => x.Errors).Where(x => x != null).ToList();
            if (failures.Count != 0)
            {
                var message = failures.Select(x => string.Concat(x.PropertyName, " ", x.ErrorMessage)).FirstOrDefault();
                throw new ValidationException(message);
            }
        }

        return await next();

    }
}
