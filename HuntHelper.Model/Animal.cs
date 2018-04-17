using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HuntHelper.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Animal
    {
        /// <summary>
        /// Gets or sets the animal identifier.
        /// </summary>
        /// <value>
        /// The animal identifier.
        /// </value>
        [Key]
        public int AnimalId { get; set; }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        /// <value>
        /// The name of the animal.
        /// </value>
        public string AnimalName { get; set; }

        /// <summary>
        /// Gets or sets the hunt start.
        /// </summary>
        /// <value>
        /// The hunt start.
        /// </value>
        public string HuntStart { get; set; }

        /// <summary>
        /// Gets or sets the hunt end.
        /// </summary>
        /// <value>
        /// The hunt end.
        /// </value>
        public string HuntEnd { get; set; }

        /// <summary>
        /// Gets or sets the extra detail.
        /// </summary>
        /// <value>
        /// The extra detail.
        /// </value>
        public string ExtraDetail { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is points animal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is points animal; otherwise, <c>false</c>.
        /// </value>
        public bool IsPointsAnimal { get; set; }

        /// <summary>
        /// Gets or sets the hunted animals.
        /// </summary>
        /// <value>
        /// The hunted animals.
        /// </value>
        public virtual ICollection<HuntedAnimal> HuntedAnimals { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return AnimalName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animal"/> class.
        /// </summary>
        public Animal()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Animal"/> class.
        /// </summary>
        /// <param name="animalName">Name of the animal.</param>
        /// <param name="huntStart">The hunt start.</param>
        /// <param name="huntEnd">The hunt end.</param>
        /// <param name="extraDetail">The extra detail.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="isPointsAnimal">if set to <c>true</c> [is points animal].</param>
        public Animal(string animalName, string huntStart, string huntEnd, string extraDetail, string imageUrl, bool isPointsAnimal)
        {
            AnimalName = animalName;
            HuntStart = huntStart;
            HuntEnd = huntEnd;
            ExtraDetail = extraDetail;
            ImageUrl = imageUrl;
            IsPointsAnimal = isPointsAnimal;
        }
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid { get => !string.IsNullOrEmpty(AnimalName); }
    }
}
