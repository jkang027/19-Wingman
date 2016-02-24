using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wingman.Domain
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public class WingmanUser : IdentityUser
    {
        public Gender Gender { get; set; }
        public int? NumberOfFlags { get; set; }
        public int? NumberOfKeys { get; set; }
        public int? NumberOfRatings { get; set; }
        public decimal? AverageRating { get; set; }
        public int? NumberOfAnswersPicked { get; set; }
        public int? NumberOfAnswersSubmitted { get; set; }
        
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}