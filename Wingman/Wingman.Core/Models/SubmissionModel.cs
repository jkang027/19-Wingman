using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wingman.Core.Domain;

namespace Wingman.Core.Models
{
    public class SubmissionModel
    {
        public int SubmissionId { get; set; }
        public int? TopicId { get; set; }
        public string UserId { get; set; }
        public string Context { get; set; }
        public string TextMessage { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        public virtual TopicModel Topic { get; set; }
        public virtual ICollection<ResponseModel> Responses { get; set; }
    }
}