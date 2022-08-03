using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/api", async (context) =>
{
    Console.WriteLine("traceparent header: " + context.Request.Headers["traceparent"]);
    Console.WriteLine("Trace Id: " + Activity.Current.TraceId);
    Console.WriteLine("Parent Span Id: " + Activity.Current.ParentSpanId);
    Console.WriteLine("Parent Id: " + Activity.Current.ParentId);
    Console.WriteLine("Span Id: " + Activity.Current.SpanId);
    
    
    var httpClient = new HttpClient();
    var httpResponseMessage = await httpClient.GetAsync("http://localhost:5002/api");
    var callResult= await httpResponseMessage.Content.ReadAsStringAsync();
    await context.Response.WriteAsync(callResult);
});

app.Run();












//An attempt to disable HttpClient starting it's own activity before sending request
//https://devblogs.microsoft.com/dotnet/dotnet-6-networking-improvements/#diagnostics
// DistributedContextPropagator.Current = new SkipHttpClientActivityPropagator();
// public sealed class SkipHttpClientActivityPropagator : DistributedContextPropagator
// {
//     private readonly DistributedContextPropagator _originalPropagator = Current;
//
//     public override IReadOnlyCollection<string> Fields => _originalPropagator.Fields;
//
//     public override void Inject(Activity? activity, object? carrier, PropagatorSetterCallback? setter)
//     {
//         if (activity?.OperationName == "System.Net.Http.HttpRequestOut")
//         {
//             activity = activity.Parent;
//         }
//
//         _originalPropagator.Inject(activity, carrier, setter);
//     }
//
//     public override void ExtractTraceIdAndState(object? carrier, PropagatorGetterCallback? getter, out string? traceId, out string? traceState) =>
//         _originalPropagator.ExtractTraceIdAndState(carrier, getter, out traceId, out traceState);
//
//     public override IEnumerable<KeyValuePair<string, string?>>? ExtractBaggage(object? carrier, PropagatorGetterCallback? getter) =>
//         _originalPropagator.ExtractBaggage(carrier, getter);
// }











// public static class AnotherOperation
// {
//     [Trace]
//     [MethodImpl(MethodImplOptions.NoInlining)]
//     public static void Do()
//     {
//         //NewRelic.Api.Agent.NewRelic.SetTransactionName("Tasks", "blablabla");
//         //NewRelic.Api.Agent.NewRelic.GetAgent().CurrentTransaction.AcceptDistributedTraceHeaders();
//         Thread.Sleep(3000);
//     }
// }