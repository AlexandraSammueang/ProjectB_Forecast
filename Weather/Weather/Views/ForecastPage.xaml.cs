using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Weather.Models;
using Weather.Services;
using System.Globalization;

namespace Weather.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ForecastPage : ContentPage
    {
        OpenWeatherService service;
     

        public ForecastPage()
        {
            InitializeComponent();

            service = new OpenWeatherService();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MainThread.BeginInvokeOnMainThread(async () => { await LoadForecast(); });
        }

        private async Task LoadForecast()
        {

            Forecast t1 = await service.GetForecastAsync(Title);

            {

                t1.Items.ForEach(x => x.Icon = $"http://openweathermap.org/img/wn/{x.Icon}@2x.png");


                WeatherListView.ItemsSource = t1.Items.GroupBy(x => x.DateTime.Date);

            };

        }

        private async void RefreshPage(object sender, EventArgs args)
        {

            await LoadForecast();

        }

    }
}