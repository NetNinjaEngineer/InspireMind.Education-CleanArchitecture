using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.DTOs.Course;
using InspireMind.Education.Application.DTOs.Topic;
using InspireMind.Education.Application.Specifications;
using InspireMind.Education.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InspireMind.Education.Persistence.Repos;
public sealed class TopicRepository(
    ApplicationDbContext context) : GenericRepository<Topic>(context), ITopicRepository
{
    public async Task<TopicWithRelatedCoursesDto?> GetTopicWithRelatedCourses(Guid topicId)
    {
        var specification = new GetAllTopicsWithCoursesSpecification();
        var query = await _context.Topics
            .Where(x => x.Id == topicId)
            .Select(x => new TopicWithRelatedCoursesDto
            {
                Id = x.Id,
                TopicName = x.TopicName,
                Courses = x.Courses.Select(c => new CourseForListDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Duration = c.Duration
                })
            }).FirstOrDefaultAsync();

        return query;
    }
}
