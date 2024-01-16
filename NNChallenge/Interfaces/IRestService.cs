using System.Threading.Tasks;

namespace NNChallenge.Interfaces
{
    public interface IRestService
    {
        Task<IWeatherForecastVO> GetData(string location);
    }
}
