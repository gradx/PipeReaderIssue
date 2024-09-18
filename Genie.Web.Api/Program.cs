using System.Net;


var app = Build(args);

app.MapPost("errors", async (HttpContext httpContext) =>
{
    var read = await httpContext.Request.BodyReader.ReadAsync(new CancellationToken());
    return HttpStatusCode.OK;
});

app.MapPost("github_suggestion", async (HttpContext httpContext) =>
{
    var read = await httpContext.Request.BodyReader.ReadAsync(new CancellationToken());
    httpContext.Request.BodyReader.AdvanceTo(read.Buffer.Start, read.Buffer.End);
    return HttpStatusCode.OK;
});

app.MapPost("works3", async (HttpContext httpContext) =>
{
    var ms = new byte[(int)httpContext.Request.ContentLength];
    await httpContext.Request.BodyReader.AsStream().ReadAsync(ms, new CancellationToken());
    return HttpStatusCode.OK;
});

app.MapPost("works2", async (HttpContext httpContext) =>
{
    var ms = new byte[(int)httpContext.Request.ContentLength];
    var read = await httpContext.Request.Body.ReadAsync(ms, new CancellationToken());
    return HttpStatusCode.OK;
});

app.MapPost("works", async (HttpContext httpContext) =>
{
    using var ms = new MemoryStream();
    await httpContext.Request.BodyReader.CopyToAsync(ms, new CancellationToken());
    return HttpStatusCode.OK;
});

app.Run();


static WebApplication Build(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);
    return builder.Build();
}
