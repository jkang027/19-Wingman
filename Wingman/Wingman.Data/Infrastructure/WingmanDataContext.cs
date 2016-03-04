using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Wingman.Core.Domain;

namespace Wingman.Infrastructure
{
    public class WingmanDataContext : DbContext
    {
        public WingmanDataContext() : base("Wingman")
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        //SQL Tables
        public IDbSet<Response> Responses { get; set; }
        public IDbSet<Submission> Submissions { get; set; }
        public IDbSet<Topic> Topics { get; set; }
        public IDbSet<WingmanUser> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }

        //Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Response

            //Submission
            modelBuilder.Entity<Submission>()
                        .HasMany(s => s.Responses)
                        .WithRequired(r => r.Submission)
                        .HasForeignKey(r => r.SubmissionId)
                        .WillCascadeOnDelete(false);

            //Topic
            modelBuilder.Entity<Topic>()
                        .HasMany(s => s.Submissions)
                        .WithRequired(s => s.Topic)
                        .HasForeignKey(s => s.TopicId);

            //WingmanUser
            modelBuilder.Entity<WingmanUser>()
                        .HasMany(wu => wu.Responses)
                        .WithRequired(r => r.User)
                        .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<WingmanUser>()
                        .HasMany(wu => wu.Submissions)
                        .WithRequired(s => s.User)
                        .HasForeignKey(s => s.UserId)
                        .WillCascadeOnDelete(false);

            // Specify Relationships
            modelBuilder.Entity<WingmanUser>()
                        .HasMany(u => u.Roles)
                        .WithRequired(ur => ur.User)
                        .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
                        .HasMany(r => r.Users)
                        .WithRequired(ur => ur.Role)
                        .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            base.OnModelCreating(modelBuilder);
        }

        
    }
}