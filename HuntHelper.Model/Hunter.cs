using System.Collections.Generic;

namespace HuntHelper.Model
{
    /// <summary>
    /// Hunter Model class
    /// </summary>
    public class Hunter
    {
        /// <summary>
        /// Gets or sets the hunter identifier.
        /// </summary>
        /// <value>
        /// The hunter identifier.
        /// </value>
        public int HunterId { get; set; }

        /// <summary>
        /// Gets or sets the name of the hunter.
        /// </summary>
        /// <value>
        /// The name of the hunter.
        /// </value>
        public string HunterName { get; set; }

        /// <summary>
        /// Gets or sets the weapon.
        /// </summary>
        /// <value>
        /// The weapon.
        /// </value>
        public Weapon Weapon { get; set; }

        /// <summary>
        /// Gets or sets the hunted animals.
        /// </summary>
        /// <value>
        /// The hunted animals.
        /// </value>
        public virtual ICollection<HuntedAnimal> HuntedAnimals { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hunter"/> class.
        /// </summary>
        public Hunter()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hunter"/> class.
        /// </summary>
        /// <param name="hunterName">Name of the hunter.</param>
        /// <param name="weapon">The weapon.</param>
        public Hunter(string hunterName, Weapon weapon)
        {
            HunterName = hunterName;
            Weapon = weapon;
        }
    }
}

