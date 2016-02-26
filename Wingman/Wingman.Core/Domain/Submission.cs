using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wingman.Core.Models;

namespace Wingman.Core.Domain
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public int? TopicId { get; set; }
        public string UserId { get; set; }
        public string Context { get; set; }
        public string TextMessage { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        public virtual ICollection<Response> Responses { get; set; }
        public virtual WingmanUser User { get; set; }
        public virtual Topic Topic { get; set; }

        public Submission()
        {

        }

        public Submission(SubmissionModel model)
        {
            this.Update(model);
        }

        public void Update(SubmissionModel model)
        {
            SubmissionId = model.SubmissionId;
            TopicId = model.TopicId;
            UserId = model.UserId;
            Context = model.Context;
            TextMessage = model.TextMessage;
            DateOpened = model.DateOpened;
            DateClosed = model.DateClosed;
        }
    }
}