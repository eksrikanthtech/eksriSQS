using Amazon.SQS.Model;

namespace SQS.Service.Interface
{
    public interface ISqsService
    {
        Task<string> SendMessageToQueue<T>(T messageBody);
        Task<List<Message>> ReceiveMessagesAsync(int maxMessages);
        Task DeleteMessageAsync(string receiptHandle);
    }
}
