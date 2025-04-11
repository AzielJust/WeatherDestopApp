using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Weather_Application_v1.Models
{
    public class marinedata_model
    {
        public class current_units
        {
            public string time { get; set; }
            public string interval { get; set; }
            public string swell_wave_height { get; set; }
            public string wave_height { get; set; }
        }

        public class current
        {
            public string time { get; set; }
            public int interval { get; set; }
            public string swell_wave_height { get; set; }
            public string wave_height { get; set; }
        }

        public class root
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public double generationtime_ms { get; set; }
            public int utc_offset_seconds { get; set; }
            public string timezone { get; set; }
            public string timezone_abbreviation { get; set; }
            public double elevation { get; set; }
            public current_units current_units { get; set; }
            public current current { get; set; }
        }
    }
}