using AdvertiesAPI.Models;
using Amazon.DynamoDBv2.DataModel;
using System;

namespace AdvertiesAPI.Services
{
    [DynamoDBTable("Adverties")]
    public class AdvertDbModel
    {
        [DynamoDBHashKey] public string Id { get; set; }

        [DynamoDBProperty] public string Title { get; set; }

        [DynamoDBProperty] public string Description { get; set; }

        [DynamoDBProperty] public double Price { get; set; }

        [DynamoDBProperty] public DateTime CreationDateTime { get; set; }

        [DynamoDBProperty] public AdvertiesStatus Status { get; set; }

        [DynamoDBProperty] public string FilePath { get; set; }

        [DynamoDBProperty] public string UserName { get; set; }
    }
}
