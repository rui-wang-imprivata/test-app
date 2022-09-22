namespace TestAppApi.Services
{
    public interface IForecastService
    {
        public IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}
