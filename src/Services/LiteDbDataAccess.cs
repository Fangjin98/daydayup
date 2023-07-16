using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DayDayUp.Models;
using LiteDB;

namespace DayDayUp.Services
{
    public class LiteDbDataAccess : IDataAccess
    {
        public LiteDbDataAccess()
        {
            name = "MyTodos";
            var filePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, name);
            Debug.WriteLine(filePath, "LiteDB");
            db = new LiteDatabase(filePath);
        }

        public void ExportToJson(string outputPath)
        {
            if(File.Exists(outputPath)) { File.Delete(outputPath); }
            db.Execute(string.Format("SELECT $  INTO $file('{0}') FROM Todo;", outputPath));
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
