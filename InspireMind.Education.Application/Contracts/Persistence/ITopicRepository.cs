using InspireMind.Education.Application.Features.Topics.DTOs;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Contracts.Persistence;
public interface ITopicRepository : IGenericRepository<Topic>
{
    Task<TopicWithRelatedCoursesDto?> GetTopicWithRelatedCourses(Guid topicId);
}
