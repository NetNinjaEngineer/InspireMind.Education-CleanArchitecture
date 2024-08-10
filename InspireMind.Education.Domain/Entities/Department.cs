using InspireMind.Education.Domain.Entities.Common;

namespace InspireMind.Education.Domain.Entities;

public class Department : BaseEntity
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public DateOnly? ManageDate { get; set; }
    public Guid? InstructorManagerId { get; set; }
    public Instructor? InstructorManager { get; set; }
    public ICollection<Student> Students { get; set; } = [];
    public ICollection<Instructor> Instructors { get; set; } = []; // one - many

}
