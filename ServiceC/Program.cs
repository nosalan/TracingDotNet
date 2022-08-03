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
    
    await context.Response.WriteAsync("Hello World! from service 3");
});


app.Run();