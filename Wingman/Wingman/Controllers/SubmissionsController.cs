﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //GET: api/Submission/5/Responses
        [Route("api/submissions/{SubmissionId}/responses")]
        public IEnumerable<ResponseModel> GetResponseForSubmission(int SubmissionId)
        {
            return Mapper.Map<IEnumerable<ResponseModel>>(
                _responseRepository.GetWhere(r => r.SubmissionId == SubmissionId)
            );
        }

        // GET: api/Submissions/User
        [Route("api/submissions/user")]
        public IEnumerable<SubmissionModel> GetUserSubmissions()
        {
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetWhere(s => s.UserId == CurrentUser.Id));
        }

        // GET: api/Submissions/Open
        [Route("api/submissions/open")]
        public IEnumerable<SubmissionModel> GetOpenSubmissions()
        {
            return Mapper.Map<IEnumerable<SubmissionModel>>(_submissionRepository.GetWhere(s => s.DateClosed == null));
        }

        // GET: api/Submissions/5/Responses/Paid
        [Route("api/submissions/{SubmissionId}/responses/paid")]
        public IEnumerable<ResponseModel> GetPaidResponses(ResponseModel response)
        {
            return Mapper.Map<IEnumerable<ResponseModel>>(_responseRepository.GetWhere(r => r.Purchased == true));
        }

        // GET: api/Submissions/5/Responses/Unpaid
        [Route("api/submissions/{SubmissionId}/responses/unpaid")]
        public IEnumerable<ResponseModel> GetUnpaidResponses(ResponseModel response)
        {
            return Mapper.Map<IEnumerable<ResponseModel>>(_responseRepository.GetWhere(r => r.Purchased == false));
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

        // POST: api/Submissions/5
        [HttpPost]
        [Route("api/submissions/close")]
        public IHttpActionResult PickAnswer(ResponseModel response)
        {
            var dbResponse = _responseRepository.GetById(response.ResponseId);

            if (dbResponse.Submission.DateClosed == null)
            {
                dbResponse.Picked = true;
                dbResponse.Submission.DateClosed = DateTime.Now;

                _responseRepository.Update(dbResponse);
                _unitOfWork.Commit();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool SubmissionExists(int id)
        {
            //return db.Submissions.Count(e => e.SubmissionId == id) > 0;
            return _submissionRepository.Any(e => e.SubmissionId == id);
        }
    }
}