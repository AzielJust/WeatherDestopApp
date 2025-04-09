using System.Dynamic;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Weather_Application_v1
{
    public class GeoapifyResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("features")]
        public List<Feature> Features { get; set; }

        [JsonPropertyName("query")]
        public Query Query { get; set; }
    }

    public class Feature
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("properties")]
        public LProperties Properties { get; set; }

        [JsonPropertyName("bbox")]
        public List<double> Bbox { get; set; }
    }

    public class Geometry
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }
    }

    public class LProperties
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("datasource")]
        public Datasource Datasource { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("state_code")]
        public string StateCode { get; set; }

        [JsonPropertyName("county_code")]
        public string CountyCode { get; set; }

        [JsonPropertyName("district")]
        public string District { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("result_type")]
        public string ResultType { get; set; }

        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        [JsonPropertyName("formatted")]
        public string Formatted { get; set; }

        [JsonPropertyName("address_line1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("address_line2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("timezone")]
        public Timezone Timezone { get; set; }

        [JsonPropertyName("plus_code")]
        public string PlusCode { get; set; }

        [JsonPropertyName("plus_code_short")]
        public string PlusCodeShort { get; set; }

        [JsonPropertyName("iso3166_2")]
        public string Iso3166_2 { get; set; }

        [JsonPropertyName("rank")]
        public Rank Rank { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }
    }

    public class Datasource
    {
        [JsonPropertyName("sourcename")]
        public string Sourcename { get; set; }

        [JsonPropertyName("attribution")]
        public string Attribution { get; set; }

        [JsonPropertyName("license")]
        public string License { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Timezone
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("offset_STD")]
        public string OffsetSTD { get; set; }

        [JsonPropertyName("offset_STD_seconds")]
        public int OffsetSTDSeconds { get; set; }

        [JsonPropertyName("offset_DST")]
        public string OffsetDST { get; set; }

        [JsonPropertyName("offset_DST_seconds")]
        public int OffsetDSTSeconds { get; set; }

        [JsonPropertyName("abbreviation_STD")]
        public string AbbreviationSTD { get; set; }

        [JsonPropertyName("abbreviation_DST")]
        public string AbbreviationDST { get; set; }
    }

    public class Rank
    {
        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }  // Changed from int to double

        [JsonPropertyName("match_type")]
        public string MatchType { get; set; }
    }

    public class Query
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

}