using AgeMe.Models;
using Android.Graphics;
using FaceRecognitionLibrary;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AgeMe
{
    public partial class MainPage : ContentPage
    {
        private readonly FaceRecognition faceRecognition;

        public MainPage()
        {
            InitializeComponent();
            this.faceRecognition = new FaceRecognition("<API KEY>");
        }

        private async void TakePictureButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = false
            });

            if (file == null)
                return;

            this.Indicator1.IsVisible = true;
            this.Indicator1.IsRunning = true;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());

            var theData = await faceRecognition.UploadAndDetectFaces(file);

            this.Indicator1.IsRunning = false;
            this.Indicator1.IsVisible = false;

           var bitmapSource =  DependencyService.Get<IDrawFaces>().DrawFaces(file,theData);

            Image1.Source = ImageSource.FromStream(() => bitmapSource);

        }
    }
}
