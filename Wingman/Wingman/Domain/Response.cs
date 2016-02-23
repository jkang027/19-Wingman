using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wingman.Models;

namespace Wingman.Domain
{
    public class Response
    {
        public int ResponseId { get; set; }
        public int SubmissionId { get; set; }
        public string UserId { get; set; }
        public string ResponseText { get; set; }
        public bool Picked { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int KeyPrice { get; set; }
      
        public virtual Submission Submission { get; set; }
        public virtual WingmanUser User { get; set; }

        public Response()
        {

        }

        public Response(ResponseModel model)
        {
            this.Update(model);
        }

        public void Update(ResponseModel model)
        {
            ResponseId = model.ResponseId;
            SubmissionId = model.SubmissionId;
            UserId = model.UserId;
            ResponseText = model.ResponseText;
            Picked = model.Picked;
            DateSubmitted = model.DateSubmitted;
            KeyPrice = model.KeyPrice;
        }
    }
}