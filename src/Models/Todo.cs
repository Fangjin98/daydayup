﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DayDayUp.Models
{
    [Table("MyTasks")]
    public class Todo : ObservableObject
    {

        [Key]
        public int Id { get; set; }

        // init by users
        private string name;

        public string Name {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private int expectedDurationMins;

        public int ExpectedDurationMins
        {
            get => expectedDurationMins;
            set => SetProperty(ref expectedDurationMins, value);
        }

        // set by users
        public string Description { get; set; }

        public bool IsFinished { get; set; }

        private TodoStatus status;

        public TodoStatus Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        // record by app
        public DateTime CreationDate { get; set; }

        public List<DateTime> TimeStamps { get; set; }
        
        // init by program
        [NotMapped]
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
        [NotMapped]
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

        [NotMapped]
        public DateTime ExpectedFinishDate { get; set; }

        [NotMapped]
        public int Progress
        {
            get
            {
                if (IsFinished == true) return 100;
                if (ExpectedDurationMins != 0)
                {
                    return Convert.ToInt32(
                        Convert.ToDecimal(DurationMins) / Convert.ToDecimal(ExpectedDurationMins) * 100);
                }
                else return 0;
            }
        }

        [NotMapped]
        public double Bias
        {
            get
            {
                if (ExpectedDurationMins != 0 && DurationMins != 0)
                {
                    return (double)Math.Round(Convert.ToDecimal(DurationMins - ExpectedDurationMins) /
                        Convert.ToDecimal(ExpectedDurationMins), 2);
                }
                else return 0;
            }
        }
    }

    public enum TodoStatus
    {
        Doing = 0,
        Pause = 1
    }
}
