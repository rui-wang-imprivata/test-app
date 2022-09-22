namespace TestAppApi.Services
{
    public class ForecastService : IForecastService
    {
        public IEnumerable<WeatherForecast> GetWeatherForecasts()
        {
            throw new Exception("Error happened when get weatherforecasts");
        }
    }
}
