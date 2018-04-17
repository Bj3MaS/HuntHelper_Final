using HuntHelper.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace HuntHelper.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class DeleteHuntedAnimalCommand : ICommand
    {
        /// <summary>
        /// The view model
        /// </summary>
        private HunterPageViewModel _ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAuthorCommand"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public DeleteHuntedAnimalCommand(HunterPageViewModel ViewModel)
        {
            _ViewModel = ViewModel;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter) => parameter != null;

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public async void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                await _ViewModel.DeleteObject();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Template10.Mvvm.ViewModelBase" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HunterPageViewModel : ViewModelBase, INotifyPropertyChanged
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HuntedAnimal">The hunted animal.</param>
        /// <returns></returns>
        delegate int GetNumber(HuntedAnimal HuntedAnimal);

        /// <summary>
        /// The path
        /// </summary>
        private string path = "HuntedAnimals/";

        //private HuntedAnimal SelectedAnimal { get; set; }
        //public ObservableCollection<Animal> testlist = new ObservableCollection<Animal>();
        //public BasicGeoposition test
        /// <summary>
        /// The timer
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 500) };
        /// <summary>
        /// The timer update
        /// </summary>
        DispatcherTimer timerUpdate = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
        /// <summary>
        /// The total time
        /// </summary>
        private int totalTime;
        /// <summary>
        /// The total length
        /// </summary>
        private int TotalLength;

        /// <summary>
        /// Gets or sets the delete author command.
        /// </summary>
        /// <value>
        /// The delete author command.
        /// </value>
        public ICommand DeleteHuntedAnimalCommand { get; set; }

        /// <summary>
        /// The hunted animals
        /// </summary>
        private ObservableCollection<HuntedAnimal> _HuntedAnimals = new ObservableCollection<HuntedAnimal>();
        /// <summary>
        /// Gets or sets the hunted animals.
        /// </summary>
        /// <value>
        /// The hunted animals.
        /// </value>
        public ObservableCollection<HuntedAnimal> HuntedAnimals
        {
            get { return _HuntedAnimals; }
            set{ Set(ref _HuntedAnimals, value); }
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
                RaisePropertyChanged(nameof(HuntedAnimal));
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
            set { Set(ref _Animals, value); }
        }

        /// <summary>
        /// The resultat
        /// </summary>
        private ObservableCollection<Animal> _Resultat = new ObservableCollection<Animal>();
        /// <summary>
        /// Gets or sets the resultat.
        /// </summary>
        /// <value>
        /// The resultat.
        /// </value>
        public ObservableCollection<Animal> Resultat
        {
            get { return _Resultat; }
            set { Set(ref _Resultat, value); }
        }

        /// <summary>
        /// The search word
        /// </summary>
        private string _SearchWord;
        /// <summary>
        /// Gets or sets the search word.
        /// </summary>
        /// <value>
        /// The search word.
        /// </value>
        public string SearchWord
        {
            get { return _SearchWord; }
            set
            {
                Set(ref _SearchWord, value);
                RaisePropertyChanged("SearcWord");
            }
        }

        /// <summary>
        /// The map center
        /// </summary>
        private Geopoint _MapCenter;
        /// <summary>
        /// Gets or sets the map center.
        /// </summary>
        /// <value>
        /// The map center.
        /// </value>
        public Geopoint MapCenter
        {
            get { return _MapCenter; }
            set { Set(ref _MapCenter, value); }
        }

        /// <summary>
        /// The posistion
        /// </summary>
        private Geopoint _posistion;
        /// <summary>
        /// Gets or sets the posistion.
        /// </summary>
        /// <value>
        /// The posistion.
        /// </value>
        public Geopoint Posistion
        {
            get { return _posistion; }
            set { Set(ref _posistion, value); }
        }

        /// <summary>
        /// The position
        /// </summary>
        private Geopoint _Position = new Geopoint(new BasicGeoposition());
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Geopoint Position
        {
            get { return _Position; }
            set
            {
                Set(ref _Position, value);
                RaisePropertyChanged("Position");
            }
        }

        /// <summary>
        /// The map zoom level
        /// </summary>
        private double _mapZoomLevel;
        /// <summary>
        /// Gets or sets the map zoom level.
        /// </summary>
        /// <value>
        /// The map zoom level.
        /// </value>
        public double MapZoomLevel
        {
            get { return _mapZoomLevel; }
            set { Set(ref _mapZoomLevel, value); }
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
            get { return _Error;  }
            set
            {
                Set(ref _Error, value);
                RaisePropertyChanged(nameof(Error));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HunterPageViewModel"/> class.
        /// </summary>
        public HunterPageViewModel()
        {
            DeleteHuntedAnimalCommand = new DeleteHuntedAnimalCommand(this);
            StartAsync();
        }

        /// <summary>
        /// Starts the asynchronous.
        /// </summary>
        public async void StartAsync()
        {
            Error = true;
            HttpClient client = new HttpClient();

            //Convert to parent or child from a json string
            try
            {
                HuntedAnimals.Clear();

                var jArray = JArray.Parse(await client.GetStringAsync(new Uri("http://localhost:61604/api/HuntedAnimal/Hunter/1")));

                for (var i = 0; i < jArray.Count; i++)
                {
                   
                    if ((bool)jArray[i]["Animal"]["IsPointsAnimal"])
                    {
                        HuntedAnimals.Add((jArray[i] as JObject).ToObject<HuntedAnimalPoints>());
                    }
                    else
                    {
                        HuntedAnimals.Add((jArray[i] as JObject).ToObject<HuntedAnimal>());
                    }
                }
            }

            //Send the Excepetion to a file
            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }

            //Get animal 
            Animals = await ApiCall.Get<ObservableCollection<Animal>>("Animals");

            if (HuntedAnimals != null && Animals != null)
            {
                Error = false;
                TotalLength = HuntedAnimals.Count();
                
            }
        }

        /// <summary>
        /// Hunteds the animal selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        public void HuntedAnimalSelected(object sender, ItemClickEventArgs e)
        {
          
            if (e.ClickedItem.ToString() == "HuntedAnimalPoints")
            {
                HuntedAnimal = (HuntedAnimalPoints)e.ClickedItem;
            }

            else
            {
                HuntedAnimal = (HuntedAnimal)e.ClickedItem;
            }

     

            Position = new Geopoint(new BasicGeoposition() { Latitude = HuntedAnimal.Latitude, Longitude = HuntedAnimal.Longitude });


            var PositionOnMap = new BasicGeoposition() { Latitude = HuntedAnimal.Latitude, Longitude = HuntedAnimal.Longitude };
        
            Posistion = new Geopoint(PositionOnMap);

        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <returns></returns>
        public async Task DeleteObject()
        {
            var StatusCode = await ApiCall.Delete(path, HuntedAnimal.HuntedAnimalId);
            //Value = StatusCode.ToString();

            if (StatusCode.ToString() == "OK")
            {
                //var temp = Animal;
                //Animal = null;
                HuntedAnimals.Remove(HuntedAnimal);

            }
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateObject()
        {
            if (!await ApiCall.Update(path, HuntedAnimal))
            {
                Error = true;
            }
        }

        /// <summary>
        /// Creates the object.
        /// </summary>
        public async void CreateObject()
        {
            var Hunter = await ApiCall.Get<Hunter>("Hunters/1");
            NavigationService.Navigate(typeof(Views.CreateHuntedAnimalPage), Hunter);
 
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

            await Task.CompletedTask;
        }

        /// <summary>
        /// Handles the Loaded event of the Map control. And set the pinpoint in the center of the map.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Windows.UI.Xaml.RoutedEventArgs"/> instance containing the event data.</param>
        public async void Map_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
           
            try
            {
                var locator = new Geolocator();
               
                var position = await locator.GetGeopositionAsync();
                var PositionOnMap = new BasicGeoposition() { Latitude = position.Coordinate.Point.Position.Latitude, Longitude = position.Coordinate.Point.Position.Longitude };


                MapCenter = new Geopoint(PositionOnMap);
                MapZoomLevel = 14;
                Posistion = new Geopoint(PositionOnMap);

            }

            catch (Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
                var PositionOnMap = new BasicGeoposition() { Latitude = 59.9, Longitude = 10.75 };
                MapCenter = new Geopoint(PositionOnMap);
                MapZoomLevel = 14;
                Posistion = new Geopoint(PositionOnMap);
            }
        }

        /// <summary>
        /// Starts the search.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        public void StartSearch(object sender, KeyRoutedEventArgs e)
        {
            e.OriginalSource.ToString();
            totalTime = 0;
            timer.Stop();

            if (e.Key.ToString() == "Enter")
            {
                timer.Stop();
                Resultat.Clear();
                SendSearchAsync(SearchWord);
            }

            else
            {
                timer.Start();
                timer.Tick += TickAsync;
              
            }
          
        }

        /// <summary>
        /// Ticks the asynchronous.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void TickAsync(object sender, object e)
        {
            Resultat.Clear();
            
            if (totalTime > 1)
            {
                timer.Stop();
                if (SearchWord != "")
                {
                    var list = Animals.Where(it => it.AnimalName.Contains(SearchWord.Substring(0, 1).ToUpper() + SearchWord.Substring(1)));
               
                    foreach (var item in list)
                    {

                      Resultat.Add(item);
                    }

                    timer.Stop();
                }
            
                else if (TotalLength != HuntedAnimals.Count())
                {
                    try
                    {
                        HuntedAnimals = await ApiCall.Get<ObservableCollection<HuntedAnimal>>("HuntedAnimal/Hunter/1");
                        timer.Stop();
                    }

                    catch(Exception ex)
                    {
                        await Task.Run(() => ReportError.ErrorAsync(ex.Message));
                    }
                }
            }
            totalTime++;   
        }

        /// <summary>
        /// Starts the update.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        public void StartUpdate(object sender, KeyRoutedEventArgs e)
       {
            totalTime = 0;
            timerUpdate.Stop();
            timerUpdate.Start();
            timerUpdate.Tick += TickUpdateAsync;


        }

        /// <summary>
        /// Ticks the update asynchronous.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private async void TickUpdateAsync(object sender, object e)
        {        
            if (totalTime > 1)
            {
                try
                {
                    timer.Stop();
                    await UpdateObject();
                }

                catch (Exception ex)
                {
                    await Task.Run(() => ReportError.ErrorAsync(ex.Message));
                }

                finally
                {
                    timerUpdate.Stop();
                }
              
            }
            else
            {

                totalTime++;
            }

        }

        /// <summary>
        /// Searches the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AutoSuggestBoxQuerySubmittedEventArgs"/> instance containing the event data.</param>
        public void Search(object sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SendSearchAsync(args.QueryText);
        }

        /// <summary>
        /// Sends the search asynchronous.
        /// </summary>
        /// <param name="searchword">The searchword.</param>
        private async void SendSearchAsync(string searchword)
        {
            HuntedAnimals = await ApiCall.Get<ObservableCollection<HuntedAnimal>>($"HuntedAnimals/Search/{searchword}");
        }

        /// <summary>
        /// Gets the hunted animal highest.
        /// </summary>
        /// <param name="huntedAnimals">The hunted animals.</param>
        /// <param name="getNumber">The get number.</param>
        /// <returns></returns>
        private ObservableCollection<HuntedAnimal> GetHuntedAnimalHighest(HuntedAnimal[] huntedAnimals, GetNumber getNumber)
        {
            ObservableCollection<HuntedAnimal> HuntedAnimal = new ObservableCollection<HuntedAnimal>();
            int HighestNumber = 0;
            foreach (HuntedAnimal huntedAnimal in huntedAnimals)
            {
                int Number = getNumber(huntedAnimal);
                if (Number > HighestNumber)
                {
                    HuntedAnimal.Clear();
                    HighestNumber = Number;
                    HuntedAnimal.Add(huntedAnimal);
                }
                else if(Number == HighestNumber)
                {
                    HuntedAnimal.Add(huntedAnimal);
                }
            }
   
            return HuntedAnimal;
        }

        /// <summary>
        /// Gets the highest weight button asynchronous.
        /// </summary>
        public async void GetHighestWeightButtonAsync()
        {
            
            HuntedAnimals = await ApiCall.Get<ObservableCollection<HuntedAnimal>>("HuntedAnimal/Hunter/1");
            if(HuntedAnimals != null)
            {
                HuntedAnimals = GetHuntedAnimalHighest(HuntedAnimals.ToArray(), GetHuntedAnimalWeight);
                Error = false;
            }

            else
            {
                Error = true;
            }
        }

        /// <summary>
        /// Gets the highest bullet count button asynchronous.
        /// </summary>
        public async void GetHighestBulletCountButtonAsync()
        {
           
            HuntedAnimals = await ApiCall.Get<ObservableCollection<HuntedAnimal>>("HuntedAnimal/Hunter/1");

            if(HuntedAnimals == null)
            {
                Error = true;
            }
            else
            {
                HuntedAnimals = GetHuntedAnimalHighest(HuntedAnimals.ToArray(), GetHuntedAnimalBulletCount);
            }
        }



        /// <summary>
        /// Gets the hunted animal bullet count.
        /// </summary>
        /// <param name="huntedAnimal">The hunted animal.</param>
        /// <returns></returns>
        private int GetHuntedAnimalBulletCount(HuntedAnimal huntedAnimal)
        {
            try
            {
                return Int32.Parse(huntedAnimal.BulletCount);
            }

            catch
            {
                return 0;
            }
           
        }

        /// <summary>
        /// Gets the hunted animal weight.
        /// </summary>
        /// <param name="huntedAnimal">The hunted animal.</param>
        /// <returns></returns>
        private int GetHuntedAnimalWeight(HuntedAnimal huntedAnimal)
        {
            try
            {
                return Int32.Parse(huntedAnimal.Weight);
            }
            
            catch
            {
                return 0;
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

        /// <summary>
        /// Called when [numbers].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        public void OnlyNumbers(object sender, KeyRoutedEventArgs e)
        {
           
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
        /// Gets the full hunted animal list asynchronous.
        /// </summary>
        public async void GetFullHuntedAnimalListAsync()
        {
            HuntedAnimals = await ApiCall.Get<ObservableCollection<HuntedAnimal>>("HuntedAnimal/Hunter/1");

            if (HuntedAnimals != null)
            {
                Error = false;
            }
        }
    }
}


