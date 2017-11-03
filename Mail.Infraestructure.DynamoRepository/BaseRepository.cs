using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;


namespace Mail.Infraestructure.DynamoRepository
{


    public class BaseRepository
    {
        protected readonly DynamoDBContext _Context;
        protected readonly AmazonDynamoDBClient _client;

        public BaseRepository()
        {
            AWSCredentials credential = new EnvironmentVariablesAWSCredentials();
            var config = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(System.Environment.GetEnvironmentVariable("REGION"))
            };
            _client = new AmazonDynamoDBClient(credential, config);
            _Context = new DynamoDBContext(_client);
        }

        public void InsertOrupdate<T>(T item) where T : class
        {
            Task resulTask = _Context.SaveAsync(item);
            resulTask.Wait();

        }

        public void Delete<T>(T item) where T : class
        {
            Task resulTask = _Context.DeleteAsync(item);
            resulTask.Wait();

        }
        public List<T> GetScan<T>(IEnumerable<ScanCondition> scan) where  T: class
        {
            AsyncSearch<T> search = _Context.ScanAsync<T>(scan);
            Task<List<T>> item = search.GetRemainingAsync();
            return item.Result;

        }
        public T GetItem<T>(object key) where T : class
        {
            Task<T> item = _Context.LoadAsync<T>(key);
            return item.Result;

        }

    }
}
