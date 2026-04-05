# Swallow.ContentSecurityPolicy

A neat Content Security Policy (CSP) integration for ASP.NET Core.

## Setup

See [the demo host](./demo/DemoHost/Program.cs) for a simple example.

Register the services:
```csharp
// Register the services and add a fallback policy.
builder.Services.AddContentSecurityPolicy(
    opt => opt.SetFallback(b => b.AddDefaultSource(Allow.Self)));
```

Then add the middleware:
```csharp
app.UseContentSecurityPolicy();
```

And done!

### Use a nonce

If your CSP contains a *nonce*, you can access that nonce via the `HttpContext`:

```csharp
builder.Services.AddContentSecurityPolicy(
    opt => opt.SetDefaultPolicy(b => b.AddDefaultSource(Allow.Nonce)));

// ... later
app.UseContentSecurityPolicy();
app.MapGet("/", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.Nonce}'"));
```

### Use a specific policy per endpoint

Similar to authorization policies, you can configure multiple different content
security policies and choose which one to apply based on the
`ContentSecurityPolicyAttribute`. You can even prevent the default policy from
being applied by using the `IgnoreContentSecurityPolicyAttribute`.

```csharp
builder.Services.AddContentSecurityPolicy(opt => opt
        .SetDefaultPolicy(b => b.AddDefaultSource(Allow.Nothing))
        .AddPolicy("nonce", b => b.AddDefaultSource(Allow.Nonce))
        .AddPolicy("self", b => b.AddDefaultSource(Allow.Self)));

app.MapGet("/", () => "I have nothing");
app.MapGet("/nonce", () => "I have a nonce").WithMetadata(new ContentSecurityPolicyAttribute("nonce"));
app.MapGet("/self", () => "I have self").WithMetadata(new ContentSecurityPolicyAttribute("self"));
app.MapGet("/ignore", () => "I don't have CSP").WithMetadata(new IgnoreContentSecurityPolicyAttribute());
```

### Reporting violations

To not actually block any resources, but only send reports for resources that
*would* be blocked, you can set `ReportOnly` on the `ContentSecurityPolicy`.

```csharp
builder.Services.AddContentSecurityPolicy(opt => opt
    .SetDefaultPolicy(b => b
        .AddDefaultSource(Allow.Nothing)
        .ReportOnly()));
```

This only makes sense when actually setting a reporting endpoint, though.

```csharp
builder.Services.AddContentSecurityPolicy(opt => opt
    .SetDefaultPolicy(b => b
        .AddDefaultSource(Allow.Nothing)
        .SendReportsTo("/reports")
        .ReportOnly()));
```

### Handling violation reports

Most of the time, you want to handle the CSP violations in the same host that
defines them. To do that, you can implement one (or more) `IReportHandler`s. To
automatically wire up the handler to the `report-to` directive, you can use
`SendReportsToLocal`:

```csharp
builder.Services.AddContentSecurityPolicy(opt => opt
    .SetDefaultPolicy(b => b
        .AddDefaultSource(Allow.Nothing)
        .SendReportsToLocal()
        .ReportOnly()));

builder.Services.AddContentSecurityPolicyReportHandler<MyHandler>();
// ...or builder.Services.AddScoped<IReportHandler, MyHandler>()
```

The URL of the reporting endpoint is resolved automatically... once you map
the endpoint:

```csharp
// Handles reports on _csp/report by default
app.MapContentSecurityPolicyViolations();

// You can also pass in a custom route if you want:
app.MapContentSecurityPolicyViolations(route: "custom-report-route");
```

The report handler itself can be fairly simple:

```csharp
public sealed class ReportHandler(ILogger<ReportHandler> logger) : IReportHandler
{
    public Task Handle(ViolationReport[] violationReports, CancellationToken cancellationToken)
    {
        foreach (var report in violationReports)
        {
            // Just log them as warnings.
            logger.LogWarning("CSP Violation {Report}", JsonSerializer.Serialize(report));
        }

        return Task.CompletedTask;
    }
}

```
