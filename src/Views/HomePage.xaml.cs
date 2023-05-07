using CommunityToolkit.Mvvm.DependencyInjection;
using DayDayUp.Controls;
using DayDayUp.Models;
using DayDayUp.Services;
using DayDayUp.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using Windows.System;

namespace DayDayUp.Views;

public sealed partial class HomePage : Page
{

    public HomePageViewModel ViewModel => (HomePageViewModel)DataContext;

    public HomePage()
    {
        InitializeComponent();
        DataContext = Ioc.Default.GetService<HomePageViewModel>();
    }

    private void newTaskTextBoxKeyUp(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter && newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 0,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>(),
            });

            resetTaskInput();
        }
    }

    private void AlignmentMenuFlyoutItem0_Click(object sender, RoutedEventArgs e)
    {
        if (newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 30,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>(),
            });

            resetTaskInput();
        }
    }

    private void AlignmentMenuFlyoutItem1_Click(object sender, RoutedEventArgs e)
    {
        if (newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 60,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>()
            });

            resetTaskInput();
        }
    }

    private void AlignmentMenuFlyoutItem2_Click(object sender, RoutedEventArgs e)
    {
        if (newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 6 * 60,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>()
            });

            resetTaskInput();
        }
    }

    private void AlignmentMenuFlyoutItem3_Click(object sender, RoutedEventArgs e)
    {
        if (newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 12 * 60,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>()
            });

            resetTaskInput();
        }
    }

    private void AlignmentMenuFlyoutItem4_Click(object sender, RoutedEventArgs e)
    {
        if (newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 24 * 60,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>()
            });

            resetTaskInput();
        }
    }

    private void AlignmentMenuFlyoutItem5_Click(object sender, RoutedEventArgs e)
    {
        if (newTaskTextBox.Text != "")
        {
            ViewModel.AddTodoCommand.ExecuteAsync(new Todo
            {
                Name = newTaskTextBox.Text,
                ExpectedDurationMins = 0,
                IsFinished = false,
                Status = TodoStatus.Pause,
                CreationDate = DateTime.Now,
                TimeStamps = new List<DateTime>()
            });

            resetTaskInput();
        }
    }


    private void resetTaskInput()
    {
        newTaskTextBox.Text = "";
    }


    private void resetDetailPanel()
    {
        HomeSplitView.IsPaneOpen = false;
    }

    private void TaskList_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadTodoCommand.ExecuteAsync(null);
    }

    private void TaskList_ItemClick(object sender, TappedRoutedEventArgs e)
    {
        if (((FrameworkElement)e.OriginalSource).DataContext as Todo == null)
        {
            HomeSplitView.IsPaneOpen = false;
        }
        else
        {
            HomeSplitView.IsPaneOpen = true;
        }
    }

    private void ListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
        ListView listView = sender as ListView;
        if (((FrameworkElement)e.OriginalSource).DataContext as Todo != null)
        {
            TodoItemFlyout.ShowAt(listView, e.GetPosition(listView));
            ViewModel.SelectedTodo = ((FrameworkElement)e.OriginalSource).DataContext as Todo;
        }
    }

    private void DeleteTaskFlyoutButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.Delete(ViewModel.SelectedTodo);

        resetDetailPanel();
    }

    private void CheckBox_Click(object sender, RoutedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        Todo todo = (Todo)checkBox.DataContext;
        ViewModel.Complete(todo);
        resetDetailPanel();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        Todo todo = (Todo)button.DataContext;
        ViewModel.SwapTodoStatus(todo);
    }

    private void DetailPanelTaskName_KeyUp(object sender, KeyRoutedEventArgs e)
    {
        var textbox = (TextBox)sender;
        ViewModel.SelectedTodo.Name = textbox.Text;
        ViewModel.Update(ViewModel.SelectedTodo);
    }

    private async void ExpectedDurationMinsButton_Click(object sender, RoutedEventArgs e)
    {
        ContentDialog dialog = new ContentDialog();
        dialog.Title = ViewModel.Strings.SetTheDuration;
        dialog.PrimaryButtonText = ViewModel.Strings.Save;
        dialog.CloseButtonText = ViewModel.Strings.Cancel;
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.Content = new DurationSettingDialog(ViewModel.SelectedTodo.ExpectedDurationMins);
        dialog.XamlRoot = Content.XamlRoot;

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            DurationSettingDialog tmp = (DurationSettingDialog)dialog.Content;
            ViewModel.SelectedTodo.ExpectedDurationMins = tmp.DurationResult;
            ViewModel.Update(ViewModel.SelectedTodo);
        }
    }
    private void StatusButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.SwapTodoStatus(ViewModel.SelectedTodo);
    }

    private async void PredictionButton_Click(object sender, RoutedEventArgs e)
    {
        ContentDialog dialog = new ContentDialog();
        dialog.Title = ViewModel.Strings.PredictionDuration;
        dialog.PrimaryButtonText = ViewModel.Strings.OK;
        dialog.Content = new DurationPredictionDialog(
            Ioc.Default.GetRequiredService<TodoManager>(),
            ViewModel.SelectedTodo.ExpectedDurationMins);
        dialog.XamlRoot = Content.XamlRoot;

        await dialog.ShowAsync();
    }
}
