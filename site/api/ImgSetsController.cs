using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using site.core;
using site.core.Models;

namespace site.api
{
    public class ImgSetsController : ApiController
    {
        private SiteContext db = new SiteContext();

        // GET: api/ImgSets
        public IQueryable<ImgSet> GetImgSets()
        {
            return db.ImgSets;
        }

        // GET: api/ImgSets/5
        [ResponseType(typeof(ImgSet))]
        public async Task<IHttpActionResult> GetImgSet(string id)
        {
            ImgSet imgSet = await db.ImgSets.FindAsync(id);
            if (imgSet == null)
            {
                return NotFound();
            }

            return Ok(imgSet);
        }

        // PUT: api/ImgSets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutImgSet(string id, ImgSet imgSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imgSet.Id)
            {
                return BadRequest();
            }

            db.Entry(imgSet).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImgSetExists(id))
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

        // POST: api/ImgSets
        [ResponseType(typeof(ImgSet))]
        public async Task<IHttpActionResult> PostImgSet(ImgSet imgSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ImgSets.Add(imgSet);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImgSetExists(imgSet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = imgSet.Id }, imgSet);
        }

        // DELETE: api/ImgSets/5
        [ResponseType(typeof(ImgSet))]
        public async Task<IHttpActionResult> DeleteImgSet(string id)
        {
            ImgSet imgSet = await db.ImgSets.FindAsync(id);
            if (imgSet == null)
            {
                return NotFound();
            }

            db.ImgSets.Remove(imgSet);
            await db.SaveChangesAsync();

            return Ok(imgSet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ImgSetExists(string id)
        {
            return db.ImgSets.Count(e => e.Id == id) > 0;
        }
    }
}