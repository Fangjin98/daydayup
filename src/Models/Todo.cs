﻿using LiteDB;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayDayUp.Models;

public class Todo : ObservableObject
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public List<DateTime> TimeStamps { get; set; }
    public string Description { get; set; }
    public bool IsFinished { get; set; }
    // TODO: redesign, remove pause procedure.

    public string Name
    {
        get => name;
        set => SetProperty(ref name, value);
    }

    public int ExpectedDurationMins
    {
        get => expectedDurationMins;
        set => SetProperty(ref expectedDurationMins, value);
    }

    public TodoStatus Status
    {
        get => status;
        set => SetProperty(ref status, value);
    }

    // TODO: reimplementation
    [BsonIgnore]
    public int DurationMins
    {
        get
        {
            if (ExpectedDurationMins != 0)
            {
                int len = TimeStamps.Count;
                double totalDuration = 0;
                if (IsFinished == true)
                {
                    if (len == 1) // directly finish the todo, only finish date
                    {
                        return 0;
                    }
                    else if (len % 2 == 0) // todo is finished under the doing status
                    {
                        for (int i = 0; i < len; i += 2)
                        {
                            totalDuration += (TimeStamps[i + 1] - TimeStamps[i]).TotalMinutes;
                        }
                    }
                    else // todo is finished under the pause status, the finish date can't be counted
                    {
                        for (int i = 0; i < len - 1; i += 2)
                        {
                            totalDuration += (TimeStamps[i + 1] - TimeStamps[i]).TotalMinutes;
                        }
                    }
                }
                else // unfinished todos, no finish date
                {
                    if (len % 2 == 0)
                    {
                        for (int i = 0; i < len; i += 2)
                        {
                            totalDuration += (TimeStamps[i + 1] - TimeStamps[i]).TotalMinutes;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < len - 1; i += 2)
                        {
                            totalDuration += (TimeStamps[i + 1] - TimeStamps[i]).TotalMinutes;
                        }
                        totalDuration += (DateTime.Now - TimeStamps.Last()).TotalMinutes;
                    }
                }
                return Convert.ToInt32(totalDuration);
            }
            else
            {
                return 0;
            }
        }
    }
    
    [BsonIgnore]
    public DateTime? FinishDate
    {
        get
        {
            if (IsFinished == true)
            {
                return TimeStamps.Last();
            }
            else return null;
        }
    }

    [BsonIgnore]
    public DateTime ExpectedFinishDate { get; set; }
    
    [BsonIgnore]
    public int Progress
    {
        get
        {
            if (ExpectedDurationMins != 0)
            {
                return Convert.ToInt32(Convert.ToDecimal(DurationMins) / Convert.ToDecimal(ExpectedDurationMins) * 100);
            }
            else return 0;
        }
    }
    
    [BsonIgnore]
    public int Bias
    {
        get
        {
            if (ExpectedDurationMins != 0)
            {
                return Convert.ToInt32(
                    Convert.ToDecimal(DurationMins - ExpectedDurationMins) / Convert.ToDecimal(ExpectedDurationMins) * 100);
            }
            else return 0;
        }
    }

    private string name;
    private int expectedDurationMins;
    private TodoStatus status;
}
