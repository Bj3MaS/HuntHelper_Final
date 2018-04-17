using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HuntHelper.Model
{
    /// <summary>
    /// HuntedAnimal calss
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HuntedAnimal : INotifyPropertyChanged
    {


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the hunted animal identifier.
        /// </summary>
        /// <value>
        /// The hunted animal identifier.
        /// </value>
        public int HuntedAnimalId { get; set; }

        /// <summary>
        /// The animal
        /// </summary>
        [Required]
        private Animal animal;
        /// <summary>
        /// Gets or sets the animal.
        /// </summary>
        /// <value>
        /// The animal.
        /// </value>
        public virtual Animal Animal{
            get { return animal; }
            set
            {
                animal = value;
                RaisePropertyChanged(nameof(Animal));
                RaisePropertyChanged(nameof(IsValid));
            }
        }


        /// <summary>
        /// Gets or sets the hunter.
        /// </summary>
        /// <value>
        /// The hunter.
        /// </value>
        public virtual Hunter Hunter { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }



        /// <summary>
        /// The weight
        /// </summary>
        private string weight;
        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                RaisePropertyChanged(nameof(Weight));
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        /// <summary>
        /// The bullet count
        /// </summary>
        private string bulletCount;
        /// <summary>
        /// Gets or sets the bullet count.
        /// </summary>
        /// <value>
        /// The bullet count.
        /// </value>
        public string BulletCount
        {
            get { return bulletCount; }
            set
            {
                bulletCount = value;
                RaisePropertyChanged(nameof(BulletCount));
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        /// <summary>
        /// The date time
        /// </summary>
        private string dateTime;
        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                RaisePropertyChanged(nameof(DateTime));
            }
        }

        /// <summary>
        /// The image URL
        /// </summary>
        private string imageUrl;
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                RaisePropertyChanged(nameof(ImageUrl));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuntedAnimal"/> class.
        /// </summary>
        public HuntedAnimal()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuntedAnimal"/> class.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="hunter">The hunter.</param>
        /// <param name="animal">The animal.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="bulletCount">The bullet count.</param>
        public HuntedAnimal( string imageUrl ,Hunter hunter = null, Animal animal = null , double latitude = 0, double longitude = 0, string  weight = null, string bulletCount = null )
        {
            ImageUrl = imageUrl;
            Hunter = hunter;
            Animal = animal;
            Latitude = latitude;
            Longitude = longitude;
            Weight = weight;
            BulletCount = bulletCount;
  
        }



        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsValid { get =>  !string.IsNullOrEmpty(bulletCount)  && !string.IsNullOrEmpty(weight) && Animal != null; }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

