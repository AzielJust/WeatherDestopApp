using System.Dynamic;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Weather_Application_v1
{
    public class WeatherDetails
    {
        public class WeatherResponse
        {
            [JsonPropertyName("coord")]
            public Coordinates Coord { get; set; }

            [JsonPropertyName("weather")]
            public List<Weather> Weather { get; set; }

            [JsonPropertyName("base")]
            public string Base { get; set; }

            [JsonPropertyName("main")]
            public MainWeatherData Main { get; set; }

            [JsonPropertyName("visibility")]
            public int Visibility { get; set; }

            [JsonPropertyName("wind")]
            public Wind Wind { get; set; }

            [JsonPropertyName("clouds")]
            public Clouds Clouds { get; set; }

            [JsonPropertyName("dt")]
            public long Dt { get; set; } // Unix timestamp

            [JsonPropertyName("sys")]
            public SystemData Sys { get; set; }

            [JsonPropertyName("timezone")]
            public int Timezone { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("cod")]
            public int Cod { get; set; }
        }

        public class Coordinates
        {
            [JsonPropertyName("lon")]
            public double Longitude { get; set; }

            [JsonPropertyName("lat")]
            public double Latitude { get; set; }
        }

        public class Weather
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("main")]
            public string Main { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("icon")]
            public string Icon { get; set; }
        }

        public class MainWeatherData
        {
            [JsonPropertyName("temp")]
            public double Temperature { get; set; }

            [JsonPropertyName("feels_like")]
            public double FeelsLike { get; set; }

            [JsonPropertyName("temp_min")]
            public double TempMin { get; set; }

            [JsonPropertyName("temp_max")]
            public double TempMax { get; set; }

            [JsonPropertyName("pressure")]
            public int Pressure { get; set; }

            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }

            [JsonPropertyName("sea_level")]
            public int SeaLevel { get; set; }

            [JsonPropertyName("grnd_level")]
            public int GroundLevel { get; set; }
        }

        public class Wind
        {
            [JsonPropertyName("speed")]
            public double Speed { get; set; }

            [JsonPropertyName("deg")]
            public int Direction { get; set; }

            [JsonPropertyName("gust")]
            public double Gust { get; set; }
        }

        public class Clouds
        {
            [JsonPropertyName("all")]
            public int Cloudiness { get; set; }
        }

        public class SystemData
        {
            [JsonPropertyName("sunrise")]
            public long Sunrise { get; set; } // Unix timestamp

            [JsonPropertyName("sunset")]
            public long Sunset { get; set; } // Unix timestamp
        }
    }
}