using InspireMind.Education.Domain.Entities;
using InspireMind.Education.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduConnect.Persistence.Configurations;

internal sealed class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.TopicName)
            .HasColumnType("varchar").HasMaxLength(50)
            .IsRequired();

        builder.HasData(DatabaseHelper.GetTopics());

        builder.ToTable("Topics");
    }
}
