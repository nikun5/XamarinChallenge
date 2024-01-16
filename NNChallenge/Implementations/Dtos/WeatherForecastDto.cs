using Newtonsoft.Json;
using System.Collections.Generic;

namespace NNChallenge.Implementations.Dtos
{
    public class WeatherForecastDto
    {
        [JsonProperty("location")]
        public LocationDto Location { get; set; }

        [JsonProperty("forecast")]
        public ForecastDto Forecast { get; set; }
    }

    public class LocationDto
    {
        [JsonProperty("name")]
        public string City { get; set; }
    }

    public class ForecastDto
    {
        [JsonProperty("forecastday")]
        public ICollection<ForecastDayDto> ForecastDays { get; set; }
    }

    public class ForecastDayDto
    {
        [JsonProperty("hour")]
        public ICollection<HourDto> Hours { get; set; }
    }

    public class HourDto
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("temp_c")]
        public float TempC { get; set; }

        [JsonProperty("temp_f")]
        public float TempF { get; set; }

        [JsonProperty("condition")]
        public ConditionDto Condition { get; set; }
    }

    public class ConditionDto
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}
