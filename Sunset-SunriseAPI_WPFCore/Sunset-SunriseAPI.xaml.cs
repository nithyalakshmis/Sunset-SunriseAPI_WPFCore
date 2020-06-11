using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Sunset_SunriseAPI_WPFCore
{
    /// <summary>
    /// Interaction logic for Sunset_SunriseAPI.xaml
    /// </summary>


    public partial class Sunset_SunriseAPI : Window
    {
        static HttpClient client = new HttpClient();
        public Sunset_SunriseAPI()
        {
            InitializeComponent();
        }

        private async void btnGetdata_Click(object sender, RoutedEventArgs e)
        {
            Application application = await GetData("https://api.sunrise-sunset.org/json?lat="+txtLatitude.Text+"&lng="+txtLongitute.Text);
            lblSunrise.Content = Convert.ToDateTime(application.results.sunrise).ToLocalTime().ToString("t");
            lblSunset.Content = Convert.ToDateTime(application.results.sunset).ToLocalTime().ToString("t");


        }

        public class Results
        {
            public string sunrise { get; set; }
            public string sunset { get; set; }
        }
        public class Application
        {
            public Results results { get; set; }
        }

        static async Task<Application> GetData(string url)
        {
            Application application = null;
            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                application = await httpResponseMessage.Content.ReadAsAsync<Application>();
            }
            return application;
        }
    }
}
