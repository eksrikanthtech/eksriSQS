using System;
using System.Collections.Generic;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.SQS;
using Amazon.SQS.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

public class SQSLambda
{
    private readonly IAmazonSQS _sqsClient = new AmazonSQSClient();

    public void FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
        foreach (var record in sqsEvent.Records)
        {
            try
            {
                string messageBody = record.Body;
                Console.WriteLine($"Received message: {messageBody}");

                // Process the message here

                // If processing is successful, delete the message from the queue
                DeleteMessageRequest deleteRequest = new DeleteMessageRequest
                {
                    QueueUrl = "YOUR_QUEUE_URL",
                    ReceiptHandle = record.ReceiptHandle
                };
                _sqsClient.DeleteMessageAsync(deleteRequest).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing message: {ex.Message}");
            }
        }
    }
}
