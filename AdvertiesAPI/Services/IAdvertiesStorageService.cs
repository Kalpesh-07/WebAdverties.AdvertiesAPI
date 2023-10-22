using AdvertiesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvertiesAPI.Services
{
    public interface IAdvertiesStorageService
    {
        Task<string> AddAsync(AdvertiesModel model);
        Task ConfirmAsync(ConfirmAdvertiesModel model);
        Task<AdvertiesModel> GetByIdAsync(string id);
        Task<bool> CheckHealthAsync();
        Task<List<AdvertiesModel>> GetAllAsync();
    }
}
