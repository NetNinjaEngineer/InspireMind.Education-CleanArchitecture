using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.DTOs.Course;
using InspireMind.Education.Application.Features.Courses.Requests.Commands;
using InspireMind.Education.Application.Features.Courses.Requests.Queries;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InspireMind.Education.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : AppControllerBase
{
    public CoursesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("CoursesWithTopics")]
    [ProducesResponseType(typeof(Pagination<CourseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetCoursesWithTopics([FromQuery] CourseRequestParameters parameters)
    {
        var pagedResult = await _mediator.Send(new GetCoursesWithTopicsQuery() { Parameters = parameters });
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.Value.MetaData));
        return Ok(pagedResult.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CourseForListDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CourseForListDto>> CreateNewCourse([FromBody] CourseForCreateDto model)
    {
        var course = await _mediator.Send(new CreateCourseCommand { Course = model });
        //return CreatedAtRoute("GetCourseByGuidId", new { id = course.Id }, course);
        return Accepted(course.Value);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> UpdateTopic([FromRoute] Guid id, CourseForUpdateDto updateModel)
    {
        await _mediator.Send(new UpdateCourseCommand(id, updateModel));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> DeleteCourse([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteCourseCommand(id));
        return NoContent();
    }

    [HttpGet]
    [Route("getAllCourses")]
    public async Task<ActionResult<IReadOnlyList<CourseForListDto>>> GetAllCoursesWithoutPagination()
        => Ok(await _mediator.Send(new GetCoursesListQuery()));


}
