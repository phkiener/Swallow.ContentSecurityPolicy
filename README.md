# Swallow.ContentSecurityPolicy

A neat Content Security Policy (CSP) integration for ASP.NET Core. Because after
all this time, there isn't really a built-in way to do that, especially when
working with a nonce. AOT-compatible, by the way!

## Getting started

See [the demo host](./docs/demo) for full example.

### Reference the package

The packages are available on [NuGet](https://www.nuget.org/packages/Swallow.ContentSecurityPolicy/1.0.0).

```xml
<ItemGroup>
  <PackageReference Include="Swallow.ContentSecurityPolicy" Version="1.0.0" />

  <!-- Or, if you don't need to set up a host project: -->
  <PackageReference Include="Swallow.ContentSecurityPolicy.Abstractions" Version="1.0.0" />
</ItemGroup>
```

### Register the services

```csharp
builder.Services.AddContentSecurityPolicy(
    opt => opt
        .SetFallbackPolicy(b => b.AddDefaultSource(Allow.Nothing))
        .SetDefaultPolicy(b => b.AddDefaultSource(Allow.Self, Allow.Nonce))
        .AddPolicy("Special", b => b.AddDefaultSource(Allow.UnsafeInline))
    );
```

The policies are similar to authorization policies:
- The *fallback policy* is used when an endpoint does not define a policy
- The *default policy* is used when an endpoint defines a policy but does not require a _specific_ policy
- A specific policy is used when an endpoint refers to that policy by its name

### Add the middleware

```csharp
app.UseContentSecurityPolicy();
```

It should be placed after `UseRouting()` because the middleware tries to read
metadata from the resolved endpoint. If you do not specify `UseRouting()` by
yourself, it doesn't matter - you'll be safe!

### Apply a policy to an endpoint

```csharp
app.MapGet("/", () => "I have the fallback policy");
app.MapGet("/default", () => "I have the default policy").WithContentSecurityPolicy();
app.MapGet("/specific", () => "I have a specific policy").WithContentSecurityPolicy("Special");
app.MapGet("/ignored", () => "I don't have any policy").DisableContentSecurityPolicy();
```

You can also use the attributes directly: `ContentSecurityPolicyAttribute` and `DisableContentSecurityPolicyAttribute`.

While you *can* add multiple `WithContentSecurityPolicy()`-calls to a single endpoint,
only the last one is considered. As soon as you use `DisableContentSecurityPolicy()`,
all other calls to `WithContentSecurityPolicy()` are ignored; this is very similar
to `[Authorize]` and `[AllowAnonymous]`.

### Use a *nonce*

If your policy is configured to allow a *nonce*, you can access that nonce via the `HttpContext`:

```csharp
builder.Services.AddContentSecurityPolicy(
    opt => opt.SetFallbackPolicy(b => b.AddDefaultSource(Allow.Nonce)));

// ... later
app.UseContentSecurityPolicy();
app.MapGet("/", ctx => ctx.Response.WriteAsync($"The nonce is '{ctx.Nonce}'"));
```

### Reporting violations

To not actually block any resources, but only send reports for resources that
*would* be blocked, you can set `ReportOnly` on the policy.

```csharp
builder.Services.AddContentSecurityPolicy(opt => opt
    .SetFallbackPolicy(b => b
        .AddDefaultSource(Allow.Nothing)
        .ReportOnly()));
```

This only makes sense when actually setting a reporting endpoint, though.

```csharp
builder.Services.AddContentSecurityPolicy(opt => opt
    .SetFallbackPolicy(b => b
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
    .SetFallbackPolicy(b => b
        .AddDefaultSource(Allow.Nothing)
        .SendReportsToLocal()
        .ReportOnly()));

builder.Services.AddContentSecurityPolicyReportHandler<MyHandler>();
// ...or builder.Services.AddScoped<IReportHandler, MyHandler>()
```

The URL of the reporting endpoint is resolved automatically once you map the
endpoint:

```csharp
// Handles reports on _framework/content-security-policy/violations by default
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
