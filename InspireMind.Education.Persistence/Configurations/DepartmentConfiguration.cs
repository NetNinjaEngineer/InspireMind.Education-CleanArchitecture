using InspireMind.Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Persistence.Configurations;

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("varchar").HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Location)
            .HasColumnType("varchar").HasMaxLength(50)
            .IsRequired(false);

        builder.Property(d => d.ManageDate)
            .HasColumnType("date")
            .IsRequired(false);

        builder.HasOne(x => x.InstructorManager)
            .WithOne()
            .HasForeignKey<Department>(x => x.InstructorManagerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable("Departments");
    }
}
