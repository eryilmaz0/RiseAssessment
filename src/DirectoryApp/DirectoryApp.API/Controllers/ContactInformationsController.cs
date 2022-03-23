using DirectoryApp.API.ApiResponse;
using DirectoryApp.Application.Command.CreateContactInformation;
using DirectoryApp.Application.Command.RemoveContactInformation;
using DirectoryApp.Application.Query.GetEnrolleeWithDetail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DirectoryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactInformationsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateContactInformation([FromBody] CreateContactInformationCommand request)
        {
            var result = await _mediator.Send(request);

            if(!result.IsSuccess)
                return BadRequest(new ApiResponse<CreateContactInformationResponse>(false, result.ResultMessage));

            return Ok(new ApiResponse<CreateContactInformationResponse>(true, result.ResultMessage, result));
        }


        [HttpDelete]
        [Route("{contactInformationId}")]
        public async Task<IActionResult> RemoveContactInformation(Guid contactInformationId)
        {
            var result = await _mediator.Send(new RemoveContactInformationCommand()
            {
                InformationId = contactInformationId
            });

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<RemoveContactInformationResponse>(false, result.ResultMessage));

            return Ok(new ApiResponse<RemoveContactInformationResponse>(true, "Contact Information Removed.", result));
        }
    }
}
