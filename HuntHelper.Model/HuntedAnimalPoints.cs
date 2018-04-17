using System.ComponentModel;

namespace HuntHelper.Model
{
    /// <summary>
    /// Child of HuntedAnimals, Some animals have trophy points
    /// </summary>
    /// <seealso cref="HuntHelper.Model.HuntedAnimal" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HuntedAnimalPoints : HuntedAnimal, INotifyPropertyChanged
    {

        /// <summary>
        /// The points
        /// </summary>
        private string points;
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public string Points
        {
            get { return points; }
            set
            {
                points = value;
                RaisePropertyChanged(nameof(Points));
                RaisePropertyChanged(nameof(IsValid));
                
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="HuntedAnimalPoints"/> class.
        /// </summary>
        public HuntedAnimalPoints()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuntedAnimalPoints"/> class.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="hunter">The hunter.</param>
        /// <param name="animal">The animal.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="bulletCount">The bullet count.</param>
        /// <param name="points">The points.</param>
        public HuntedAnimalPoints(string imageUrl, Hunter hunter = null, Animal animal= null, double latitude = 0, double longitude = 0, string weight = null, string bulletCount = null, string points = null) : base (imageUrl,hunter, animal, latitude, longitude, weight, bulletCount)
        {
            ImageUrl = imageUrl;
            Hunter = hunter;
            Animal = animal;
            Latitude = latitude;
            Longitude = longitude;
            Weight = weight;
            BulletCount = bulletCount;
            Points = points;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public override bool IsValid { get => !string.IsNullOrEmpty(BulletCount) && !string.IsNullOrEmpty(Weight) && !string.IsNullOrEmpty(Points) && Animal != null; }


    }
}
