using CommunityToolkit.Mvvm.Input;
using FlashQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashQuiz.Services
{
    public class AladdinContext: DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public AladdinContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "flashquiz.sqlite");
            options.UseSqlite($"Filename={dbPath}");
        }
    }
}
