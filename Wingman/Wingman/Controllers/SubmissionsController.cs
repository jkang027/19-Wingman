using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Wingman.Core.Domain;
using Wingman.Core.Infrastructure;
using Wingman.Core.Models;
using Wingman.Core.Repository;

namespace Wingman.Controllers
{
    public class SubmissionsController : ApiController
    {
        //private WingmanDataContext db = new WingmanDataContext();

        private readonly ISubmissionRepository _submissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        //Constructor based dependency injection
        public SubmissionsController(ISubmissionRepository submissionRepository, IUnitOfWork unitOfWork)
        {
            _submissionRepository = submissionRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Submissions
        public IEnumerable<SubmissionModel> GetSubmissions()
        {
            // return Mapper.Map<IEnumerable<SubmissionModel>>(db.Submissions);
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetAll());
        }

        // GET: api/Submissions/5
        [ResponseType(typeof(Submission))]
        public IHttpActionResult GetSubmission(int id)
        {
            //Submission submission = db.Submissions.Find(id);
            Submission submission = _submissionRepository.GetById(id);

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

            var dbSubmission = _submissionRepository.GetById(id);

            dbSubmission.Update(submission);

            //db.Entry(dbSubmission).State = EntityState.Modified;
            _submissionRepository.Update(dbSubmission);

            try
            {
                //db.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception)
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
            dbSubmission.User = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            //db.Submissions.Add(dbSubmission);
            //db.SaveChanges();
            _submissionRepository.Add(dbSubmission);
            _unitOfWork.Commit();

            submission.SubmissionId = dbSubmission.SubmissionId;
            submission.DateOpened = dbSubmission.DateOpened;

            return CreatedAtRoute("DefaultApi", new { id = submission.SubmissionId }, submission);
        }

        // DELETE: api/Submissions/5
        [ResponseType(typeof(Submission))]
        public IHttpActionResult DeleteSubmission(int id)
        {
            //Submission submission = db.Submissions.Find(id);
            Submission submission = _submissionRepository.GetById(id);

            if (submission == null)
            {
                return NotFound();
            }

            //db.Submissions.Remove(submission);
            //db.SaveChanges();
            _submissionRepository.Delete(submission);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<SubmissionModel>(submission));
        }

        private bool SubmissionExists(int id)
        {
            //return db.Submissions.Count(e => e.SubmissionId == id) > 0;
            return _submissionRepository.Any(e => e.SubmissionId == id);
        }
    }
}