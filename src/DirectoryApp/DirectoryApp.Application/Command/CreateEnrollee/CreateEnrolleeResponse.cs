namespace DirectoryApp.Application.Command.CreateEnrollee;

public class CreateEnrolleeResponse
{
    public bool IsSuccess { get; set; }
    public string ResultMessage { get; set; }
    public Guid CreatedEnrolleeId { get; set; }
}