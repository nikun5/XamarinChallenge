using Newtonsoft.Json;
using NNChallenge.Constants;
using NNChallenge.Implementations.Dtos;
using NNChallenge.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NNChallenge.Implementations
{
    public class RestService : IRestService
    {
        private readonly HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<IWeatherForecastVO> GetData(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return null;
            }

            var uri = new Uri(string.Format(UriConstants.FORECAST_URI, location.Split(',')[0]));
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var stringContent = await response.Content.ReadAsStringAsync();
            var weatherForecastDto = JsonConvert.DeserializeObject<WeatherForecastDto>(stringContent);
            var weatherForecastVO = new WeatherForecastVO(weatherForecastDto);
            return weatherForecastVO;
        }
    }
}
