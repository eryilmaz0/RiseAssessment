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
        var workbook = new XLWorkbook();     //creates the workbook
        var wsDetailedData = workbook.AddWorksheet("data"); //creates the worksheet with sheetname 'data'
        wsDetailedData.Cell(1, 1).InsertTable(locationReportList.OrderByDescending(x => x.ActiveLocationCount)); //inserts the data to cell A1 including default column name
        workbook.SaveAs($"Reports/{name}"); //saves the workbook

        return true;
    }


    //public bool CreateReport3()
    //{
    //    string name = $"{DateTime.Now.ToString("dd.mm.yyyy-HH.mm")}.LocationReport.xlsx";
    //    var dataList = new List<LocationReportModel>()
    //    {
    //        new(){Location = "Location1", ActiveLocationCount = 100, LocatedActivePhoneNumber = 100, LocatedActiveUser = 100},
    //        new(){Location = "Location2", ActiveLocationCount = 200, LocatedActivePhoneNumber = 200, LocatedActiveUser = 200},
    //        new(){Location = "Location3", ActiveLocationCount = 300, LocatedActivePhoneNumber = 300, LocatedActiveUser = 300},
    //        new(){Location = "Location4", ActiveLocationCount = 400, LocatedActivePhoneNumber = 400, LocatedActiveUser = 400},
    //        new(){Location = "Location5", ActiveLocationCount = 500, LocatedActivePhoneNumber = 500, LocatedActiveUser = 500}
    //    };
    //    var workbook = new XLWorkbook();     //creates the workbook
    //    var wsDetailedData = workbook.AddWorksheet("data"); //creates the worksheet with sheetname 'data'
    //    wsDetailedData.Cell(1, 1).InsertTable(dataList); //inserts the data to cell A1 including default column name
    //    workbook.SaveAs($"Reports/{name}"); //saves the workbook

    //    return true;
    //}
}