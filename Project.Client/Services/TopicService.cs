namespace Project.Client.Services;

public class TopicService(string topicKey, string topicName)
{
    public string TopicKey { get; } = topicKey;
    public string TopicName { get; } = topicName;
}