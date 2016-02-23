using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Wingman.Domain;
using Wingman.Infrastructure;
using Wingman.Models;

namespace Wingman.Controllers
{
    public class SubmissionsController : ApiController
    {
        private WingmanDataContext db = new WingmanDataContext();

        // GET: api/Submissions
        public IEnumerable<SubmissionModel> GetSubmissions()
        {
            return Mapper.Map<IEnumerable<SubmissionModel>>(db.Submissions);
        }

        // GET: api/Submissions/5
        [ResponseType(typeof(Submission))]
        public IHttpActionResult GetSubmission(int id)
        {
            Submission submission = db.Submissions.Find(id);
            if (submission == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SubmissionModel>(submission));
        }

        // PUT: api/Submissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubmission(int id, SubmissionModel submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != submission.SubmissionId)
            {
                return BadRequest();
            }

            var dbSubmission = db.Submissions.Find(id);

            dbSubmission.Update(submission);
            db.Entry(dbSubmission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmissionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Submissions
        [ResponseType(typeof(Submission))]
        public IHttpActionResult PostSubmission(SubmissionModel submission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbSubmission = new Submission(submission);

            db.Submissions.Add(dbSubmission);
            db.SaveChanges();

            submission.SubmissionId = dbSubmission.SubmissionId;

            return CreatedAtRoute("DefaultApi", new { id = submission.SubmissionId }, submission);
        }

        // DELETE: api/Submissions/5
        [ResponseType(typeof(Submission))]
        public IHttpActionResult DeleteSubmission(int id)
        {
            Submission submission = db.Submissions.Find(id);
            if (submission == null)
            {
                return NotFound();
            }

            db.Submissions.Remove(submission);
            db.SaveChanges();

            return Ok(Mapper.Map<SubmissionModel>(submission));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubmissionExists(int id)
        {
            return db.Submissions.Count(e => e.SubmissionId == id) > 0;
        }
    }
}