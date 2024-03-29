﻿using DayDayUp.Models;
using DayDayUp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public class ArchivePageViewModel : BaseTodoListViewModel
    {
        public int AverageBias => (int) todoManager.FinishedTodos.Average(x => x.Bias);
        public int MinBias => todoManager.FinishedTodos.Max(x => x.Bias);
        public int MaxBias => todoManager.FinishedTodos.Min(x => x.Bias);

        public ArchivePageViewModel(TodoManager TodoManager) :
           base(TodoManager)
        {
        }

        public void Restore(Todo item)
        {
            item.IsFinished = false;
            item.TimeStamps.Remove(item.TimeStamps.Last());
            todoManager.Update(item);
            Todos.Remove(item);
        }

        protected override async Task LoadTodoAsync()
        {
            using (await loadingLock.LockAsync())
            {
                Todos.Clear();
                var tmp = todoManager.FinishedTodos;
                tmp.Reverse();
                foreach (Todo item in tmp)
                {
                    Todos.Add(item);
                }
            }
        }
    }
}
