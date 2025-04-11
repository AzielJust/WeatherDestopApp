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
using Weather_Application_v1.Models;


namespace Weather_Application_v1
{
    public partial class HomeScreen: Form
    {
        string geoapifyAPIKey = "7da95d2324c84ea18d57f28800ad8993";
        string openWeatherAPIKey = "d04041825617e22140b626f6392e0c81";
        string stormglassAPIKey = "a6f378b6-1561-11f0-a906-0242ac130003-a6f37910-1561-11f0-a906-0242ac130003";
        
        double longitude;
        double latitude;
        string formatedLocation;
        public HomeScreen()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void formatLocation()
        {
            string encoded = HomeScreenSearchBar.Text.Replace(" ", "%");
            formatedLocation = encoded;
        }
        void getBeachLocation()
        {
            using (WebClient wc = new WebClient())
            {
                string geoapifyURL = string.Format("https://api.geoapify.com/v1/geocode/search?text=" +
                                           formatedLocation + "&apiKey=" + geoapifyAPIKey +
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
                    HomeScreenBeachName.Text = firstResult.Properties.Formatted;
            
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
                var json = wc.DownloadString(openWeatherURL);
                weatherdetails_model.root weatherData = JsonConvert.DeserializeObject<weatherdetails_model.root>(json);
                
                if (weatherData != null)
                {
                    Console.WriteLine($"Temperature: {weatherData.main.temp}°C");
                    Console.WriteLine($"Conditions: {weatherData.weather[0].description}");
                    Console.WriteLine($"Icon: {weatherData.weather[0].icon}");
                    Console.WriteLine($"Wind Speed: {weatherData.wind.speed} m/s");
                    Console.WriteLine(openWeatherURL);
                    
                    HomeScreenForcastImage.Load(string.Format("https://openweathermap.org/img/wn/" + weatherData.weather[0].icon + "@4x.png"));
                    HomeScreenBeachTemp.Text = weatherData.main.temp + " °C";
                    HomeScreenConditionWindSpeed.Text = weatherData.wind.speed + " m/s";
                    HomeScreenConditionTemp.Text = weatherData.main.temp + " °C";
                    HomeScreenOverview.Text = weatherData.weather[0].description;
                }
            }
        }
        void GetWaveDetails()
        {
            using (WebClient wc = new WebClient())
            {
                string marineAPIURL = $"https://marine-api.open-meteo.com/v1/marine?latitude={latitude}&longitude={longitude}&current=swell_wave_height,wave_height";
                var json = wc.DownloadString(marineAPIURL);
                marinedata_model.root marineData = JsonConvert.DeserializeObject<marinedata_model.root>(json);

                Console.WriteLine("Swell Size: " + marineData.current.swell_wave_height);
                HomeScreenConditionSwellSize.Text = (marineData.current.swell_wave_height);
            }
        }
        
        private void DisplayFiveDayForecast(List<DailyForecast> forecasts)
        {
            // Clear existing forecasts
            ForecastUC.Controls.Clear();

            foreach (var forecast in forecasts)
            {
                // Create new instance of your user control
                var forecastControl = new ForecastPanel();
        
                // Set the control properties
                forecastControl.ForecastDate.Text = forecast.Date;
                forecastControl.ForecastTemp.Text = $"{forecast.MinTemp}°C / {forecast.MaxTemp}°C";
        
                // Load weather icon (example using OpenWeatherMap icons)
                string iconUrl = $"http://openweathermap.org/img/wn/{forecast.Icon}@2x.png";
                forecastControl.ForecastIcon.LoadAsync(iconUrl);
        
                // Add to flow panel
                ForecastUC.Controls.Add(forecastControl);
            }
        }
        
        void GetFiveDayForecast()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string marineAPIURL = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={openWeatherAPIKey}&units=metric";
                    var json = wc.DownloadString(marineAPIURL);
                    var forecastData = JsonConvert.DeserializeObject<five_day_forecast_model.root>(json);
    
                    var dailyForecasts = forecastData.list
                        .GroupBy(item => DateTime.Parse(item.dt_txt).Date)
                        .Select(group => group
                            .OrderBy(item => Math.Abs(DateTime.Parse(item.dt_txt).Hour - 12))
                            .First())
                        .Take(5)
                        .Select(item => new DailyForecast 
                        {
                            Date = DateTime.Parse(item.dt_txt).ToString("ddd, MMM dd"), // More readable format
                            Icon = item.weather.First().icon,
                            MinTemp = Math.Round(item.main.temp_min),
                            MaxTemp = Math.Round(item.main.temp_max)
                        })
                        .ToList();
            
                    // Display in UI
                    DisplayFiveDayForecast(dailyForecasts);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load forecast: {ex.Message}");
            }
        }
        
        private void HomeScreenSearchButton_Click(object sender, EventArgs e)
        {
            formatLocation();
            getBeachLocation();
            getWeatherDetails();
            GetWaveDetails();
            GetFiveDayForecast();
        }

        private void ControlPanelHomeButton_Click(object sender, EventArgs e)
        {
            HomeScreenViewPanel.Visible = false;
            HomeScreenViewPanel.Enabled = false;
        }
    }
}
