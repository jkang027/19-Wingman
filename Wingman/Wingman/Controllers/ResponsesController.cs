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
    public class ResponsesController : ApiController
    {
        private WingmanDataContext db = new WingmanDataContext();

        // GET: api/Responses
        public IEnumerable<ResponseModel> GetResponses()
        {
            return Mapper.Map<IEnumerable<ResponseModel>>(db.Responses);
        }

        // GET: api/Responses/5
        [ResponseType(typeof(Response))]
        public IHttpActionResult GetResponse(int id)
        {
            Response response = db.Responses.Find(id);
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

            var dbResponse = db.Responses.Find(id);

            dbResponse.Update(response);
            db.Entry(dbResponse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
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

            db.Responses.Add(dbResponse);
            db.SaveChanges();

            response.ResponseId = dbResponse.ResponseId;
            response.DateSubmitted = dbResponse.DateSubmitted;

            return CreatedAtRoute("DefaultApi", new { id = response.ResponseId }, response);
        }

        // DELETE: api/Responses/5
        [ResponseType(typeof(Response))]
        public IHttpActionResult DeleteResponse(int id)
        {
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return NotFound();
            }

            db.Responses.Remove(response);
            db.SaveChanges();

            return Ok(Mapper.Map<ResponseModel>(response));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponseExists(int id)
        {
            return db.Responses.Count(e => e.ResponseId == id) > 0;
        }
    }
}