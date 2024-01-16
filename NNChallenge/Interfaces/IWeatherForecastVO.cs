﻿using System;
namespace NNChallenge.Interfaces
{
    public interface IWeatherForecastVO
    {
        /// <summary>
        /// Name of the city
        /// </summary>
        string City { get; }
        /// <summary>
        /// Array of weather forecast entries
        /// </summary>
        IHourWeatherForecastVO[] HourForecast { get; }
    }

    public interface IHourWeatherForecastVO
    {
        /// <summary>
        /// date of forecast
        /// </summary>
        DateTime Date { get; }
        /// <summary>
        /// temperature in Celcius
        /// </summary>
        float TemperatureCelcius { get; }
        /// <summary>
        /// Temperature in Fahrenheit
        /// </summary>
        float TemperatureFahrenheit { get; }
        /// <summary>
        /// url for picture
        /// </summary>
        string ForecastPictureURL { get; }
    }
}
