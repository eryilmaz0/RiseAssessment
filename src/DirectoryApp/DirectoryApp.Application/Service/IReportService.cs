using DirectoryApp.Application.Command.GenerateReport;
using MediatR;

namespace DirectoryApp.Application.Service;

public interface IReportService : IRequestHandler<GenerateReportCommand, GenerateReportResponse>
{
    
}