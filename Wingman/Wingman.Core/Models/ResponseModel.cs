using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wingman.Core.Domain;

namespace Wingman.Core.Models
{
    public class ResponseModel
    {
        public int ResponseId { get; set; }
        public int SubmissionId { get; set; }
        public string UserId { get; set; }
        public string ResponseText { get; set; }
        public bool Picked { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int KeyPrice { get; set; }
        public bool Purchased { get; set; }

        public virtual SubmissionModel Submission { get; set; }
    }
}