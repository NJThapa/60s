using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<Pitch> Pitches { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUsers> NotificationUsers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<ScriptContent> ScriptContents { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }
        public DbSet<UserDeviceToken> UserDeviceTokens { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Rehersal> Rehersals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDeviceToken>().HasKey("UserId", "DeviceType");
            modelBuilder.Entity<NotificationUsers>().HasNoKey();
        }
    }
}
