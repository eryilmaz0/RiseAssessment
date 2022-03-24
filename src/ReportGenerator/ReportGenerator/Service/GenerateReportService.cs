using ReportGenerator.Context;

namespace ReportGenerator.Service;

public class GenerateReportService
{
    private readonly ReportGeneratorContext _context;

    public GenerateReportService(ReportGeneratorContext context)
    {
        _context = context;
    }

    public bool CreateReport()
    {
        return true;
    }
}