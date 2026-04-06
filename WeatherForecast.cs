namespace csharp_core_web_api;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}


// The namespace 'csharp_core_web_api' 
// already contains a definition for 'WeatherForecast' [/var/www/html/csharp_core_web_api.csproj]