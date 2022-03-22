using DirectoryApp.Application.Command.GenerateReport;
using DirectoryApp.Application.MessageSender;

namespace DirectoryApp.Application.Service;

public class ReportService : IReportService
{
    private readonly IMessageSender _messageSender;

    public ReportService(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task<GenerateReportResponse> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
    {
        await _messageSender.SendGenerateReportMessage();
        return new() { IsSuccess = true, ResultMessage = "Location Report Started to be Created." };
    }
}