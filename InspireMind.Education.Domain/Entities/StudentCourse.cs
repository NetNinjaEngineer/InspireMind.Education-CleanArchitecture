namespace InspireMind.Education.Domain.Entities;

public class StudentCourse
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public int? Grade { get; set; }
    public Course? Course { get; set; }
    public Student? Student { get; set; }
}
