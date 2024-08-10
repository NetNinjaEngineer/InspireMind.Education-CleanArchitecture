namespace InspireMind.Education.Domain.Entities;

public class InstructorCourse
{
    public Guid InstructorId { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public Instructor? Instructor { get; set; }
}
