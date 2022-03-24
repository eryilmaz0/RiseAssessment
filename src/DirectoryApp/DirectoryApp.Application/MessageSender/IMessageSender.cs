namespace DirectoryApp.Application.MessageSender;

public interface IMessageSender
{
    public Task<bool> SendGenerateReportMessage(object @object);
}