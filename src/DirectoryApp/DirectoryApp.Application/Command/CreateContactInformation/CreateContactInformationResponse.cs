namespace DirectoryApp.Application.Command.CreateContactInformation;

public class CreateContactInformationResponse
{
    public bool IsSuccess { get; set; }
    public Guid CreatedContactInformationId { get; set; }
}