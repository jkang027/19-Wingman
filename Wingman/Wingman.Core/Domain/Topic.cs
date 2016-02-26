using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wingman.Core.Models;

namespace Wingman.Core.Domain
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        
        public virtual ICollection<Submission> Submissions { get; set; }

        public Topic()
        {

        }

        public Topic(TopicModel model)
        {
            this.Update(model);
        }

        public void Update(TopicModel model)
        {
            TopicId = model.TopicId;
            TopicName = model.TopicName;
        }
    }
}