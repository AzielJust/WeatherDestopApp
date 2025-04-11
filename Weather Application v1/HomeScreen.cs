using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

        private string unitsOfMeasurement = "metric";
        private string unitsOfMeasurmentWindSymbol = " kph";
        private string unitsOfMeasurmentSymbol = " m";
        private string unitsOfMeasurmentTempSymbol = " °c";
        
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
                string openWeatherURL = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={openWeatherAPIKey}&units={unitsOfMeasurement}";
                var json = wc.DownloadString(openWeatherURL);
                weatherdetails_model.root weatherData = JsonConvert.DeserializeObject<weatherdetails_model.root>(json);
                
                if (weatherData != null)
                {
                    HomeScreenForcastImage.Load(string.Format("https://openweathermap.org/img/wn/" + weatherData.weather[0].icon + "@4x.png"));
                    HomeScreenBeachTemp.Text = weatherData.main.temp + unitsOfMeasurmentTempSymbol;
                    HomeScreenConditionWindSpeed.Text = weatherData.wind.speed + unitsOfMeasurmentWindSymbol;
                    HomeScreenConditionTemp.Text = weatherData.main.temp + unitsOfMeasurmentTempSymbol;
                    HomeScreenOverview.Text = weatherData.weather[0].description;
                }
            }
        }
        void GetWaveDetails()
        {
            using (WebClient wc = new WebClient())
            {
                string marineAPIURL = $"https://marine-api.open-meteo.com/v1/marine?latitude={latitude}&longitude={longitude}&current=swell_wave_height,wave_height&length_unit={unitsOfMeasurement}";
                var json = wc.DownloadString(marineAPIURL);
                marinedata_model.root marineData = JsonConvert.DeserializeObject<marinedata_model.root>(json);

                Console.WriteLine("Swell Size: " + marineData.current.swell_wave_height + unitsOfMeasurmentSymbol);
                HomeScreenConditionSwellSize.Text = (marineData.current.swell_wave_height + unitsOfMeasurmentSymbol);
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
                forecastControl.ForecastTemp.Text = $"{forecast.MinTemp}{unitsOfMeasurmentTempSymbol} / {forecast.MaxTemp}{unitsOfMeasurmentTempSymbol}";
        
                // Load weather icon (example using OpenWeatherMap icons)
                string iconUrl = $"http://openweathermap.org/img/wn/{forecast.Icon}@2x.png";
                forecastControl.ForecastIcon.LoadAsync(iconUrl);
        
                // Add to flow panel
                ForecastUC.Controls.Add(forecastControl);
            }
        }
        private double ConvertWindSpeed(double speedInMetersPerSecond)
        {
            if (unitsOfMeasurement == "imperial")
            {
                // Convert m/s to mph (1 m/s = 2.23694 mph)
                return Math.Round(speedInMetersPerSecond * 2.23694, 1);
            }
            else // metric
            {
                // Convert m/s to km/h (1 m/s = 3.6 km/h)
                return Math.Round(speedInMetersPerSecond * 3.6, 1);
            }
        }
        void GetFiveDayForecast()
        {
           
            using (WebClient wc = new WebClient())
            {
                // Added 'lang=en' parameter to get English descriptions
                string marineAPIURL = $"https://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={openWeatherAPIKey}&units={unitsOfMeasurement}&lang=en";
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
                        Date = DateTime.Parse(item.dt_txt).ToString("ddd, MMM dd"),
                        Icon = item.weather.First().icon,
                        Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.weather.First().description), // Capitalized description
                        WindSpeed = ConvertWindSpeed(item.wind.speed), // Convert m/s to km/h and round
                        MinTemp = Math.Round(item.main.temp_min),
                        MaxTemp = Math.Round(item.main.temp_max)
                    })
                    .ToList();
        
                // Display in UI
                DisplayFiveDayForecast(dailyForecasts);
                
                // Optional: Print to console for debugging
                Console.WriteLine("Forecast loaded successfully:");
                foreach (var forecast in dailyForecasts)
                {
                    Console.WriteLine($"{forecast.Date}: {forecast.Description}, {forecast.WindSpeed} km/h, {forecast.MinTemp}°C/{forecast.MaxTemp}°C");
                }
            }
        }

        void pullDisplay()
        {
            formatLocation();
            getBeachLocation();
            getWeatherDetails();
            GetWaveDetails();
            GetFiveDayForecast();
        }
        
        private void HomeScreenSearchButton_Click(object sender, EventArgs e)
        {
            pullDisplay();
        }

        
        

        private void SettingsPanelMetricButton_Click(object sender, EventArgs e)
        {
            unitsOfMeasurement = "metric";
            unitsOfMeasurmentWindSymbol = " kph";
            unitsOfMeasurmentSymbol = " m";
            unitsOfMeasurmentTempSymbol = " °c";
            
            SettingsPanelMetricButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#0066cc");
            SettingsPannelImperialButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#202b3b");
        }

        private void SettingPannelImperialButton_Click(object sender, EventArgs e)
        {
            unitsOfMeasurement = "imperial";
            unitsOfMeasurmentWindSymbol = " mph";
            unitsOfMeasurmentSymbol = " ft";
            unitsOfMeasurmentTempSymbol = "°c";
            
            SettingsPannelImperialButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#0066cc");
            SettingsPanelMetricButton.BackColor = System.Drawing.ColorTranslator.FromHtml("#202b3b");
        }

        private void HelpScreenGetHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.sites.google.com/stu.usc.edu.tt/flowforecast/home?read_current=1");
        }

        private void ControlPanelHelpButton_Click(object sender, EventArgs e)
        {
            HelpScreen.Visible = true;
            HelpScreen.Enabled = true;
            
            HomeScreenViewPanel.Visible = false;
            HomeScreenViewPanel.Enabled = false;

            SettingsScreenPanel.Visible = false;
            SettingsScreenPanel.Enabled = false;
        }


        private void ControlPanelSettingsButton_Click(object sender, EventArgs e)
        {
            SettingsScreenPanel.Visible = true;
            SettingsScreenPanel.Enabled = true;
            
            HomeScreenViewPanel.Visible = false;
            HomeScreenViewPanel.Enabled = false;

            HelpScreen.Visible = false;
            HelpScreen.Enabled = false;
        }
        
        private void ControlPanelHomeButton_Click(object sender, EventArgs e)
        {
            HomeScreenViewPanel.Visible = true;
            HomeScreenViewPanel.Enabled = true;
            
            SettingsScreenPanel.Visible = false;
            HomeScreenViewPanel.Enabled = false;

            HelpScreen.Visible = false;
            HelpScreen.Enabled = false;
        }

        private void ControlPanelLeaveButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
