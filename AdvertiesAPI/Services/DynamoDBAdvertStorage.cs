using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AdvertiesAPI.Models;
using System.Linq;

namespace AdvertiesAPI.Services
{
    public class DynamoDBAdvertStorage : IAdvertiesStorageService
    {
        private readonly IMapper _mapper;

        public DynamoDBAdvertStorage(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> AddAsync(AdvertiesModel model)
        {
            var dbModel = _mapper.Map<AdvertDbModel>(model);

            dbModel.Id = Guid.NewGuid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertiesStatus.Pending;

            using (var client = new AmazonDynamoDBClient())
            {
                var table = await client.DescribeTableAsync("Adverties");

                using (var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }
            }

            return dbModel.Id;
        }

        public async Task<bool> CheckHealthAsync()
        {
            Console.WriteLine("Health checking...");
            using (var client = new AmazonDynamoDBClient())
            {
                var tableData = await client.DescribeTableAsync("Adverties");
                return string.Compare(tableData.Table.TableStatus, "active", true) == 0;
            }
        }

        public async Task ConfirmAsync(ConfirmAdvertiesModel model)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<AdvertDbModel>(model.ID);
                    if (record == null) throw new KeyNotFoundException($"A record with ID={model.ID} was not found.");
                    if (model.Status == AdvertiesStatus.Active)
                    {
                        record.FilePath = model.FilePath;
                        record.Status = AdvertiesStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
            }
        }

        public async Task<List<AdvertiesModel>> GetAllAsync()
        {
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    var scanResult =
                        await context.ScanAsync<AdvertDbModel>(new List<ScanCondition>()).GetNextSetAsync();
                    return scanResult.Select(item => _mapper.Map<AdvertiesModel>(item)).ToList();
                }
            }
        }

        public async Task<AdvertiesModel> GetByIdAsync(string id)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    var dbModel = await context.LoadAsync<AdvertDbModel>(id);
                    if (dbModel != null) return _mapper.Map<AdvertiesModel>(dbModel);
                }
            }

            throw new KeyNotFoundException();
        }
    }
}
