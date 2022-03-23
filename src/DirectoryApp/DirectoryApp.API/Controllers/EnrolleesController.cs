using DirectoryApp.API.ApiResponse;
using DirectoryApp.Application.Command.CreateEnrollee;
using DirectoryApp.Application.Command.RemoveEnrollee;
using DirectoryApp.Application.Command.UpdateEnrollee;
using DirectoryApp.Application.Query.GetEnrolleeWithDetail;
using DirectoryApp.Application.Query.ListEnrollees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolleesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrolleesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetEnrollees()
        {
            var result = await _mediator.Send(new ListEnrolleesQuery());
            return Ok(new ApiResponse<List<ListEnrolleesViewModel>>(true, "Enrollees listed.", result.ToList()));
        }


        [HttpGet]
        [Route("{enrolleeId}")]
        public async Task<IActionResult> GetEnrollee([FromRoute] Guid enrolleeId)
        {
            var result = await _mediator.Send(new GetEnrolleeWithDetailQuery()
            {
                EnrolleeId = enrolleeId
            });

            if (result is null)
                return BadRequest(new ApiResponse<GetEnrolleeWithDetailViewModel>(false));

            return Ok(new ApiResponse<GetEnrolleeWithDetailViewModel>(true, "Enrollee Fetched.", result));
        }


        [HttpPost]
        public async Task<IActionResult> CreateEnrollee([FromBody] CreateEnrolleeCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<CreateEnrolleeResponse>(false, result.ResultMessage));

            return Ok(new ApiResponse<CreateEnrolleeResponse>(true, result.ResultMessage, result));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEnrollee([FromBody] UpdateEnrolleeCommand request)
        {
            var result = await _mediator.Send(request);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<UpdateEnrolleeResponse>(false, result.ResultMessage));

            return Ok(new ApiResponse<UpdateEnrolleeResponse>(true, result.ResultMessage, result));
        }


        [HttpDelete]
        [Route("{enrolleeId}")]
        public async Task<IActionResult> RemoveEnrollee(Guid enrolleeId)
        {
            var result = await _mediator.Send(new RemoveEnrolleeCommand()
            {
                EnrolleeId = enrolleeId
            });

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<RemoveEnrolleeResponse>(false, result.ResultMessage));

            return Ok(new ApiResponse<RemoveEnrolleeResponse>(true, result.ResultMessage, result));
        }
    }
}
