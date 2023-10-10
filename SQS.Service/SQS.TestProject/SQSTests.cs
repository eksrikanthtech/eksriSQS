using Amazon.SQS;
using Moq;
using SQS.Service.Interface;
using Xunit;
namespace SQS.TestProject
{
    public class SQSTests
    {
        private string _queueUrl = "";
        private SqsService _sqsService;

        public SQSTests()
        {

            // Arrange
            var mockSqsClient = new Mock<IAmazonSQS>();
            _sqsService = new SqsService(mockSqsClient.Object, _queueUrl);
        }

        [Fact]
        public async Task SendMessageToQueue_ShouldSucceed()
        {

            // Act
            var result = await _sqsService.SendMessageToQueue<string>("Test Message");

            // Assert
            Assert.True(string.IsNullOrEmpty(result)); // Add your assertions here
        }

        [Fact]
        public async Task ReceiveMessagesAsync_ValidInput_ReturnsMessages()
        {
            // Act
            var maxMessages = 5;
            var messages = await _sqsService.ReceiveMessagesAsync(maxMessages);

            // Assert
            Assert.NotNull(messages);
            Assert.NotEmpty(messages);
            Assert.True(messages.Count <= maxMessages);
        }
    }
}