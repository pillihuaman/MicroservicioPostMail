using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
namespace Mail.Infraestructure.Notification
{
    public class BaseNotification
    {
        private readonly AmazonSimpleNotificationServiceClient _client;
        private readonly string _topicArn;


        public BaseNotification(string topicArn)
        {
            AWSCredentials credentials = new EnvironmentVariablesAWSCredentials();
            var config = new AmazonSimpleNotificationServiceConfig()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(System.Environment.GetEnvironmentVariable("REGION"))

            };
            _client = new AmazonSimpleNotificationServiceClient(config);
            _topicArn = topicArn;
        }

        public void SendMessage(string subject , string message)
        {
            Task<PublishResponse> responseTask = _client.PublishAsync(new PublishRequest
            {
                Subject = subject,
                Message = message,
                TopicArn = _topicArn

            });
            responseTask.Wait();

        }
    }
}
