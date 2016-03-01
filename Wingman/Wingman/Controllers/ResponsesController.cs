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
using Wingman.Infrastructure;

namespace Wingman.Controllers
{
    public class ResponsesController : BaseApiController
    {
        private readonly IResponseRepository _responseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResponsesController(IResponseRepository responseRepository , IUnitOfWork unitOfWork, IWingmanUserRepository wingmanUserRepository) : base(wingmanUserRepository)
        {
            _responseRepository = responseRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Responses
        public IEnumerable<ResponseModel> GetResponses()
        {
            return Mapper.Map<IEnumerable<ResponseModel>>(_responseRepository.GetAll());
        }

        // GET: api/Responses/5
        [ResponseType(typeof(Response))]
        public IHttpActionResult GetResponse(int id)
        {
            Response response = _responseRepository.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ResponseModel>(response));
        }

        // PUT: api/Responses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResponse(int id, ResponseModel response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != response.ResponseId)
            {
                return BadRequest();
            }

            var dbResponse = _responseRepository.GetById(id);

            dbResponse.Update(response);

            _responseRepository.Update(dbResponse);


            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ResponseExists(id))
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

        // POST: api/Responses
        [ResponseType(typeof(Response))]
        public IHttpActionResult PostResponse(ResponseModel response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbResponse = new Response(response);

            dbResponse.UserId = CurrentUser.Id;
            _responseRepository.Add(dbResponse);
            _unitOfWork.Commit();

            response.ResponseId = dbResponse.ResponseId;
            response.DateSubmitted = dbResponse.DateSubmitted;

            return CreatedAtRoute("DefaultApi", new { id = response.ResponseId }, response);
        }

        // DELETE: api/Responses/5
        [ResponseType(typeof(Response))]
        public IHttpActionResult DeleteResponse(int id)
        {
            Response response = _responseRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            _responseRepository.Delete(response);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<ResponseModel>(response));
        }

        private bool ResponseExists(int id)
        {
            return _responseRepository.Any(e => e.ResponseId == id);
        }
    }
}