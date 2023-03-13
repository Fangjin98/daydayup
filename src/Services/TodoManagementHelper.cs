using System;
using System.Collections.Generic;
using System.Diagnostics;
using DayDayUp.Models;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace DayDayUp.Services;

public class TodoManager
{
    public List<Todo> UnfinishedTodos
    {
        get { return allTodos.FindAll(t => t.IsFinished == false); }
    }

    public List<Todo> FinishedTodos
    {
        get { return allTodos.FindAll(t => t.IsFinished == true); }
    }

    public TodoManager()
    {
        _dataAccess = Ioc.Default.GetRequiredService<IDataAccess>();
        allTodos = _dataAccess.GetData();
    }

    public void Finish(Todo item)
    {
        item.IsFinished = true;
        item.TimeStamps.Add(DateTime.Now);
        Update(item);
    }

    public void Remove(Todo item)
    {
        allTodos.Remove(item);
        _dataAccess.DeleteDataAsync(item);
    }

    public void Add(Todo item)
    {
        allTodos.Add(item);
        _dataAccess.AddDataAsync(item);
    }

    public void SwitchStaus(Todo item)
    {
        item.Status = item.Status == TodoStatus.Doing ? TodoStatus.Pause : TodoStatus.Doing;
        item.TimeStamps.Add(DateTime.Now);
        Update(item);
    }

    public async void Update(Todo item)
    {
        await _dataAccess.UpdateDataAsync(item);
    }

    public void UpdateAll()
    {
        foreach (Todo todo in allTodos)
        {
            Update(todo);
        }
    }

    private List<Todo> allTodos;

    private IDataAccess _dataAccess;
}
