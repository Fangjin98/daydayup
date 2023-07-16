using DayDayUp.Models;
using System.Collections.Generic;
using Windows.Storage;
using System.Threading.Tasks;

namespace DayDayUp.Services
{
    public interface IDataAccess
    {
        Task<List<Todo>> GetDataAsync();

        List<Todo> GetData();

        void AddDataAsync(Todo item);

        Task DeleteDataAsync(Todo item);

        Task UpdateDataAsync(Todo item);

        void Export(string path);

        void Import(StorageFile file);
    }
}
