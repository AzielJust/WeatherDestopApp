using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;


namespace Weather_Application_v1
{
    public partial class HomeScreen: Form
    {
        string geoapifyAPIKey = "7da95d2324c84ea18d57f28800ad8993";
        private string openWeatherAPIKey = "d04041825617e22140b626f6392e0c81";
        
        double longitude;
        double latitude;
        public HomeScreen()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void getBeachLocation()
        {
            using (WebClient wc = new WebClient())
            {
                string geoapifyURL = string.Format("https://api.geoapify.com/v1/geocode/search?text=" +
                                           HomeScreenSearchBar.Text + "%20Beach%20Barbados&apiKey=" + geoapifyAPIKey +
                                           "&fields=lon,lat");
        
                var jsonResult = wc.DownloadString(geoapifyURL);
                GeoapifyResponse response = JsonConvert.DeserializeObject<GeoapifyResponse>(jsonResult);

                if (response?.Features?.Count > 0)
                {
                    // Access the first result
                    var firstResult = response.Features[0];
            
                    // Get coordinates (two ways)
                    longitude = firstResult.Geometry.Coordinates[0];
                    latitude = firstResult.Geometry.Coordinates[1];
            
                    // Use the data as needed
                    Console.WriteLine(longitude + "," + latitude);
                }
            }
        }

        void getWeatherDetails()
        {
            using (WebClient wc = new WebClient())
            {
                string openWeatherURL = string.Format("https://api.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&appid=" + openWeatherAPIKey + "&units=metric");
                var jsonResult = wc.DownloadString(openWeatherURL);
                WeatherResponse weatherData = JsonConvert.DeserializeObject<WeatherResponse>(jsonResult);
                
                if (weatherData != null && weatherData.Cod == 200)
                {
                    Console.WriteLine($"Temperature: {weatherData.Main.Temperature}°C");
                    Console.WriteLine($"Conditions: {weatherData.Weather[0].Description}");
                    Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} m/s");
                }
            }
        }
        private void HomeScreenSearchButton_Click(object sender, EventArgs e)
        {
            getBeachLocation();
            getWeatherDetails();
        }
    }
}
