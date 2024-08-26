using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Attributes;
using InspireMind.Education.Application.Features.Courses.DTOs;
using InspireMind.Education.Application.Features.Courses.Requests.Commands;
using InspireMind.Education.Application.Features.Courses.Requests.Queries;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InspireMind.Education.Api.Controllers
{
    /// <summary>
    /// Manages operations related to courses.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for creating, updating, deleting, and retrieving course data.
    /// </remarks>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : AppControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoursesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used for sending commands and queries.</param>
        public CoursesController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves a list of courses along with their topics, with pagination.
        /// </summary>
        /// <param name="parameters">The parameters for filtering and pagination.</param>
        /// <returns>A paginated list of courses with their topics.</returns>
        /// <response code="200">Returns a paginated list of courses with topics.</response>
        [HttpGet]
        [Route("CoursesWithTopics")]
        [ProducesResponseType(typeof(Pagination<CourseDto>), StatusCodes.Status200OK)]
        [DistributedCached(300)]
        public async Task<ActionResult<IReadOnlyList<CourseDto>>> GetCoursesWithTopics([FromQuery] CourseRequestParameters parameters)
        {
            var pagedResult = await _mediator.Send(new GetCoursesWithTopicsQuery() { Parameters = parameters });
            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.Value.MetaData));
            return Ok(pagedResult.Value);
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <param name="model">The model containing course details to be created.</param>
        /// <returns>The details of the created course.</returns>
        /// <response code="201">Returns the created course.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CourseForListDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<CourseForListDto>> CreateNewCourse([FromBody] CourseForCreateDto model)
        {
            var course = await _mediator.Send(new CreateCourseCommand { Course = model });
            return Accepted(course.Value);
        }

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        /// <param name="id">The unique identifier of the course to be updated.</param>
        /// <param name="updateModel">The model containing updated course information.</param>
        /// <returns>No content if the update was successful.</returns>
        /// <response code="204">Indicates that the course was successfully updated.</response>
        /// <response code="404">If the course to be updated is not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> UpdateTopic([FromRoute] Guid id, CourseForUpdateDto updateModel)
        {
            await _mediator.Send(new UpdateCourseCommand(id, updateModel));
            return NoContent();
        }

        /// <summary>
        /// Deletes a course by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the course to be deleted.</param>
        /// <returns>No content if the delete was successful.</returns>
        /// <response code="204">Indicates that the course was successfully deleted.</response>
        /// <response code="404">If the course to be deleted is not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> DeleteCourse([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteCourseCommand(id));
            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of all courses without pagination.
        /// </summary>
        /// <returns>A list of all courses.</returns>
        [HttpGet]
        [Route("getAllCourses")]
        [ProducesResponseType(typeof(IReadOnlyList<CourseForListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<CourseForListDto>>> GetAllCoursesWithoutPagination()
            => Ok(await _mediator.Send(new GetCoursesListQuery()));
    }
}
