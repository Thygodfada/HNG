using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BasicWebServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ErnestServer : ControllerBase
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<ErnestServer> _logger;

		public ErnestServer(IHttpClientFactory httpClient, ILogger<ErnestServer> logger)
		{
			_httpClientFactory = httpClient;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] string visitor_name)
		{
			string apiKey = "3a70856d25ec434d89530626240107"; //Environment.GetEnvironmentVariable("WEATHER_API_KEY");

			// Get client IP address
			string clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
			if (string.IsNullOrEmpty(clientIp) || clientIp == "::1")
			{
				clientIp = "185.249.71.82"; 
			}

			var httpClient = _httpClientFactory.CreateClient();

			try
			{
				// Fetch location information based on IP
				var ipInfoResponse = await httpClient.GetStringAsync($"http://api.weatherapi.com/v1/ip.json?q={clientIp}&key={apiKey}");
				dynamic ipInfo = JObject.Parse(ipInfoResponse);
				string city = ipInfo.city ?? "Unknown location";

				_logger.LogInformation($"City determined from IP: {city}");

				// Fetch weather information for the location
				var weatherResponse = await httpClient.GetStringAsync($"http://api.weatherapi.com/v1/current.json?q={city}&key={apiKey}");
				dynamic weatherInfo = JObject.Parse(weatherResponse);
				string temperature = weatherInfo.current?.temp_c ?? "N/A";

				// Construct the response
				var response = new
				{
					client_ip = clientIp,
					location = city,
					greeting = $"Hello, {visitor_name}!, the temperature is {temperature} degrees Celsius in {city}"
				};

				return Ok(response);
			}
			catch (HttpRequestException httpEx)
			{
				_logger.LogError($"HttpRequestException: {httpEx.Message}");
				return StatusCode((int)httpEx.StatusCode, new { error = httpEx.Message });
			}
			catch (Exception ex)
			{
				_logger.LogError($"Exception: {ex.Message}");
				return StatusCode(500, new { error = ex.Message });
			}
		}
	}
}
