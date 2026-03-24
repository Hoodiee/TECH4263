using System.Text.Json;

// ── Cities to look up ──────────────────────────────────────────────
string[] cities = { "Tokyo", "Cairo", "Toronto" };
string[] usCities = { "Atlanta", "Nashville", "Martin", "Richmond","Detroit","Akron" };

// ── Single shared HttpClient ───────────────────────────────────────
var httpClient = new HttpClient();

// ── Helper: convert weathercode to a description ───────────────────
// TODO: Expand this to cover at least 8 codes — see the table belown
string GetWeatherDescription(int code) => code switch 
{
    0 => "Clear Sky",
    1 or 2 => "Partly Cloudy",
    3 => "Overcast",
    45 or 48 => "Fog",
    51 or 53 or 55 => "Drizzle",
    61 or 63 or 65 => "Rain",
    71 or 73 or 75 => "Snow",
    80 or 81 or 82 => "Rain Showers",
    95 => "Thunderstorm",
    _ => $"Code {code}"
};

// ── Main program ───────────────────────────────────────────────────
Console.WriteLine("===================================");
Console.WriteLine("    CITY WEATHER LOOKUP");
Console.WriteLine("===================================\n");

foreach (var city in cities)
{
    Console.WriteLine($"Looking up: {city}...");

    // ── STEP 1: Geocoding API — city name → coordinates ────────────
    try
    {
        var geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={city}&count=1";
        var geoResponse = await httpClient.GetAsync(geoUrl);

        if (!geoResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"  [!] Geocoding failed: {geoResponse.StatusCode}\n");
            continue;
        }

        string geoJson = await geoResponse.Content.ReadAsStringAsync();
        using var geoDoc = JsonDocument.Parse(geoJson);

        var results = geoDoc.RootElement.GetProperty("results");
        if (results.GetArrayLength() == 0)
        {
            Console.WriteLine($"  [!] City '{city}' not found.\n");
            continue;
        }

        var location = results[0];
        string cityName = location.GetProperty("name").GetString()!;
        string country = location.GetProperty("country").GetString()!;
        double lat = location.GetProperty("latitude").GetDouble();
        double lon = location.GetProperty("longitude").GetDouble();

        Console.WriteLine($"  Found    : {cityName}, {country} ({lat}°N, {lon}°E)");

        // ── STEP 2: Weather API — coordinates → weather ────────────
        var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
        var weatherResponse = await httpClient.GetAsync(weatherUrl);

        if (!weatherResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"  [!] Weather request failed: {weatherResponse.StatusCode}\n");
            continue;
        }

        string weatherJson = await weatherResponse.Content.ReadAsStringAsync();
        using var weatherDoc = JsonDocument.Parse(weatherJson);

        var current = weatherDoc.RootElement.GetProperty("current_weather");
        double temp = current.GetProperty("temperature").GetDouble();
        double wind = current.GetProperty("windspeed").GetDouble();
        int code = current.GetProperty("weathercode").GetInt32();

        Console.WriteLine($"  Weather  : Temp: {temp}°C  |  Wind: {wind} km/h  |  {GetWeatherDescription(code)}");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"  [!] Network error: {ex.Message}");
    }

    Console.WriteLine();
}

Console.WriteLine("===================================");
Console.WriteLine("    US CITY WEATHER LOOKUP");
Console.WriteLine("===================================\n");

foreach (var city in usCities)
{
    Console.WriteLine($"Looking up: {city}...");

    // ── STEP 1: Geocoding API — city name → coordinates ────────────
    try
    {
        var geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={city}&count=1";
        var geoResponse = await httpClient.GetAsync(geoUrl);

        if (!geoResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"  [!] Geocoding failed: {geoResponse.StatusCode}\n");
            continue;
        }

        string geoJson = await geoResponse.Content.ReadAsStringAsync();
        using var geoDoc = JsonDocument.Parse(geoJson);

        var results = geoDoc.RootElement.GetProperty("results");
        if (results.GetArrayLength() == 0)
        {
            Console.WriteLine($"  [!] City '{city}' not found.\n");
            continue;
        }

        var location = results[0];
        string cityName = location.GetProperty("name").GetString()!;
        string country = location.GetProperty("country").GetString()!;
        double lat = location.GetProperty("latitude").GetDouble();
        double lon = location.GetProperty("longitude").GetDouble();

        Console.WriteLine($"  Found    : {cityName}, {country} ({lat}°N, {lon}°E)");

        // ── STEP 2: Weather API — coordinates → weather ────────────
        var weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
        var weatherResponse = await httpClient.GetAsync(weatherUrl);

        if (!weatherResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"  [!] Weather request failed: {weatherResponse.StatusCode}\n");
            continue;
        }

        string weatherJson = await weatherResponse.Content.ReadAsStringAsync();
        using var weatherDoc = JsonDocument.Parse(weatherJson);

        var current = weatherDoc.RootElement.GetProperty("current_weather");
        double temp = current.GetProperty("temperature").GetDouble();
        double wind = current.GetProperty("windspeed").GetDouble();
        int code = current.GetProperty("weathercode").GetInt32();

        Console.WriteLine($"  Weather  : Temp: {temp}°C  |  Wind: {wind} km/h  |  {GetWeatherDescription(code)}");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"  [!] Network error: {ex.Message}");
    }

    Console.WriteLine();
}

Console.WriteLine("===================================");
Console.WriteLine("Done!");