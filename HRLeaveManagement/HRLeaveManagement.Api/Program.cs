using HRLeaveManagement.Api.Middleware;
using HRLeaveManagement.Identity;
using HRLeaveManagement.Infrastructure;
using HRLeaveManagement.Persistence;
using HRLeaveManagment.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplictionServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

string? connString;
if (builder.Environment.IsDevelopment())
    connString = builder.Configuration.GetConnectionString("LeaveManagementConnectionString");
else
{
    // Use connection string provided at runtime by FlyIO.
    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    // Parse connection URL to connection string for Npgsql
    connUrl = connUrl.Replace("postgres://", string.Empty);
    var pgUserPass = connUrl.Split("@")[0];
    var pgHostPortDb = connUrl.Split("@")[1];
    var pgHostPort = pgHostPortDb.Split("/")[0];
    var pgDb = pgHostPortDb.Split("/")[1];
    var pgUser = pgUserPass.Split(":")[0];
    var pgPass = pgUserPass.Split(":")[1];
    var pgHost = pgHostPort.Split(":")[0];
    var pgPort = pgHostPort.Split(":")[1];
    var host = pgHost.Replace("flycast", "internal");

    //var db = pgDb.Split("?")[0];

    connString = $"Server={host};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
}

builder.Services.AddDbContext<HRLeaveManagementDbContext>(options =>
    options.UseNpgsql(connString));

builder.Services.AddDbContext<LeaveManagementIdentityDbContext>(options =>
                options.UseNpgsql(connString,
                b => b.MigrationsAssembly(typeof(LeaveManagementIdentityDbContext).Assembly.FullName)));

builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
                {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
       }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HR Leave Management Api",

    });

});

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        policy => policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(builder.Environment.IsDevelopment() ? "http://hrleavemanagement.mvc" : "https://hrmanagement.fly.dev"));
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRLeaveManagement.Api v1");
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

//app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<HRLeaveManagementDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);

    var identityContext = services.GetRequiredService<LeaveManagementIdentityDbContext>();
    await identityContext.Database.MigrateAsync();
    await SeedIdentity.SeedIdentityData(identityContext);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();

