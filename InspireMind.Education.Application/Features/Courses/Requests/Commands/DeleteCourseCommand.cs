using MediatR;

namespace InspireMind.Education.Application.Features.Courses.Requests.Commands;
public sealed class DeleteCourseCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; } = id;
}
