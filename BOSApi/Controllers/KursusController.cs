﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BOSApi.Models;

namespace BOSApi.Controllers
{
    public class KursusController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/Kursus
        public IQueryable<Kursus> GetKursus()
        {
            return db.Kursus;
        }

        // GET: api/Kursus/5
        [ResponseType(typeof(Kursus))]
        public IHttpActionResult GetKursus(int id)
        {
            Kursus kursus = db.Kursus.Find(id);
            if (kursus == null)
            {
                return NotFound();
            }

            return Ok(kursus);
        }

        // PUT: api/Kursus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKursus(int id, Kursus kursus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kursus.Id)
            {
                return BadRequest();
            }

            db.Entry(kursus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KursusExists(id))
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

        // POST: api/Kursus
        [ResponseType(typeof(Kursus))]
        public IHttpActionResult PostKursus(Kursus kursus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kursus.Add(kursus);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KursusExists(kursus.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kursus.Id }, kursus);
        }

        // DELETE: api/Kursus/5
        [ResponseType(typeof(Kursus))]
        public IHttpActionResult DeleteKursus(int id)
        {
            Kursus kursus = db.Kursus.Find(id);
            if (kursus == null)
            {
                return NotFound();
            }

            db.Kursus.Remove(kursus);
            db.SaveChanges();

            return Ok(kursus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KursusExists(int id)
        {
            return db.Kursus.Count(e => e.Id == id) > 0;
        }
    }
}