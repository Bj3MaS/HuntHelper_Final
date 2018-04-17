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
    public class AnimalsController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private HuntHelperContext db = new HuntHelperContext();

        // GET: api/Animals
        /// <summary>
        /// Gets the animals.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Animal> GetAnimals()
        {
            return db.Animals;
        }

        // GET: api/Animals/5
        /// <summary>
        /// Gets the animal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> GetAnimal(int id)
        {
            Animal animal = await db.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        //Get
        /// <summary>
        /// Gets the hunted animal.
        /// </summary>
        /// <param name="searchWord">The search word.</param>
        /// <returns></returns>
        [Route("api/Animals/Search/{searchWord}")]
        [ResponseType(typeof(HuntedAnimal))]
        public IHttpActionResult GetHuntedAnimal(string searchWord)
        {
            var Animals = from a in db.Animals
                          where a.AnimalName.Contains(searchWord)
                          select a;

            return Ok(Animals);
        }

        // PUT: api/Animals/5
        /// <summary>
        /// Puts the animal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="animal">The animal.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAnimal(int id, Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.AnimalId)
            {
                return BadRequest();
            }

            db.Entry(animal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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

        // POST: api/Animals
        /// <summary>
        /// Posts the animal.
        /// </summary>
        /// <param name="animal">The animal.</param>
        /// <returns></returns>
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> PostAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animals.Add(animal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = animal.AnimalId }, animal);
        }

        /// <summary>
        /// Posts the animal.
        /// </summary>
        /// <param name="animal">The animal.</param>
        /// <returns></returns>
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> PostAnimal(PointsAnimal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animals.Add(animal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = animal.AnimalId }, animal);
        }

        // DELETE: api/Animals/5
        /// <summary>
        /// Deletes the animal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Animal))]
        public async Task<IHttpActionResult> DeleteAnimal(int id)
        {
            Animal animal = await db.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            db.Animals.Remove(animal);
            await db.SaveChangesAsync();

            return Ok(animal);
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
        /// Animals the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool AnimalExists(int id)
        {
            return db.Animals.Count(e => e.AnimalId == id) > 0;
        }
    }
}