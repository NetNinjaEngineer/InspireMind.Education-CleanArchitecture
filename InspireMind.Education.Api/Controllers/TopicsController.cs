using Asp.Versioning;
using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Abstractions;
using InspireMind.Education.Application.Features.Topics.DTOs;
using InspireMind.Education.Application.Features.Topics.Requests.Commands;
using InspireMind.Education.Application.Features.Topics.Requests.Queries;
using InspireMind.Education.Application.RequestParams;
using InspireMind.Education.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InspireMind.Education.Api.Controllers
{
    /// <summary>
    /// Manages operations related to topics.
    /// </summary>
    /// <remarks>
    /// This controller handles creating, updating, deleting, and retrieving topic information.
    /// </remarks>
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/topics")]
    [ApiController]
    public class TopicsController : AppControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TopicsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used for sending commands and queries.</param>
        public TopicsController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves a list of all topics.
        /// </summary>
        /// <returns>A list of all topics.</returns>
        /// <response code="200">Returns a list of topics.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<TopicDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<TopicDto>>> GetAllTopics()
        {
            Result<IReadOnlyList<TopicDto>> topicsResult = await _mediator.Send(new GetAllTopicsQuery());
            return Ok(topicsResult.Value);
        }

        /// <summary>
        /// Retrieves a topic by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the topic.</param>
        /// <returns>The details of the specified topic.</returns>
        /// <response code="200">Returns the details of the topic.</response>
        /// <response code="404">If the topic is not found.</response>
        [HttpGet("{id:guid}", Name = "GetTopicByGuidId")]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TopicDto>> GetTopic([FromRoute] Guid id)
        {
            Result<TopicDto> result = await _mediator.Send(new GetSingleTopicQuery(id));
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        /// <summary>
        /// Creates a new topic.
        /// </summary>
        /// <param name="model">The model containing the details of the topic to be created.</param>
        /// <returns>The details of the created topic.</returns>
        /// <response code="201">Returns the created topic.</response>
        [HttpPost]
        [ProducesResponseType(typeof(TopicDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<TopicDto>> CreateNewTopic([FromBody] TopicForCreationDto model)
        {
            var createTopicCommand = new CreateTopicCommand { Topic = model };
            Result<TopicDto> createTopicResult = await _mediator.Send(createTopicCommand);
            return CreatedAtRoute("GetTopicByGuidId", new { id = createTopicResult.Value.Id }, createTopicResult.Value);
        }

        /// <summary>
        /// Retrieves a paginated list of topics.
        /// </summary>
        /// <param name="topicRequestParams">Parameters for filtering and pagination.</param>
        /// <returns>A paginated list of topics.</returns>
        /// <response code="200">Returns a paginated list of topics.</response>
        [HttpGet("pagination")]
        [ProducesResponseType(typeof(Pagination<TopicDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<TopicDto>>> GetPaginatedTopics(
            [FromQuery] TopicRequestParams topicRequestParams)
        {
            Result<Pagination<TopicDto>> pagedResult = await _mediator.Send(
                new GetAllTopicsWithParamsQuery { TopicRequestParams = topicRequestParams });

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.Value.MetaData));

            return Ok(pagedResult.Value);
        }

        /// <summary>
        /// Retrieves a topic along with related courses by its unique identifier.
        /// </summary>
        /// <param name="topicId">The unique identifier of the topic.</param>
        /// <returns>The topic along with related courses.</returns>
        /// <response code="200">Returns the topic along with related courses.</response>
        [HttpGet("topicWithRelatedCourses/{topicId:guid}")]
        [ProducesResponseType(typeof(TopicWithRelatedCoursesDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TopicWithRelatedCoursesDto>> TopicWithRelatedCourses([FromRoute] Guid topicId)
        {
            var result = await _mediator.Send(new GetTopicWithRelatedCoursesQuery() { TopicId = topicId });
            return Ok(result.Value);
        }

        /// <summary>
        /// Updates an existing topic.
        /// </summary>
        /// <param name="id">The unique identifier of the topic to be updated.</param>
        /// <param name="updateModel">The model containing updated topic information.</param>
        /// <returns>No content if the update was successful.</returns>
        /// <response code="204">Indicates that the topic was successfully updated.</response>
        /// <response code="404">If the topic to be updated is not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> UpdateTopic(Guid id, TopicForUpdateDto updateModel)
        {
            await _mediator.Send(new UpdateTopicCommand(id, updateModel));
            return NoContent();
        }

        /// <summary>
        /// Deletes a topic by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the topic to be deleted.</param>
        /// <returns>No content if the delete was successful.</returns>
        /// <response code="204">Indicates that the topic was successfully deleted.</response>
        /// <response code="404">If the topic to be deleted is not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> DeleteTopic([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteTopicCommand(id));
            return NoContent();
        }
    }
}