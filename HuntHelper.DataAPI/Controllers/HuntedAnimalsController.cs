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

namespace HuntHelper.DataAPI.Controllers
{
    public class HuntedAnimalsController : ApiController
    {
        private HuntHelperContext db = new HuntHelperContext();

        // GET: api/HuntedAnimals
        public IQueryable<HuntedAnimal> GetHuntedAnimals()
        {
            return db.HuntedAnimals;
        }

        // GET: api/HuntedAnimals/5
        [ResponseType(typeof(HuntedAnimal))]
        public async Task<IHttpActionResult> GetHuntedAnimal(int id)
        {
            HuntedAnimal huntedAnimal = await db.HuntedAnimals.FindAsync(id);
            if (huntedAnimal == null)
            {
                return NotFound();
            }

            return Ok(huntedAnimal);
        }

        // PUT: api/HuntedAnimals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutHuntedAnimal(int id, HuntedAnimal huntedAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != huntedAnimal.HuntedAnimalId)
            {
                return BadRequest();
            }

            db.Entry(huntedAnimal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HuntedAnimalExists(id))
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

        // POST: api/HuntedAnimals
        [ResponseType(typeof(HuntedAnimal))]
        public async Task<IHttpActionResult> PostHuntedAnimal(HuntedAnimal huntedAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.HuntedAnimals.Add(huntedAnimal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = huntedAnimal.HuntedAnimalId }, huntedAnimal);
        }

        // DELETE: api/HuntedAnimals/5
        [ResponseType(typeof(HuntedAnimal))]
        public async Task<IHttpActionResult> DeleteHuntedAnimal(int id)
        {
            HuntedAnimal huntedAnimal = await db.HuntedAnimals.FindAsync(id);
            if (huntedAnimal == null)
            {
                return NotFound();
            }

            db.HuntedAnimals.Remove(huntedAnimal);
            await db.SaveChangesAsync();

            return Ok(huntedAnimal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HuntedAnimalExists(int id)
        {
            return db.HuntedAnimals.Count(e => e.HuntedAnimalId == id) > 0;
        }
    }
}