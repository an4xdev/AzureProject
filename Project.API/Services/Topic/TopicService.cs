using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace Project.API.Services.Topic;

public class TopicService : ITopicService
{
    private readonly string _topicKey = Environment.GetEnvironmentVariable("TOPIC_KEY") ?? "";
    private readonly string _topicName = Environment.GetEnvironmentVariable("TOPIC_NAME") ?? "";
    public async Task SendMessage(string message)
    {
        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        var client = new ServiceBusClient(_topicKey, clientOptions);
        var sender = client.CreateSender(_topicName);
        var msg = new ServiceBusMessage(message);
        await sender.SendMessageAsync(msg);
    }

    public async Task CreateSubscription(string subName)
    {
        var mixedSubName = subName.Replace("@", "__");
        var adminClient = new ServiceBusAdministrationClient(_topicKey);
        await adminClient.CreateSubscriptionAsync(_topicName, mixedSubName);
    }
}

public interface ITopicService
{
    public Task SendMessage(string message);

    public Task CreateSubscription(string subName);
}