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
using HuntHelper.DataAccess;
using HuntHelper.Model;

namespace HuntHelper.Data.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HuntersController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private HuntHelperContext db = new HuntHelperContext();

        // GET: api/Hunters
        /// <summary>
        /// Gets the hunters.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Hunter> GetHunters()
        {
            return db.Hunters;
        }

        // GET: api/Hunters/5
        /// <summary>
        /// Gets the hunter.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Hunter))]
        public async Task<IHttpActionResult> GetHunter(int id)
        {
            Hunter hunter = await db.Hunters.FindAsync(id);
            if (hunter == null)
            {
                return NotFound();
            }

            return Ok(hunter);
        }

        // PUT: api/Hunters/5
        /// <summary>
        /// Puts the hunter.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="hunter">The hunter.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHunter(int id, Hunter hunter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hunter.HunterId)
            {
                return BadRequest();
            }

            db.Entry(hunter).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HunterExists(id))
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

        // POST: api/Hunters
        /// <summary>
        /// Posts the hunter.
        /// </summary>
        /// <param name="hunter">The hunter.</param>
        /// <returns></returns>
        [ResponseType(typeof(Hunter))]
        public async Task<IHttpActionResult> PostHunter(Hunter hunter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hunters.Add(hunter);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hunter.HunterId }, hunter);
        }

        // DELETE: api/Hunters/5
        /// <summary>
        /// Deletes the hunter.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Hunter))]
        public async Task<IHttpActionResult> DeleteHunter(int id)
        {
            Hunter hunter = await db.Hunters.FindAsync(id);
            if (hunter == null)
            {
                return NotFound();
            }

            db.Hunters.Remove(hunter);
            await db.SaveChangesAsync();

            return Ok(hunter);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Hunters the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool HunterExists(int id)
        {
            return db.Hunters.Count(e => e.HunterId == id) > 0;
        }
    }
}