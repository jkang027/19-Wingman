using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wingman.Core.Domain
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public class WingmanUser : IUser<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        public string Email { get; set; }
        public string Telephone { get; set; }

        public Gender Gender { get; set; }
        public int? NumberOfFlags { get; set; }
        public int? NumberOfKeys { get; set; }
        public int? NumberOfRatings { get; set; }
        public decimal? AverageRating { get; set; }
        public int? NumberOfAnswersPicked
        {
            get
            {
                return Responses.Count(r => r.Picked);
            }
        }
        public int? NumberOfAnswersSubmitted
        {
            get
            {
                return Responses.Count();
            }
        }
        
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}