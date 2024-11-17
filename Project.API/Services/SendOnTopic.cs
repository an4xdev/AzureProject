using Azure.Messaging.ServiceBus;
using Project.Shared.DoNotGit;

namespace Project.API.Services;

public class SendOnTopic : ISendOnTopic
{
    private ServiceBusClient _client;

    private ServiceBusSender _sender;


    public async Task SendMessage(string message)
    {
        if (_client == null)
        {
            return;
        }
        var msg = new ServiceBusMessage(message);
        await _sender.SendMessageAsync(msg);
    }

    public void Initialize()
    {
        if (_client == null)
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            _client = new ServiceBusClient(DontCommit.TopicKey, clientOptions);
            _sender = _client.CreateSender("testtopic");
        }
    }
}

public interface ISendOnTopic
{
    public Task SendMessage(string message);

    public void Initialize();
}