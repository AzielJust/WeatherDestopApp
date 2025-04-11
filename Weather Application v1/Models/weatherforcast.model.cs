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
        }

        public class list
        {
            public main main { get; set; }
            public List<weather> weather { get; set; }
            public string dt_txt { get; set; }
        }

        public class root
        {
            public List<list> list { get; set; }
        }
    }
    
    public class DailyForecast
    {
        public string Date { get; set; }       // Format: "YYYY-MM-DD"
        public string Icon { get; set; }       // Weather icon code
        public double MinTemp { get; set; }    // Minimum temperature
        public double MaxTemp { get; set; }    // Maximum temperature
    }
}