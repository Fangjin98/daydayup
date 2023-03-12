using Microsoft.EntityFrameworkCore;
using DayDayUp.Models;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace DayDayUp.Services
{
    public class MyTaskContext : DbContext
    {
        public DbSet<Todo> myTasks { get; set; }

        public string DbPath { get; }

        public MyTaskContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = System.IO.Path.Combine(path, "mytasks.db");
            Debug.WriteLine(DbPath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .Property(e => e.TimeStamps)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<DateTime>>(v, (JsonSerializerOptions)null),
                    new ValueComparer<List<DateTime>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
}
