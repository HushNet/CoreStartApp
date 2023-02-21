namespace CoreStartApp.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private string logPath = @"C:\Users\5047449\Documents\RiderProjects\CoreStartApp\CoreStartApp\Logs\ResponseLog.txt";

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        File.Delete(logPath);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logText = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}";
        Console.WriteLine(logText);
        await File.AppendAllTextAsync(logPath, $"{logText} \n");
        await _next.Invoke(context);
    }
}