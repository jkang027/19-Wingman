using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Wingman.Domain;

namespace Wingman.Infrastructure
{
    public class WingmanDataContext : IdentityDbContext<WingmanUser>
    {
        public WingmanDataContext() : base("Wingman")
        {
        }

        //SQL Tables
        public IDbSet<Response> Responses { get; set; }
        public IDbSet<Submission> Submissions { get; set; }
        public IDbSet<Topic> Topics { get; set; }

        //Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Response

            //Submission
            modelBuilder.Entity<Submission>().HasMany(s => s.Responses).WithRequired(r => r.Submission).HasForeignKey(r => r.SubmissionId).WillCascadeOnDelete(false);

            //Topic
            modelBuilder.Entity<Topic>().HasMany(s => s.Submissions).WithRequired(s => s.Topic).HasForeignKey(s => s.TopicId);

            //WingmanUser
            modelBuilder.Entity<WingmanUser>().HasMany(wu => wu.Responses).WithRequired(r => r.User).HasForeignKey(r => r.UserId);
            modelBuilder.Entity<WingmanUser>().HasMany(wu => wu.Submissions).WithRequired(s => s.User).HasForeignKey(s => s.UserId).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        
    }
}