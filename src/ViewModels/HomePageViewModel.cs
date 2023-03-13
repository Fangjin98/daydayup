﻿using CommunityToolkit.Mvvm.Input;
using DayDayUp.Models;
using DayDayUp.Services;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{

    public sealed class HomePageViewModel : BaseTodoListViewModel
    {
        public IAsyncRelayCommand AddTodoCommand { get; }

        internal HomePageStrings Strings => LanguageManager.Instance.HomePage;

        public HomePageViewModel(TodoManager TodoManager): base(TodoManager)
        {
            AddTodoCommand = new AsyncRelayCommand<Todo>(AddTodoAsync);
        }

        public void SwapTodoStatus(Todo todo)
        {
            todoManager.SwitchStaus(todo);
        }

        public void Complete(Todo todo)
        {
            Todos.Remove(todo);
            todoManager.Finish(todo);

            if (SelectedTodo == todo)
            {
                SelectedTodo = null;
            }
        }

        private async Task AddTodoAsync(Todo todo)
        {
            using (await loadingLock.LockAsync())
            {
                try
                {
                    todoManager.Add(todo);
                }
                catch
                {
                    return;
                }
                Todos.Add(todo);
            }
        }

        protected override async Task LoadTodoAsync()
        {

            using (await loadingLock.LockAsync())
            {
                Todos.Clear();

                foreach (Todo item in todoManager.UnfinishedTodos)
                {
                    Todos.Add(item);
                }
            }
        }

    }
}
