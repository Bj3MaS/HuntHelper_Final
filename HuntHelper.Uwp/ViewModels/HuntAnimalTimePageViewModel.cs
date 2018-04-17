using HuntHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace HuntHelper.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Template10.Mvvm.ViewModelBase" />
    class HuntAnimalTimePageViewModel : ViewModelBase
    {

        /// <summary>
        /// The animal
        /// </summary>
        private Animal _Animal;
        /// <summary>
        /// The animals
        /// </summary>
        private ObservableCollection<Animal> _Animals = new ObservableCollection<Animal>();
        /// <summary>
        /// The timer
        /// </summary>
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 500) };
        /// <summary>
        /// The CTS
        /// </summary>
        private CancellationTokenSource cts;


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
            get{ return _Text;}
            set
            {
                Set(ref _Text, value);
                RaisePropertyChanged("Text");
            }
        }

        /// <summary>
        /// The visible
        /// </summary>
        private bool _Visible;       
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HuntAnimalTimePageViewModel"/> is visible.
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
        /// The revert visible
        /// </summary>
        private bool _RevertVisible;
        /// <summary>
        /// Gets or sets a value indicating whether [revert visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [revert visible]; otherwise, <c>false</c>.
        /// </value>
        public bool RevertVisible
        {
            get { return _RevertVisible; }
            set
            {
                Set(ref _RevertVisible, value);
            }
        }

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
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HuntAnimalTimePageViewModel"/> is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if error; otherwise, <c>false</c>.
        /// </value>
        private bool _Error;
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
        /// Initializes a new instance of the <see cref="HuntAnimalTimePageViewModel"/> class.
        /// </summary>
        public HuntAnimalTimePageViewModel()
        {
            Error = true;
            Visible = true;
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
               
            }


            StartAsync();

        }



        /// <summary>
        /// Starts the asynchronous. Send Get call to database.
        /// </summary>
        public async void StartAsync()
        {
            Animals = await ApiCall.Get<ObservableCollection<Animal>>("Animals"); 
            if(Animals != null)
            {
                Error = false;
            }
        }


        /// <summary>
        /// Tests the ListView.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        public void TestListView(object sender, SelectionChangedEventArgs e)
        {
            //Set the Animal from Selected ComboBox
            Animal = (Animal)e.AddedItems[0];

            GotoDetailsPage();

        }

        /// <summary>
        /// Gotoes the details page.
        /// </summary>
        public void GotoDetailsPage()
        {
            NavigationService.Navigate(typeof(Views.HuntAnimalTimeDetailPage), Animal);
        }

        /// <summary>
        /// Slows the search asynchronous.
        /// </summary>
        public async void SlowSearchAsync()
        {
            cts = new CancellationTokenSource();
            Visible = false;
            RevertVisible = true;
            
            
            try
            {
                await Task.Delay(50000, cts.Token);
                if (Text == "" || Text == null)
                {
                   
                    Animals = await ApiCall.Get<ObservableCollection<Animal>>($"Animals");
                }
                else
                {
                    Animals = await ApiCall.Get<ObservableCollection<Animal>>($"Animals/Search/{Text}");
                }
               
            }

            catch (OperationCanceledException ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }

            finally
            {
                Visible = true;
                RevertVisible = false;
            }
        }

        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        public async void SearchAsync()
        {
            cts = new CancellationTokenSource();
            Visible = false;
            RevertVisible = true;


            try
            {
                if (Text == "" || Text == null)
                {

                    Animals = await ApiCall.Get<ObservableCollection<Animal>>($"Animals");
                }
                else
                {
                    Animals = await ApiCall.Get<ObservableCollection<Animal>>($"Animals/Search/{Text}");
                }

            }

            catch (OperationCanceledException ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }

            finally
            {
                Visible = true;
                RevertVisible = false;
            }
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            //timer.Stop();
            this.cts.Cancel();
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