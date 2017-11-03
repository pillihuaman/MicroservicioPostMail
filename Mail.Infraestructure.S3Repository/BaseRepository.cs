using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;

namespace Mail.Infraestructure.S3Repository
{
    public class BaseRepository
    {

        private readonly AmazonS3Client _s3Client;


        public BaseRepository()
       {
            AWSCredentials credentials = new EnvironmentVariablesAWSCredentials();

            AmazonS3Config config = new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(System.Environment.GetEnvironmentVariable("REGION"))
            };
            _s3Client = new AmazonS3Client(credentials, config);


        }
        public string GetObjectDataToString(string bucketName, string KeyName)
        {

            string responseBoby;
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = KeyName

            };
            Task<GetObjectResponse> responseTask = _s3Client.GetObjectAsync(request);
            var response = responseTask.Result;
            using (var responseStream = response.ResponseStream) 
            using (var reader = new StreamReader(responseStream))
            {
                responseBoby = reader.ReadToEnd();

            }
            return responseBoby;

        }


    }
}
