using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api", async (context) =>
{
    Console.WriteLine("traceparent header" + context.Request.Headers["traceparent"]);
    Console.WriteLine("Trace Id: " + Activity.Current.TraceId);
    Console.WriteLine("Parent Span Id: " + Activity.Current.ParentSpanId);
    Console.WriteLine("Parent Id: " + Activity.Current.ParentId);
    Console.WriteLine("Span Id: " + Activity.Current.SpanId);
    
    var httpClient = new HttpClient();
    var httpResponseMessage = (await httpClient.GetAsync("http://localhost:5003/api"));
    var callResult= await httpResponseMessage.Content.ReadAsStringAsync();
    await context.Response.WriteAsync(callResult);
});


app.Run();

