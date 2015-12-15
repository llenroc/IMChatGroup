using Microsoft.AspNet.Identity.EntityFramework;
//using Base.Data.Configuration;
using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Base.Data
{
    public class BaseEntities : IdentityDbContext<User>
    {
        public BaseEntities()
          : base("DefaultConnection")
        {

        }
  
 
        //public DbSet<User> UserProfiles { get; set; }
        public DbSet<GroupInvitation> GroupInvitations { get; set; }
        public DbSet<FollowUser> FollowUser { get; set; }
        public DbSet<GroupRequest> GroupRequests { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<SysConfig> SysConfig { get; set; }
        public DbSet<Room> Rooms { get; set; }
      
        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            //modelBuilder.Configurations.Add(new CourseConfiguration());
            //modelBuilder.Configurations.Add(new SubjectConfiguration());


          //  modelBuilder.Entity<UserProfile>().ToTable("UserProfile");        
            //modelBuilder.Configurations.Add(new FocusConfiguration());
            //modelBuilder.Configurations.Add(new FollowRequestConfiguration());
            //modelBuilder.Configurations.Add(new FollowUserConfiguration());
            //modelBuilder.Configurations.Add(new GoalConfiguration());
            //modelBuilder.Configurations.Add(new GoalStatusConfiguration());
            //modelBuilder.Configurations.Add(new GroupCommentConfiguration());
            //modelBuilder.Configurations.Add(new GroupCommentUserConfguration());
            base.OnModelCreating(modelBuilder);

        }
    }
}
