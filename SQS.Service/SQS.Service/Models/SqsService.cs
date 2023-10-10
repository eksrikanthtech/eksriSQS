using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using SQS.Service.Interface;

public class SqsService : ISqsService
{
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl;

    public SqsService(IAmazonSQS sqsClient, string queueUrl)
    {
        _sqsClient = sqsClient;
        _queueUrl = queueUrl;
    }

    public async Task<string> SendMessageToQueue<T>(T messageBody)
    {
        var request = new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = JsonConvert.SerializeObject(messageBody)
        };

        var response = await _sqsClient.SendMessageAsync(request);
        return response.MessageId;
    }

    public async Task<List<Message>> ReceiveMessagesAsync(int maxMessages)
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl = _queueUrl,
            MaxNumberOfMessages = maxMessages
        };

        var response = await _sqsClient.ReceiveMessageAsync(request);
        return response.Messages;
    }

    public async Task DeleteMessageAsync(string receiptHandle)
    {
        var request = new DeleteMessageRequest
        {
            QueueUrl = _queueUrl,
            ReceiptHandle = receiptHandle
        };

        await _sqsClient.DeleteMessageAsync(request);
    }
}
