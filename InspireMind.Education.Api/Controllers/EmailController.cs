using InspireMind.Education.Api.Base;
using InspireMind.Education.Application.Bases;
using InspireMind.Education.Application.Features.Emails.Requests.Commands;
using InspireMind.Education.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InspireMind.Education.Api.Controllers
{
    /// <summary>
    /// Manages the process of sending emails.
    /// </summary>
    /// <remarks>
    /// This controller provides an endpoint for sending emails by providing the necessary email parameters.
    /// </remarks>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : AppControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance used for sending commands and queries.</param>
        public EmailController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Sends an email based on the provided email parameters.
        /// </summary>
        /// <param name="email">The email request body containing the recipient's email address, subject, and body of the email.</param>
        /// <returns>A boolean value indicating whether the email was successfully sent or not.</returns>
        /// <response code="200">Returns a result indicating if the email was successfully sent.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        public async Task<ActionResult<Result<bool>>> SendEmail([FromBody] Email email)
        {
            var result = await _mediator.Send(new SendEmailCommand { EmailRequest = email });
            return CustomResult(result);
        }
    }
}
