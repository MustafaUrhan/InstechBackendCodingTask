using System.Diagnostics;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Shared.Application.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : class, IRequest<TResponse>, ILoggableRequest
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestJson = JsonConvert.SerializeObject(request);
        var requestUri = _httpContextAccessor.HttpContext.Request.Path;
        var requestMethod = _httpContextAccessor.HttpContext.Request.Method;


        _logger.LogInformation("HTTP {Method} {Path} request {@requestJson}", requestMethod, requestUri, requestJson);
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        TResponse response = await next();
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

        if (response is IErrorOr errorOrResult)
        {
            if (errorOrResult.IsError)
            {
                var responseErrorsJson = JsonConvert.SerializeObject(errorOrResult.Errors);
                _logger.LogError("HTTP {Method} {Path} responded in {ElapsedMilliseconds}ms {@responseErrorsJson}", _httpContextAccessor.HttpContext.Request.Method, _httpContextAccessor.HttpContext.Request.Path, elapsedMilliseconds, responseErrorsJson);
            }
            else if (TryExtractErrorOrValue(response, out var responseValue))
            {
                var responseValueJson = JsonConvert.SerializeObject(responseValue);
                _logger.LogInformation("HTTP {Method} {Path} responded in {ElapsedMilliseconds}ms {@responseValueJson}", requestMethod, requestUri, elapsedMilliseconds, responseValueJson);
            }
            else
            {
                _logger.LogInformation("HTTP {Method} {Path} responded in {ElapsedMilliseconds}ms", requestMethod, requestUri, elapsedMilliseconds);
            }
        }
        else
        {
            _logger.LogInformation("HTTP {Method} {Path} responded in {ElapsedMilliseconds}ms {@responseJson}", requestMethod, requestUri, elapsedMilliseconds, response);
        }

        return response;
    }


    private bool TryExtractErrorOrValue(TResponse response, out object responseValue)
    {
        try
        {
            responseValue = null;

            if (response is IErrorOr errorOrResult && !errorOrResult.IsError)
            {
                var valueProperty = errorOrResult.GetType().GetProperty("Value");
                if (valueProperty != null)
                {
                    responseValue = valueProperty.GetValue(errorOrResult);
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            responseValue = null;
            return false;
        }
    }
}