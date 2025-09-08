using System.Text.Json.Serialization;
using gm_safety_thirdparty.Auth;
using gm_safety_thirdparty.Data; // <-- Make sure you add this
using gm_safety_thirdparty.Options;
using gm_safety_thirdparty.Repositories;
using gm_safety_thirdparty.Services;
using gm_safety_thirdparty.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddScoped<ExportService>();
// Options binding
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
builder.Services.Configure<DarwinBoxOptions>(builder.Configuration.GetSection("DarwinBoxApi"));

// HttpClient for DarwinBox
builder.Services.AddHttpClient<DarwinBoxClient>();

// ? Register DbContext with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI registrations
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeIngestService>();

// Controllers + JSON converters
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        o.JsonSerializerOptions.Converters.Add(new StringToNullableIntConverter());
        //o.JsonSerializerOptions.Converters.Add(new StringToBoolConverter());
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "gm_safety_thirdparty", Version = "v1" });
    c.AddSecurityDefinition("x-api-key", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "x-api-key",
        Type = SecuritySchemeType.ApiKey,
        Description = "Microservice API Key"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme{ Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "x-api-key" } },
            Array.Empty<string>()
        }
    });
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "gm_safety_thirdparty v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

// Minimal health check
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapControllers();
app.MapRazorPages();

app.Run();