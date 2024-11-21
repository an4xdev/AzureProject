using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Project.Shared.DoNotGit;

namespace Project.API.Services.Topic;

public class TopicService : ITopicService
{
    public async Task SendMessage(string message)
    {
        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        var client = new ServiceBusClient(DontCommit.TopicKey, clientOptions);
        var sender = client.CreateSender("testtopic");
        var msg = new ServiceBusMessage(message);
        await sender.SendMessageAsync(msg);
    }

    public async Task CreateSubscription(string subName)
    {
        var mixedSubName = subName.Replace("@", "__");
        var adminClient = new ServiceBusAdministrationClient(DontCommit.TopicKey);
        await adminClient.CreateSubscriptionAsync("testtopic", mixedSubName);
    }
}

public interface ITopicService
{
    public Task SendMessage(string message);

    public Task CreateSubscription(string subName);
}