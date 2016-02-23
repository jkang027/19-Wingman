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
    public class TopicsController : ApiController
    {
        private WingmanDataContext db = new WingmanDataContext();

        // GET: api/Topics
        public IEnumerable<TopicModel> GetTopics()
        {
            return Mapper.Map<IEnumerable<TopicModel>>(db.Topics);
        }

        // GET: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult GetTopic(int id)
        {
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<TopicModel>(topic));
        }

        // PUT: api/Topics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTopic(int id, TopicModel topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != topic.TopicId)
            {
                return BadRequest();
            }

            var dbTopic = db.Topics.Find(id);

            dbTopic.Update(topic);
            db.Entry(topic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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

        // POST: api/Topics
        [ResponseType(typeof(Topic))]
        public IHttpActionResult PostTopic(TopicModel topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbTopic = new Topic(topic);

            db.Topics.Add(dbTopic);
            db.SaveChanges();

            topic.TopicId = dbTopic.TopicId;

            return CreatedAtRoute("DefaultApi", new { id = topic.TopicId }, topic);
        }

        // DELETE: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult DeleteTopic(int id)
        {
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return NotFound();
            }

            db.Topics.Remove(topic);
            db.SaveChanges();

            return Ok(Mapper.Map<TopicModel>(topic));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TopicExists(int id)
        {
            return db.Topics.Count(e => e.TopicId == id) > 0;
        }
    }
}