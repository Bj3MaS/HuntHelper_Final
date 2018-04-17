using HuntHelper.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace HuntHelper.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Template10.Mvvm.ViewModelBase" />
    public class MachineLearningPageViewModel : ViewModelBase
    {
        /// <summary>
        /// The file
        /// </summary>
        private StorageFile file;

        /// <summary>
        /// The image source
        /// </summary>
        private BitmapImage _ImageSource;
        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>
        /// The image source.
        /// </value>
        public BitmapImage ImageSource
        {
            get{return _ImageSource;}
            set
            {
                _ImageSource = value;
                RaisePropertyChanged(nameof(ImageSource));
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
            get{ return _Text; }
            set
            {
                _Text = value;
                RaisePropertyChanged(nameof(Text));
            }
        }

        /// <summary>
        /// The error
        /// </summary>
        private bool _Error = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MachineLearningPageViewModel"/> is error.
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
        /// Initializes a new instance of the <see cref="MachineLearningPageViewModel"/> class.
        /// </summary>
        public MachineLearningPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
            }

        }

        /// <summary>
        /// Starts the machine learning asynchronous.
        /// </summary>
        public async void StartMachineLearningAsync()
        {

            try
            {
                FileOpenPicker picker = new FileOpenPicker
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.PicturesLibrary
                };
                picker.FileTypeFilter.Add(".jpg");
                file = await picker.PickSingleFileAsync();

                MessageDialog dlg;
                string filePath = file.Path;
   
                using (var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(stream);
                    ImageSource = bitmap;
                
                }
           


                await Task.Run(() => MakePredictionRequest(file));
            }

            catch(Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
            }
        }

        /// <summary>
        /// Gets the image as byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file path.</param>
        /// <returns></returns>
        private byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        /// <summary>
        /// Converts to image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public async Task<byte[]> ConvertToImage (StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                var readStream = inputStream.AsStreamForRead();

                var byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }
        }


        /// <summary>
        /// Makes the prediction request.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private async Task MakePredictionRequest(StorageFile file)
        {
            try
            {
                var client = new HttpClient();

                // Request headers - replace this example key with your valid subscription key.
                client.DefaultRequestHeaders.Add("Prediction-Key", "4b013eec09d5460cb3cbd3178a91dbe0");

                // Prediction URL - replace this example URL with your valid prediction URL.
                string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/08cbc22c-7aa3-43f3-8363-d4895ee306aa/image?iterationId=f635df50-9c4a-4ed2-ad62-1875f2402139";
                HttpResponseMessage response;

                // Request body. Try this sample with a locally stored image.
                byte[] byteData = await ConvertToImage(file);

                JToken[] memberName;
                using (var content = new ByteArrayContent(byteData))
                {

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(url, content);
                    var stuff = (await response.Content.ReadAsStringAsync());                
                    var test3 = JObject.Parse(stuff);           
                    memberName = test3["Predictions"].ToArray();    
                }

                Text = "Bilde inneholder følgende dyr: ";
                foreach (JToken jt in memberName)
                {
                    if (Double.Parse(jt["Probability"].ToString()) >= 0.95)
                        Text += jt["Tag"] + " ";
                }
            }
            catch(Exception ex)
            {
                await Task.Run(() => ReportError.ErrorAsync(ex.Message));
                Error = true;
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
        /// Tries the again asynchronous.
        /// </summary>
        public async void TryAgainAsync()
        {
            Error = false;
            await Task.Run(() => MakePredictionRequest(file));
        }
    }
}
