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
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;

namespace HuntHelper.Data.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HuntedAnimalsController : ApiController
    {

        /// <summary>
        /// The database
        /// </summary>
        private HuntHelperContext db = new HuntHelperContext();
        /// <summary>
        /// The animal
        /// </summary>
        private static Animal animal;
        /// <summary>
        /// The hunter
        /// </summary>
        private static Hunter hunter;
        /// <summary>
        /// The ha
        /// </summary>
        private static HuntedAnimal ha = new HuntedAnimal();


        // GET: api/HuntedAnimals
        /// <summary>
        /// Gets the hunted animals.
        /// </summary>
        /// <returns></returns>
        public IQueryable<HuntedAnimal> GetHuntedAnimals()
        {
            return db.HuntedAnimals;
        }

        // GET: api/HuntedAnimals/5
        /// <summary>
        /// Gets the hunted animal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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

        //Get
        /// <summary>
        /// Gets the hunted animal.
        /// </summary>
        /// <param name="searchWord">The search word.</param>
        /// <returns></returns>
        [Route("api/HuntedAnimals/Search/{searchWord}")]
        [ResponseType(typeof(HuntedAnimal))]
        public IHttpActionResult GetHuntedAnimal(string searchWord)
        {
            var huntedAnimals = from ha in db.HuntedAnimals.Include("Animal").Include("Hunter").Include("Hunter.Weapon")
                                where ha.Animal.AnimalName.Contains(searchWord)
                                select ha;

            return Ok(huntedAnimals);
        }

        // PUT: api/HuntedAnimals/5
        /// <summary>
        /// Puts the hunted animal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="huntedAnimal">The hunted animal.</param>
        /// <returns></returns>
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

        // POST: api/HuntedAnimalsPoints
        /// <summary>
        /// Posts the hunted animal.
        /// </summary>
        /// <param name="huntedAnimal">The hunted animal.</param>
        /// <returns></returns>
        [Route("api/HuntedAnimalsPoints/", Name = "PostHuntedAnimalPoints")]
        [ResponseType(typeof(HuntedAnimalPoints))]
        public async Task<IHttpActionResult> PostHuntedAnimal(HuntedAnimalPoints huntedAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var animalList = db.Animals.Where(a => a.AnimalId == huntedAnimal.Animal.AnimalId).ToList();
            animal = animalList[0];
            var hunterlist = db.Hunters.Where(h => h.HunterId == 1).ToList();
            hunter = hunterlist[0];

            huntedAnimal.Hunter = hunter;
            huntedAnimal.Animal = animal;



            db.HuntedAnimals.Add(huntedAnimal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("PostHuntedAnimalPoints", new { id = huntedAnimal.HuntedAnimalId }, huntedAnimal);
            
        }

        /// <summary>
        /// Posts the hunted animal points.
        /// </summary>
        /// <param name="huntedAnimal">The hunted animal.</param>
        /// <returns></returns>
        [ResponseType(typeof(HuntedAnimal))]
        public async Task<IHttpActionResult> PostHuntedAnimalPoints(HuntedAnimal huntedAnimal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var animalList = db.Animals.Where(a => a.AnimalId == huntedAnimal.Animal.AnimalId).ToList();
            animal = animalList[0];
            var hunterlist = db.Hunters.Where(h => h.HunterId == 1).ToList();
            hunter = hunterlist[0];

            huntedAnimal.Hunter = hunter;
            huntedAnimal.Animal = animal;



            db.HuntedAnimals.Add(huntedAnimal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = huntedAnimal.HuntedAnimalId }, huntedAnimal);
        }



        // DELETE: api/HuntedAnimals/5
        /// <summary>
        /// Deletes the hunted animal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the hunter hunted animals.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/HuntedAnimal/Hunter/{id}")]
        [ResponseType(typeof(HuntedAnimalPoints[]))]
        public IHttpActionResult GetHunterHuntedAnimals(int id)
        {

            var huntedAnimals = (from ha in db.HuntedAnimals.Include("Animal").Include("Hunter").Include("Hunter.Weapon")
                                 where ha.Hunter.HunterId == id

                                 select ha);

            //
            //var huntedAnimals2 = (from ha in db.HuntedAnimals.Include("Animal").Include("Hunter").Include("HuntedAnimalPoints").Include("Hunter.Weapon").OfType<HuntedAnimal>()
            //                      where ha.Hunter.HunterId == id

            //                      select new { ha.HuntedAnimalId, ha.BulletCount, ha.Latitude, ha.Longitude, ha.Weight, ha.DateTime , ha.Animal, ha.Hunter, ha.Hunter.Weapon, ha.Hunter.Weapon.WeaponName, ha.Hunter.Weapon.Caliber, ha.ImageUrl });

  
            return Ok(huntedAnimals);
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
        /// Hunteds the animal exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool HuntedAnimalExists(int id)
        {
            return db.HuntedAnimals.Count(e => e.HuntedAnimalId == id) > 0;
        }
    }
}