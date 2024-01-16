using NNChallenge.Implementations.Dtos;
using NNChallenge.Interfaces;
using System;
using System.Linq;

namespace NNChallenge.Implementations
{
    public class WeatherForecastVO : IWeatherForecastVO
    {
        public WeatherForecastVO(WeatherForecastDto forecastDto)
        {
            City = forecastDto.Location.City;
            HourForecast = forecastDto.Forecast.ForecastDays
                .SelectMany(x => x.Hours.Select(n => new HourWeatherForecastVO(n)))
                .ToArray();
        }

        public string City { get; }

        public IHourWeatherForecastVO[] HourForecast { get; }
    }

    public class HourWeatherForecastVO : IHourWeatherForecastVO
    {
        public HourWeatherForecastVO(HourDto hourDto)
        {
            Date = DateTime.Parse(hourDto.Time);
            TemperatureCelcius = hourDto.TempC;
            TemperatureFahrenheit = hourDto.TempF;
            ForecastPictureURL = $"https:{hourDto.Condition.Icon}";
        }

        public DateTime Date { get; }

        public float TemperatureCelcius { get; }

        public float TemperatureFahrenheit { get; }

        public string ForecastPictureURL { get; }
    }
}
