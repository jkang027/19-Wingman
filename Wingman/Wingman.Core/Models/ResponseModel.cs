using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }
}