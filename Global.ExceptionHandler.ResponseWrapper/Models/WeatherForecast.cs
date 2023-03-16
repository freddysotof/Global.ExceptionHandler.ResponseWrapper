using System.ComponentModel.DataAnnotations;

namespace Global.ExceptionHandler.ResponseWrapper.Models;

/// <summary>
/// Weather Forecast class to provide Temperature, Summary and Date.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// The Date of Occurance
    /// </summary>
    /// [
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// The Temperature in Celsius
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// The Temperature in Fahrenheit
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// The Summary of Weather Forecast
    /// </summary>
    public string? Summary { get; set; }
}

