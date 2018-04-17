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
    public class WeaponsController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private HuntHelperContext db = new HuntHelperContext();

        // GET: api/Weapons
        /// <summary>
        /// Gets the weapons.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Weapon> GetWeapons()
        {
            return db.Weapons;
        }

        // GET: api/Weapons/5
        /// <summary>
        /// Gets the weapon.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Weapon))]
        public async Task<IHttpActionResult> GetWeapon(int id)
        {
            Weapon weapon = await db.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            return Ok(weapon);
        }

        // PUT: api/Weapons/5
        /// <summary>
        /// Puts the weapon.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="weapon">The weapon.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeapon(int id, Weapon weapon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != weapon.WeaponId)
            {
                return BadRequest();
            }

            db.Entry(weapon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponExists(id))
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

        // POST: api/Weapons
        /// <summary>
        /// Posts the weapon.
        /// </summary>
        /// <param name="weapon">The weapon.</param>
        /// <returns></returns>
        [ResponseType(typeof(Weapon))]
        public async Task<IHttpActionResult> PostWeapon(Weapon weapon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Weapons.Add(weapon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = weapon.WeaponId }, weapon);
        }

        // DELETE: api/Weapons/5
        /// <summary>
        /// Deletes the weapon.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ResponseType(typeof(Weapon))]
        public async Task<IHttpActionResult> DeleteWeapon(int id)
        {
            Weapon weapon = await db.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            db.Weapons.Remove(weapon);
            await db.SaveChangesAsync();

            return Ok(weapon);
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
        /// Weapons the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool WeaponExists(int id)
        {
            return db.Weapons.Count(e => e.WeaponId == id) > 0;
        }
    }
}