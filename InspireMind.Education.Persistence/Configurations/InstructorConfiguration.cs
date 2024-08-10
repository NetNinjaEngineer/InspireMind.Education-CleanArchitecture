using InspireMind.Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Persistence.Configurations;

internal class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(50).IsRequired();

        builder.Property(x => x.Salary)
            .HasPrecision(18, 2)
            .IsRequired(false);

        builder.HasOne(x => x.Department)
            .WithMany(x => x.Instructors)
            .HasForeignKey(x => x.DepartmentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);


        builder.HasMany(i => i.Courses)
            .WithMany(c => c.Instructors)
            .UsingEntity<InstructorCourse>(
                left => left.HasOne(x => x.Course).WithMany(x => x.InstructorCourses).HasForeignKey(x => x.CourseId),
                right => right.HasOne(x => x.Instructor).WithMany(x => x.InstructorCourses).HasForeignKey(x => x.InstructorId)
            );

        builder.ToTable("Instructors");
    }
}
