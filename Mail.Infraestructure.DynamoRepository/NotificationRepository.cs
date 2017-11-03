using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal;

namespace Mail.Infraestructure.DynamoRepository
{
    public class NotificationRepository:BaseRepository
    {
        public List<Notification> GetNotificationForSending()
        {
            var query = new QueryRequest()
            {
                TableName = "mail-sending-pagoefectivo-pe_test",
                IndexName = "State-IdTemplate-index_test",
                KeyConditionExpression = "#state = :v_State and #template < :v_Template",
                ExpressionAttributeNames = new Dictionary<string, string>() { { "#state", "State" }, { "#template", "IdTemplate" } },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() { { ":v_State", new AttributeValue() { N = StateNotifications.Send.GetHashCode().ToString() } }, { ":v_Template", new AttributeValue() { N = "4" } } },
                ScanIndexForward = true

            };

            var notifications1 = GetNotifications(query);
            query = new QueryRequest()
            {
                TableName = "mail-sending-pagoefectivo-pe_test",
                IndexName = "State-IdTemplate-index_test",
                KeyConditionExpression = "#state = :v_State and #template < :v_Template",
                ExpressionAttributeNames = new Dictionary<string, string>() { { "#state", "State" }, { "#template", "IdTemplate" } },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() { { ":v_State", new AttributeValue() { N = StateNotifications.Send.GetHashCode().ToString() } }, { ":v_Template", new AttributeValue() { N = "4" } } },
                ScanIndexForward = true

            };

            var notification2 = GetNotifications(query);
            notifications1.AddRange(notification2);
            return notifications1;

        }


        public List<Notification> GetNotificationsRemainderForSending()
        {
            var query = new QueryRequest()
            {
                TableName = "mail-sending-pagoefectivo-pe_Test",
                IndexName = "State-IdTemplate-index_Test",
                KeyConditionExpression = "#state = :v_State and #template = :v_Template",
                ExpressionAttributeNames = new Dictionary<string, string>() { { "#state", "State" }, { "#template", "IdTemplate" } },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() { { ":v_State", new AttributeValue() { N = StateNotifications.Send.GetHashCode().ToString() } }, { ":v_Template", new AttributeValue() { N = "4" } } },
                ScanIndexForward = true
            };

            return GetNotifications(query);

        }

        public List<Notification> GetNotificationForDelete()
        {

            List<Notification> notificactions = new AutoConstructedList<Notification>();
            var query = new QueryRequest()
            {
                TableName = "mail-sending-pagoefectivo-pe_Test",
                IndexName = "State-index_Test",
                KeyConditionExpression = "#sta = :v_State",
                ExpressionAttributeNames = new Dictionary<string, string>() { { "#sta", "State" } },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() { { ":v_State", new AttributeValue() { N = StateNotifications.Success.GetHashCode().ToString() } } },
                ScanIndexForward = true
            };
            return GetNotifications(query);
        }

        public List<Notification> GetFailedNotificationsForSending()
        {
            List<Notification> notifications = new AutoConstructedList<Notification>();

            var query = new QueryRequest()
            {
                TableName = "mail-sending-pagoefectivo-pe_Test",
                IndexName = "State-index_Test",
                KeyConditionExpression = "#sta = :v_State",
                ExpressionAttributeNames = new Dictionary<string, string>() { { "#sta", "State" } },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>() { { ":v_State", new AttributeValue() { N = StateNotifications.Fail.GetHashCode().ToString() } } },
                ScanIndexForward = true
            };

            return GetNotifications(query);
        }


        public Notification Parse(Dictionary<string, AttributeValue> record)
        {
            var notification = new Notification();
            foreach (var key in record.Keys)
            {
                if (key == "IdTemplate")
                    notification.IdTemplate = int.Parse(record[key].N);
                if (key == "CreationDate")
                    notification.CreationDate = DateTime.Parse(record[key].S);
                if (key == "Timer")
                    notification.Timer = int.Parse(record[key].N);
                if (key == "NextTime")
                    notification.NextTime = DateTime.Parse(record[key].S);
                if (key == "Path")
                    notification.Path = record[key].S;
                if (key == "Reminder")
                    notification.Reminder = record[key].N == "1" ? true : false;
                if (key == "IdNotification")
                    notification.IdNotification = record[key].S;
                if (key == "Fields")
                    notification.Fields = record[key].S;
                if (key == "Mail")
                    notification.Mail = record[key].S;
                if (key == "ModifyDate")
                    notification.ModifyDate = DateTime.Parse(record[key].S);
                if (key == "End")
                    notification.End = DateTime.Parse(record[key].S);
                if (key == "State")
                    notification.State = int.Parse(record[key].N);
                if (key == "Begin")
                    notification.Begin = DateTime.Parse(record[key].S);
                if (key == "Origin")
                    notification.Origin = int.Parse(record[key].N);
                if (key == "Language")
                    notification.Language = record[key].S;
                if (key == "Try")
                    notification.Try = int.Parse(record[key].N);
            }
            return notification;
        }

        private List<Notification> GetNotifications(QueryRequest query)
        {
            List<Notification> notifications = new List<Notification>();
            Task<QueryResponse> responseTask = _client.QueryAsync(query);
            QueryResponse resultQueryResponse = responseTask.Result;

            foreach (var record in resultQueryResponse.Items)
            {
                var notification = Parse(record);
                notifications.Add(notification);
            }

            return notifications;
        }
        public Notification Parse(StreamRecord record)
        {
            var notification = new Notification { IdNotification = record.Keys["IdNotification"].S };

            foreach (var field in record.NewImage)
            {
                if (field.Key == "IdTemplate")
                    notification.IdTemplate = int.Parse(field.Value.N);
                if (field.Key == "CreationDate")
                    notification.CreationDate = DateTime.Parse(field.Value.S);
                if (field.Key == "Timer")
                    notification.Timer = int.Parse(field.Value.N);
                if (field.Key == "NextTime")
                    notification.NextTime = DateTime.Parse(field.Value.S);
                if (field.Key == "Path")
                    notification.Path = field.Value.S;
                if (field.Key == "Reminder")
                    notification.Reminder = field.Value.N == "1" ? true : false;
                if (field.Key == "IdNotification")
                    notification.IdNotification = field.Value.S;
                if (field.Key == "Fields")
                    notification.Fields = field.Value.S;
                if (field.Key == "Mail")
                    notification.Mail = field.Value.S;
                if (field.Key == "ModifyDate")
                    notification.ModifyDate = DateTime.Parse(field.Value.S);
                if (field.Key == "End")
                    notification.End = DateTime.Parse(field.Value.S);
                if (field.Key == "State")
                    notification.State = int.Parse(field.Value.N);
                if (field.Key == "Begin")
                    notification.Begin = DateTime.Parse(field.Value.S);
                if (field.Key == "Origin")
                    notification.Origin = int.Parse(field.Value.N);
                if (field.Key == "Language")
                    notification.Language = field.Value.S;
                if (field.Key == "Try")
                    notification.Try = int.Parse(field.Value.N);
            }

            return notification;
        }
        enum StateNotifications
        {
            Success = 0,
            Send = 1,
            Fail = 2
        }
    }
}
