using DirectoryApp.Application.MessageSender;

namespace DirectoryApp.Infrastructure.MessageSender;

public class RabbitMQMessageSender : IMessageSender
{
    public Task<bool> SendGenerateReportMessage()
    {
        throw new NotImplementedException();
    }
}