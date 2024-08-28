using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Departments.DTOs;
using MediatR;

namespace InspireMind.Education.Application.Features.Departments.Requests.Commands;
public sealed class CreateDepartmentCommand : IRequest<Result<Guid>>
{
    public DepartmentForCreateDto Department { get; set; } = null!;
}
