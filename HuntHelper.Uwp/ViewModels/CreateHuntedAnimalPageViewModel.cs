using HuntHelper.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace HuntHelper.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Template10.Mvvm.ViewModelBase" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class CreateHuntedAnimalPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// The path
        /// </summary>
        string path = @"http://localhost:61604/api/Image?filename=";
        
        /// <summary>
        /// The URI
        /// </summary>
        private string uri = "HuntedAnimals/";
        
        /// <summary>
        /// The image URL
        /// </summary>
        private string imageUrl = "ms-appx://HuntHelper.Uwp/DummyPicture/drawn-grizzly-bear-black-and-white-2.png";
        
        /// <summary>
        /// The file
        /// </summary>
        private StorageFile File;

        /// <summary>
        /// The visible
        /// </summary>
        private bool _Visible;
       
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreateHuntedAnimalPageViewModel"/> is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.
        /// </value>
        public bool Visible
        {
            get{return _Visible;}
            set
            {
                Set(ref _Visible, value);
            }
        }

        /// <summary>
        /// The hit
        /// </summary>
        private bool _Hit;
       
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CreateHuntedAnimalPageViewModel"/> is hit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hit; otherwise, <c>false</c>.
        /// </value>
        public bool Hit
        {
            get{return _Hit;}
            set
            {
                Set(ref _Hit, value);
            }
        }

        /// <summary>
        /// The ring visible
        /// </summary>
        private bool _RingVisible;
        
        /// <summary>
        /// Gets or sets a value indicating whether [ring visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ring visible]; otherwise, <c>false</c>.
        /// </value>
        public bool RingVisible
        {
            get{ return _RingVisible;}
            set
            {
                Set(ref _RingVisible, value);
            }
        }

        /// <summary>
        /// The image source
        /// </summary>

        private BitmapImage imageSource;
        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>
        /// The image source.
        /// </value>

        public BitmapImage ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// The date
        /// </summary>

        private DateTime _Date;
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date
        {
            get{ return _Date;}
            set
            {
                _Date = value;
                RaisePropertyChanged("Date");
            }
        }

        /// <summary>
        /// The hunter
        /// </summary>

        private Hunter _Hunter;
        /// <summary>
        /// Gets or sets the hunter.
        /// </summary>
        /// <value>
        /// The hunter.
        /// </value>
        public Hunter Hunter
        {
            get{ return _Hunter;}
            set
            {
                _Hunter = value;
                RaisePropertyChanged("Hunter");
            }
        }

        /// <summary>
        /// The animal
        /// </summary>

        private Animal _Animal;
        /// <summary>
        /// Gets or sets the animal.
        /// </summary>
        /// <value>
        /// The animal.
        /// </value>

        public Animal Animal
        {
            get { return _Animal; }
            set
            {
                Set(ref _Animal, value);
                
            }
        }

        /// <summary>
        /// The hunted animal
        /// </summary>
        private HuntedAnimal _HuntedAnimal;
        
        /// <summary>
        /// Gets or sets the hunted animal.
        /// </summary>
        /// <value>
        /// The hunted animal.
        /// </value>
        public HuntedAnimal HuntedAnimal
        {
            get { return _HuntedAnimal; }
            set
            {
                Set(ref _HuntedAnimal, value);
                RaisePropertyChanged("HuntedAnimal");
            }
        }

        /// <summary>
        /// The animals
        /// </summary>
        private ObservableCollection<Animal> _Animals = new ObservableCollection<Animal>();
        
        /// <summary>
        /// Gets or sets the animals.
        /// </summary>
        /// <value>
        /// The animals.
        /// </value>
        public ObservableCollection<Animal> Animals
        {
            get { return _Animals; }
            set
            {
                Set(ref _Animals, value);
                RaisePropertyChanged("Animals");
            }
        }

        /// <summary>
        /// The text
        /// </summary>
        private string _Text;
        
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return _Text; }
            set
            {
                Set(ref _Text, value);
                RaisePropertyChanged("Text");
            }
        }

        /// <summary>
        /// The error
        /// </summary>
        private bool _Error = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HunterPageViewModel"/> is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if error; otherwise, <c>false</c>.
        /// </value>
        public bool Error
        {
            get { return _Error; }
            set
            {
                Set(ref _Error, value);
                RaisePropertyChanged(nameof(Error));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHuntedAnimalPageViewModel"/> class.
        /// Constructor
        /// </summary>
        public CreateHuntedAnimalPageViewModel()
        {
           //User can use GUI
            Visible = true;
            Hit = true;
            Error = true;
            //gets the date for today
            Date = DateTime.Today;

            //Make a new instance of HuntedAnimal
            HuntedAnimal = new HuntedAnimal("http://localhost:61604/api/Image?filename=DummyPicture.png");
            
            //Create a new Bitmap
            ImageSource = new BitmapImage(new Uri(imageUrl));

            StartAsync();

        }

        /// <summary>
        /// Creates the object asynchronous.
        /// </summary>
        public async void CreateObjectAsync()
        {
            //set text to nothing
            Text = "";

            Hit = false;
            RingVisible = true;
            try
            {
                //Wait for GPS location 
                var position = await Task.WhenAll(GetPositionAsync());

                var lat = position[0].Coordinate.Point.Position.Latitude;
                var lon = position[0].Coordinate.Point.Position.Longitude;

                //Set the latitude and longitude on HuntedAnimal object
                HuntedAnimal.Latitude = lat;
                HuntedAnimal.Longitude = lon;
           
            }

            catch(Exception ex)
            {   
                //if users device dosent support location it will be sat to Oslo
                //Default Oslo
                HuntedAnimal.Latitude = 59.9;
                HuntedAnimal.Longitude = 10.75;
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));

            }

            finally
            {
                
                HuntedAnimal.DateTime = Date.ToString("MM/dd/yyyy");
             
                try
                {
                    // check if Huntedanimal have points or not.
                    if (HuntedAnimal.GetType().Name == "HuntedAnimalPoints")
                    {
                        HuntedAnimal = await ApiCall.Post<HuntedAnimalPoints>("HuntedAnimalsPoints/", HuntedAnimal);
                    }
                    else
                    {
                        HuntedAnimal = await ApiCall.Post<HuntedAnimal>("HuntedAnimals/", HuntedAnimal);
                    }
                    
                    // If user use own picture, post new picture.
                    if (HuntedAnimal.ImageUrl != "http://localhost:61604/api/Image?filename=DummyPicture.png")
                    {
                       
                        HuntedAnimal.ImageUrl = path + HuntedAnimal.HuntedAnimalId.ToString() + ".jpg";
                        await ApiCall.Update(uri, HuntedAnimal);
                        await Task.Run(() => PostImageAsync());
                    }
                    Hit = true;
                    RingVisible = false;

                    //navigate back to Home
                    NavigationService.Navigate(typeof(Views.HunterPage), HuntedAnimal);
                }

                //report error
                catch(Exception ex)
                {
                    await Task.Run(() => ReportError.ErrorAsync(ex.Message));
                    Text = "Det skjedde noe feil";
                }
            }
        }

        /// <summary>
        /// Gets the position asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task<Geoposition> GetPositionAsync()
        {
            var locator = new Geolocator
            {
                DesiredAccuracyInMeters = 50
            };
            var position = await locator.GetGeopositionAsync();

            return position;
        }

        /// <summary>
        /// Starts the asynchronous and get all aniamls.
        /// </summary>
        public async void StartAsync()
        {
            Animals = await ApiCall.Get<ObservableCollection<Animal>>("Animals/");
            if(Animals != null)
            {
                Error = false;
            }
            
        }
        /// <summary>
        /// Selecteds the animal asynchronous.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        public async void SelectedAnimalAsync(object sender, SelectionChangedEventArgs e)
        {
            //Den her begynte bare å slutte å fungere og fikk en rar feilmelding som sa det var feil med assembly. Derfor try catch her.
            try
            {
                Animal = (Animal)e.AddedItems[0];
            }

            catch(Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
            
            //convert the Huntedanimal to child or parent
            if (Animal.IsPointsAnimal)
            {
                var serializedParent = JsonConvert.SerializeObject(HuntedAnimal);
                HuntedAnimalPoints c = JsonConvert.DeserializeObject<HuntedAnimalPoints>(serializedParent);
                HuntedAnimal = c;
                Visible = true;
            }

            else
            {
                var serializedParent = JsonConvert.SerializeObject(HuntedAnimal);
                HuntedAnimal c = JsonConvert.DeserializeObject<HuntedAnimal>(serializedParent);
                HuntedAnimal = c;
                Visible = false;
            }       
     
        }

        /// <summary>
        /// User can only input number in textbox
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        public void OnlyNumbers(object sender, KeyRoutedEventArgs e)
        {
            if (sender.GetType().Name == "PointsText")
            {
                RaisePropertyChanged(nameof(HuntedAnimal.IsValid));
            }

            if (e.Key.ToString().Equals("Back"))
            {
                e.Handled = false;
                return;
                
            }
            for (int i = 0; i < 10; i++)
            {
                if (e.Key.ToString() == $"Number{i}")
                {
                    e.Handled = false;
                    return;
                  
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// Times the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DatePickerValueChangedEventArgs"/> instance containing the event data.</param>
        public void Time(object sender, DatePickerValueChangedEventArgs e)
        {
            Date = e.NewDate.Date;
        }

        /// <summary>
        /// Raises the <see cref="E:NavigatingFromAsync" /> event.
        /// </summary>
        /// <param name="args">The <see cref="NavigatingEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        /// <summary>
        /// Called when [navigated to asynchronous].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="suspensionState">State of the suspension.</param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Hunter = (Hunter)parameter;
           
           
            await Task.CompletedTask;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        public async void GetImage()
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
           
            picker.FileTypeFilter.Add(".jpg");
            try
            {
                File = await picker.PickSingleFileAsync();

                if (File.Path != null)
                {
                   
                    
                    //ImageSource = new BitmapImage(new Uri(file.Path, UriKind.Relative));

                    using (var stream = await File.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.SetSource(stream);
                        ImageSource = bitmap;
                        HuntedAnimal.ImageUrl = File.Path;
                        
                    }

                }
            }

            catch(Exception ex)
            {
                Text = "Det skjedde noe feil";
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
        }

        /// <summary>
        /// Posts the image asynchronous.
        /// </summary>
        public async void PostImageAsync()
        {
            try
            {
                var client = new HttpClient();
                byte[] byteData = await ConvertToByteData();
                var requestContent = new MultipartFormDataContent();
                //    here you can specify boundary if you need---^
                var imageContent = new ByteArrayContent(byteData);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                requestContent.Add(imageContent, "image", HuntedAnimal.HuntedAnimalId.ToString() + ".jpg");
                var test2 = await client.PostAsync("http://localhost:61604/api/Image", requestContent);
            }

            catch(Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
           
        }

        /// <summary>
        /// Converts to byte data.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> ConvertToByteData()
        {
            using (var inputStream = await File.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();

                var byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }

        /// <summary>
        /// Exits this instance.
        /// </summary>
        public void Exit()
        {
            CoreApplication.Exit();
        }

        /// <summary>
        /// Tries the again.
        /// </summary>
        public void TryAgain()
        {
            StartAsync();
        }
    }
}
