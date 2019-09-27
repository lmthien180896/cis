using CIS.Model.Models;
using System.Data.Entity;

namespace CIS.Data
{
    public class CISDbContext : DbContext
    {
        public CISDbContext() : base("CISConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestCategory> RequestCategories { get; set; }
        public DbSet<RequestReport> RequestReports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<SupportOnline> SupportOnlines { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<VisitorStatistic> VisitorStatistics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}