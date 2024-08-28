using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.Features.Departments.Requests.Commands;
using MediatR;

namespace InspireMind.Education.Application.Features.Departments.Handlers.Commands;
public sealed class DepartmentsCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDepartmentCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
