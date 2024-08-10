using InspireMind.Education.Domain.Entities.Common;

namespace InspireMind.Education.Domain.Entities;

public class Student : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Guid? DepartmentId { get; set; }
    public ICollection<Course> Courses { get; set; } = [];
    public ICollection<StudentCourse> StudentCourses { get; set; } = [];
    public Department? Department { get; set; }
}
