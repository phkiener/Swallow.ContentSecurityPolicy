# Swallow.ContentSecurityPolicy

A neat Content Security Policy (CSP) integration for ASP.NET Core.

## Setup

See [the demo host](./demo/DemoHost/Program.cs) for a simple example.

Register the services:
```csharp
// Register the services and add a default policy.
builder.Services.AddContentSecurityPolicy()
    .SetDefaultPolicy(new ContentSecurityPolicy());
```

Then add the middleware:
```csharp
app.UseContentSecurityPolicy();
```

And done!

### Use a nonce

If your CSP contains a *nonce*, you can access that nonce via the `HttpContext`:

```csharp
var defaultPolicy = new ContentSecurityPolicy { DefaultSource = [Nonce.Instance] };
builder.Services.AddContentSecurityPolicy()
    .SetDefaultPolicy(new ContentSecurityPolicy());

// ... later
app.UseContentSecurityPolicy();
app.MapGet("/", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.CspNonce}'"));
```

### Modify the CSP per request

The `HttpContext` exposes the current policy; this policy can be modified as long
as the headers have not been sent yet.

```csharp
app.MapGet("/deny-all", ctx =>
{
    ctx.ContentSecurityPolicy?.DefaultSource = [DenyAll.Instance];
    return ctx.Response.WriteAsync("You can't load anything!");
});
```

### Reporting violations

To not actually block any resources, but only send reports for resources that
*would* be blocked, you can set `ReportOnly` on the `ContentSecurityPolicy`.

```csharp
builder.Services.AddContentSecurityPolicy()
    .SetPolicy(new ContentSecurityPolicy
    {
        DefaultSource = [new Nonce()],
        StyleSource = [new Nonce()],
        ReportOnly = true // <- this one!
    })
```

This only makes sense when actually setting a reporting endpoint.

```csharp
builder.Services.AddContentSecurityPolicy()
    .SetPolicy(new ContentSecurityPolicy
    {
        DefaultSource = [new Nonce()],
        StyleSource = [new Nonce()],
        ReportOnly = true,
        ReportingEndpoint = "/report-csp" // <- this one!
    })
```

### Handling violation reports

You can also specify a handler to process any violation reports:

```csharp
builder.Services.AddContentSecurityPolicy()
    .SetPolicy(new ContentSecurityPolicy { DefaultSource = [new Nonce()], StyleSource = [new Nonce()], ReportOnly = true })
    .UseReportHandler<ReportHandler>(); // <- this one!
```

Note that you don't have to specify the `ReportingEndpoint`; it will be resolved automatically,
provided you register a matching endpoint:

```csharp
// Handles reports on _csp/report by default
app.MapContentSecurityPolicyReports();

// You can also pass in a custom route if you want:
app.MapContentSecurityPolicyReports(route: "_reports/csp/violation");
```

The report handler itself can be fairly simple:

```csharp
public sealed class ReportHandler(ILogger<ReportHandler> logger) : IReportHandler
{
    public Task Handle(CSPViolationReport violationReport, CancellationToken cancellationToken)
    {
        logger.LogWarning("CSP Violation {Report}", violationReport);

        return Task.CompletedTask;
    }
}
```
