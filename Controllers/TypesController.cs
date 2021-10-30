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
using ServiceBook;

namespace ServiceBook.Controllers
{
    public class TypesController : ApiController
    {
        private dbBookEntities db = new dbBookEntities();

        // GET: api/Types
        public IQueryable<Type> GetTypes()
        {
            return db.Types;
        }

        // GET: api/Types/5
        [ResponseType(typeof(Type))]
        public async Task<IHttpActionResult> GetType(int id)
        {
            Type type = await db.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

        // PUT: api/Types/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutType(int id, Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != type.tid)
            {
                return BadRequest();
            }

            db.Entry(type).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // POST: api/Types
        [ResponseType(typeof(Type))]
        public async Task<IHttpActionResult> PostType(Type type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Types.Add(type);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = type.tid }, type);
        }

        // DELETE: api/Types/5
        [ResponseType(typeof(Type))]
        public async Task<IHttpActionResult> DeleteType(int id)
        {
            Type type = await db.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            db.Types.Remove(type);
            await db.SaveChangesAsync();

            return Ok(type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeExists(int id)
        {
            return db.Types.Count(e => e.tid == id) > 0;
        }
    }
}