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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ClientRequestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ClientRequests
        public IQueryable<ClientRequest> GetClientRequests()
        {
            return db.ClientRequests;
        }

        // GET: api/ClientRequests/5
        [ResponseType(typeof(ClientRequest))]
        public IHttpActionResult GetClientRequest(int id)
        {
            ClientRequest clientRequest = db.ClientRequests.Find(id);
            if (clientRequest == null)
            {
                return NotFound();
            }

            return Ok(clientRequest);
        }

        // PUT: api/ClientRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClientRequest(int id, ClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientRequest.ClientId)
            {
                return BadRequest();
            }

            db.Entry(clientRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientRequestExists(id))
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

        // POST: api/ClientRequests
        [ResponseType(typeof(ClientRequest))]
        public IHttpActionResult PostClientRequest(ClientRequest clientRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ClientRequests.Add(clientRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clientRequest.ClientId }, clientRequest);
        }

        // DELETE: api/ClientRequests/5
        [ResponseType(typeof(ClientRequest))]
        public IHttpActionResult DeleteClientRequest(int id)
        {
            ClientRequest clientRequest = db.ClientRequests.Find(id);
            if (clientRequest == null)
            {
                return NotFound();
            }

            db.ClientRequests.Remove(clientRequest);
            db.SaveChanges();

            return Ok(clientRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientRequestExists(int id)
        {
            return db.ClientRequests.Count(e => e.ClientId == id) > 0;
        }
    }
}