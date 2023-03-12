﻿using DayDayUp.Helpers;
using DayDayUp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nito.AsyncEx;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public abstract class BaseTodoListViewModel : ObservableRecipient
    {
        public IAsyncRelayCommand LoadTodoCommand { get; }

        public ObservableCollection<Todo> Todos { get; set; } = new();

        private Todo selectedTodo = new();

        public Todo SelectedTodo
        {
            get => selectedTodo;
            set => SetProperty(ref selectedTodo, value);
        }

        public BaseTodoListViewModel(TodoManagementHelper TodoManager)
        {
            todoManager = TodoManager;

            LoadTodoCommand = new AsyncRelayCommand(LoadTodoAsync);
        }

        public void Delete(Todo todo)
        {
            Todos.Remove(todo);
            todoManager.Remove(todo);

            if (SelectedTodo == todo)
            {
                SelectedTodo = null;
            }
        }

        public void Update(Todo todo)
        {
            todoManager.Update(todo);
        }

        protected abstract Task LoadTodoAsync();

        protected readonly AsyncLock loadingLock = new AsyncLock();

        protected readonly TodoManagementHelper todoManager;
    }
}
