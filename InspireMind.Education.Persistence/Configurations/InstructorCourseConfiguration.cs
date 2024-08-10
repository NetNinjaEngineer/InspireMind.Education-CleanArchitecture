using InspireMind.Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Persistence.Configurations;

internal sealed class InstructorCourseConfiguration : IEntityTypeConfiguration<InstructorCourse>
{
    public void Configure(EntityTypeBuilder<InstructorCourse> builder)
    {
        builder.HasKey(x => new { x.InstructorId, x.CourseId });
        builder.ToTable("InstructorCourses");
    }
}
