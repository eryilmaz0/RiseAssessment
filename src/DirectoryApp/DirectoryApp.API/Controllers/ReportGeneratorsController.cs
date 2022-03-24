using DirectoryApp.API.ApiResponse;
using DirectoryApp.Application.Command.GenerateReport;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DirectoryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportGeneratorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportGeneratorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport()
        {
            var result = await _mediator.Send(new GenerateReportCommand());

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<GenerateReportResponse>(false, result.ResultMessage));

            return Ok(new ApiResponse<GenerateReportResponse>(true, result.ResultMessage, result));
        }
    }
}
