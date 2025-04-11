using System.Collections.Generic;

namespace Weather_Application_v1.Models
{
    public class five_day_forecast_model
    {
        public class main
        {
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class weather
        {
            public string icon { get; set; }
            public string description { get; set; }  // Added description
        }

        public class wind
        {
            public double speed { get; set; }  // Added wind speed
        }

        public class list
        {
            public main main { get; set; }
            public List<weather> weather { get; set; }
            public wind wind { get; set; }  // Added wind
            public string dt_txt { get; set; }
        }

        public class root
        {
            public List<list> list { get; set; }
        }
    }
    
    public class DailyForecast
    {
        public string Date { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }  // Added
        public double WindSpeed { get; set; }    // Added (in m/s)
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
    }
}