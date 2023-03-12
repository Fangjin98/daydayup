using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DayDayUp.Models;
using LiteDB;

namespace DayDayUp.Services
{
    public interface IDataAccess
    {
        Task<List<Todo>> GetDataAsync();

        List<Todo> GetData();

        void AddDataAsync(Todo item);

        Task DeleteDataAsync(Todo item);

        Task UpdateDataAsync(Todo item);
    }

    public class DataAccess : IDataAccess
    {

        public DataAccess(MyTaskContext context)
        {
            db = context;
            db.Database.EnsureCreated();
        }

        public async void AddDataAsync(Todo item)
        {
            db.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task DeleteDataAsync(Todo item)
        {
            try
            {
                db.Remove(item);
                await db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }
        }

        public List<Todo> GetData()
        {
            return db.myTasks.ToList();
        }

        public Task<List<Todo>> GetDataAsync()
        {
            return Task.FromResult(db.myTasks.ToList());
        }

        public async Task UpdateDataAsync(Todo item)
        {
            
           db.Update(item);
           await db.SaveChangesAsync();
        }

        private MyTaskContext db;
    }

    public class LiteDbDataAccess : IDataAccess
    {
        public LiteDbDataAccess()
        {
            name = "MyTodos";

            var filePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, name);
            db = new LiteDatabase(filePath);
        }

        public async void AddDataAsync(Todo item)
        {
            var collection = db.GetCollection<Todo>();
            collection.Insert(item);
        }

        public async Task DeleteDataAsync(Todo item)
        {
            var collection = db.GetCollection<Todo>();
            collection.Delete(item.Id);
        }

        public List<Todo> GetData()
        {
            return db.GetCollection<Todo>().FindAll().ToList();
        }

        public Task<List<Todo>> GetDataAsync()
        {
            return Task.FromResult(db.GetCollection<Todo>().FindAll().ToList());
        }

        public async Task UpdateDataAsync(Todo item)
        {
            var collection = db.GetCollection<Todo>();
            collection.Update(item);
        }

        private LiteDatabase db;

        private string name;

    }
}
