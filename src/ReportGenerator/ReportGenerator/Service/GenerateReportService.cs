using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using ReportGenerator.Context;
using ReportGenerator.Enum;
using ReportGenerator.Model;


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
        //Fetching enrollees which only have phone number or location informations
        var enrollees = _context.Enrollees
                        .Where(enrollee => enrollee.IsActive)
                        .Include(include => include.ContactInformations
                        .Where(filter => filter.IsActive && (filter.InformationType == Enum.InformationType.Location || filter.InformationType == Enum.InformationType.PhoneNumber)))
                        .ToList();


        Dictionary<string, int> existingLocations = new();

        existingLocations = _context.ContactInformations
            .Where(x => x.IsActive && x.InformationType == Enum.InformationType.Location)
            .GroupBy(x => x.Information)
            .Select(x => new KeyValuePair<string, int>(x.Key, x.Count())).ToDictionary(x => x.Key, x => x.Value);
               
                                


        List<LocationReportModel> locationReportList = new();
        foreach (var location in existingLocations)
        {
            var locatedEnrollees = enrollees.Where(x => x.ContactInformations.Any(x => x.Information == location.Key && x.IsActive)).ToList();

            locationReportList.Add(new()
            {
                Location = location.Key,
                ActiveLocationCount = location.Value,
                LocatedActiveEnrollee = locatedEnrollees.Count(),
                LocatedActivePhoneNumber = locatedEnrollees.Where(x => x.ContactInformations.Any(x => x.InformationType == InformationType.PhoneNumber)).Count()
            });
        }


        //Creating Excel
        string name = $"{DateTime.Now.ToString("dd.mm.yyyy-HH.mm")}.LocationReport.xlsx";
        var workbook = new XLWorkbook();     
        var wsDetailedData = workbook.AddWorksheet("data"); 
        wsDetailedData.Cell(1, 1).InsertTable(locationReportList.OrderByDescending(x => x.ActiveLocationCount)); 
        workbook.SaveAs($"Reports/{name}"); 

        return true;
    }

}