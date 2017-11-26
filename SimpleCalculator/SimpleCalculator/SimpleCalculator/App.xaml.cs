using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace SimpleCalculator
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new SimpleCalculator.MainPage();

            AppCenter.Start("android=585cceb6-c48c-4b5b-9191-fa5c925463f6;" + "uwp=e3a39ca4-23f5-43e1-962d-b81b269263a5;" +
                   "ios=5e457cd7-d385-4505-adc7-3578d0154aaf;",
                   typeof(Analytics));
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
