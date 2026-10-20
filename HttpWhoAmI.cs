using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace School.Function;

public class HttpWhoAmI
{
    private readonly ILogger<HttpWhoAmI> _logger;

    public HttpWhoAmI(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HttpWhoAmI>();
    }

    [Function("WhoAmI")]
    public async Task<HttpResponseData> WhoAmI(
  [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "WhoAmI")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP GET trigger function processed a request in WhoAmI().");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        await response.WriteStringAsync("André Di Lascio");

        return response;
    }
}
