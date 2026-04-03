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

### Reporting endpoints

... to be done!
