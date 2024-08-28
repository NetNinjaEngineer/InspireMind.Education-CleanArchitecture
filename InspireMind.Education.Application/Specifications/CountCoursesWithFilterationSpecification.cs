using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Domain.Entities;

namespace InspireMind.Education.Application.Specifications;
public sealed class CountCoursesWithFilterationSpecification(CourseRequestParameters parameters)
    : BaseSpecification<Course>(c =>
        string.IsNullOrEmpty(parameters.SearchTerm) ||
        c.CourseName!.ToLower()!.Contains(parameters.SearchTerm) ||
        c.Topic!.TopicName!.ToLower().Contains(parameters.SearchTerm)
        && c.TopicId!.ToString() == parameters.TopicId);
