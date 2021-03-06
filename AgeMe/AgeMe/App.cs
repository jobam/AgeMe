﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AgeMe
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            var content = new ContentPage
            {
                Title = "AgeMe",
            };

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            MainPage mainPage = MainPage as MainPage;

            mainPage.TakePicture();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
