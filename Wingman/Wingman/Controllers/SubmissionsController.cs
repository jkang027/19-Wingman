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
using Wingman.Data.Infrastructure;
using Wingman.Infrastructure;

namespace Wingman.Controllers
{
    [Authorize]
    public class SubmissionsController : BaseApiController
    {
        //private WingmanDataContext db = new WingmanDataContext();

        private readonly ISubmissionRepository _submissionRepository;
        private readonly IResponseRepository _responseRepository;
        private readonly IUnitOfWork _unitOfWork;

        //Constructor based dependency injection
        public SubmissionsController(ISubmissionRepository submissionRepository, IWingmanUserRepository wingmanUserRepository, IResponseRepository responseRepository, IUnitOfWork unitOfWork) : base(wingmanUserRepository)
        {
            _submissionRepository = submissionRepository;
            _unitOfWork = unitOfWork;
            _responseRepository = responseRepository;
        }

        // GET: api/Submissions
        public IEnumerable<SubmissionModel> GetSubmissions()
        {
            //return Mapper.Map<IEnumerable<SubmissionModel>>(db.Submissions);
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetAll());
        }

        //GET: api/Submission/5/Response
        [Route("api/submissions/{SubmissionId}/responses")]
        public IEnumerable<ResponseModel> GetResponseForSubmission(int SubmissionId)
        {
            return Mapper.Map<IEnumerable<ResponseModel>>(
                _responseRepository.GetWhere(r => r.SubmissionId == SubmissionId)
            );
        }

        // GET: api/Submissions/user
        [Route("api/Submissions/user")]
        public IEnumerable<SubmissionModel> GetUserSubmissions()
        {
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetWhere(s => s.UserId == CurrentUser.Id));
        }

        // GET: api/Submissions/user
        [Route("api/Submissions/open")]
        public IEnumerable<SubmissionModel> GetOpenSubmissions()
        {
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetWhere(s => s.DateClosed == null));
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

            //db.Submissions.Add(dbSubmission);
            //db.SaveChanges();
            dbSubmission.UserId = CurrentUser.Id;
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