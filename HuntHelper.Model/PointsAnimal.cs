using System.ComponentModel;
using Windows.UI.Xaml.Media.Imaging;

namespace HuntHelper.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="HuntHelper.Model.Animal" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class PointsAnimal : Animal, INotifyPropertyChanged
    {


        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


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
        /// Initializes a new instance of the <see cref="PointsAnimal"/> class.
        /// </summary>
        public PointsAnimal()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointsAnimal"/> class.
        /// </summary>
        /// <param name="animalName">Name of the animal.</param>
        /// <param name="huntEnd">The hunt end.</param>
        /// <param name="huntStart">The hunt start.</param>
        /// <param name="extraDetail">The extra detail.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <param name="isPointsAnimal">if set to <c>true</c> [is points animal].</param>
        /// <param name="points">The points.</param>
        public PointsAnimal(string animalName, string huntEnd, string huntStart, string extraDetail, string imageUrl, bool isPointsAnimal, string points) : base ( animalName, huntEnd, huntStart, extraDetail, imageUrl, isPointsAnimal)
        {
            
            AnimalName = animalName;
            HuntEnd = huntEnd;
            HuntStart = huntStart;
            ExtraDetail = extraDetail;
            ImageUrl = imageUrl;
        }  

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public override bool IsValid { get => !string.IsNullOrEmpty(AnimalName) && !string.IsNullOrEmpty(Points); }

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
