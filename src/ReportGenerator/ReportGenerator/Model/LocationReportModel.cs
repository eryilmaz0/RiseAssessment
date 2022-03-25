namespace ReportGenerator.Model
{
    public class LocationReportModel
    {
        public string Location { get; set; }
        public int ActiveLocationCount { get; set; }
        public int LocatedActiveUser { get; set; }
        public int LocatedActivePhoneNumber { get; set; }

    }
}
