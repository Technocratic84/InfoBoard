using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using InfoBoard.Data;
using InfoBoard.Data.Models;
using InfoBoard.Hubs;
using InfoBoard.Shared;
using Serilog;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<InfoBoard.Services.InfoService>();
builder.Services.AddDbContextFactory<InfoDBcontext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("InfoDb")));

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

// access to state variables etc
builder.Services.AddSingleton<InfoBoard.Shared.InfoGlobalClass>(); 
builder.Services.AddScoped<InfoBoard.Shared.InfoGlobalClass>();
builder.Services.AddScoped<BlazorAppContext>();

// configure Serilog
Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File("Logs/InfoLog.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

Log.Information("===========================  STARTUP ==============================");

var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();    
}
// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<InfoHub>("/infohub");
app.MapFallbackToPage("/_Host");

app.UseMiddleware<ClientIPMiddleware>();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Run();