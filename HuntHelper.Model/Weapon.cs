
using System.Collections.Generic;

namespace HuntHelper.Model
{
    /// <summary>
    /// Weapon class
    /// </summary>
    public class Weapon
    {

        /// <summary>
        /// Gets or sets the weapon identifier.
        /// </summary>
        /// <value>
        /// The weapon identifier.
        /// </value>
        public int WeaponId { get; set; }

        /// <summary>
        /// Gets or sets the name of the weapon.
        /// </summary>
        /// <value>
        /// The name of the weapon.
        /// </value>
        public string WeaponName { get; set; }

        /// <summary>
        /// Gets or sets the caliber.
        /// </summary>
        /// <value>
        /// The caliber.
        /// </value>
        public string Caliber { get; set; }

        /// <summary>
        /// Gets or sets the hunters.
        /// </summary>
        /// <value>
        /// The hunters.
        /// </value>
        public virtual ICollection<Hunter> Hunters { get; set; }
    }
}
