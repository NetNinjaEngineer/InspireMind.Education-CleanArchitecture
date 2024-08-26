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

namespace InspireMind.Education.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TopicsController(IMediator mediator) : AppControllerBase(mediator)
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TopicDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TopicDto>>> GetAllTopics()
    {
        Result<IReadOnlyList<TopicDto>> topicsResult = await _mediator.Send(new GetAllTopicsQuery());
        return Ok(topicsResult.Value);
    }

    [HttpGet("{id:guid}", Name = "GetTopicByGuidId")]
    [ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TopicDto>> GetTopic([FromRoute] Guid id)
    {
        Result<TopicDto> result = await _mediator.Send(new GetSingleTopicQuery(id));
        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TopicDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TopicDto>> CreateNewTopic([FromBody] TopicForCreationDto model)
    {
        var createTopicCommand = new CreateTopicCommand { Topic = model };
        Result<TopicDto> createTopicResult = await _mediator.Send(createTopicCommand);
        return CreatedAtRoute("GetTopicByGuidId", new { id = createTopicResult.Value.Id }, createTopicResult.Value);
    }

    [HttpGet]
    [Route("pagination")]
    [ProducesResponseType(typeof(Pagination<TopicDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Pagination<TopicDto>>> GetPaginatedTopics(
        [FromQuery] TopicRequestParams topicRequestParams)
    {
        Result<Pagination<TopicDto>> pagedResult = await _mediator.Send(
            new GetAllTopicsWithParamsQuery { TopicRequestParams = topicRequestParams });

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.Value.MetaData));

        return Ok(pagedResult.Value);
    }

    [HttpGet]
    [Route("topicWithRelatedCourses/{topicId:guid}")]
    [ProducesResponseType(typeof(TopicWithRelatedCoursesDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TopicWithRelatedCoursesDto>> TopicWithRelatedCourses([FromRoute] Guid topicId)
    {
        var result = await _mediator.Send(new GetTopicWithRelatedCoursesQuery() { TopicId = topicId });
        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> UpdateTopic(Guid id, TopicForUpdateDto updateModel)
    {
        await _mediator.Send(new UpdateTopicCommand(id, updateModel));
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Unit>> DeleteTopic([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteTopicCommand(id));
        return NoContent();
    }
}