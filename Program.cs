using Microsoft.EntityFrameworkCore;
using polisync.Data;
using polisync.Services;
using polisync.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

// === BUILDER ===
var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(config.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClaimsInterface, ClaimsRepository>();
builder.Services.AddScoped<IPolicyInterface, PolicyRepository>();

builder.Services.AddScoped<ClaimsService>();
builder.Services.AddScoped<PolicyService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500/wwwroot/html/login.html")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "polisyncAuth";
                    options.Cookie.HttpOnly  = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/access-denied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                    options.SlidingExpiration = true;

                });

builder.Services.AddAuthorization();

builder.Services.AddControllers();



// === APP ===
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// === Health Endpoint ===
// Useful check if API is alive
app.MapGet("/", () => Results.Ok(new
{
    Application = "Polisync API",
    Status = "Running",
    Environent = app.Environment.EnvironmentName,
    Time = DateTime.UtcNow
}
));

app.Run();








// === ARCHIVE ===

/* Program.cs => Replaced JWT auth with cookie based auth:

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                    };
                });
*/




/* AppDbContext => Enable logging to expose SQL Queries/migration issues in the terminal:

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
    optionsBuilder
        // expose SQL Queries in the console
        .EnableSensitiveDataLogging()
        .UseLoggerFactory(GetLoggerFactory());

private ILoggerFactory? GetLoggerFactory()
{
    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
        builder.AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name && 
            level == LogLevel.Information);
    });
    return loggerFactory;
}
*/




/* Since in Render, app runs in Production by default, the code below never executes:

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

In this case, it is OK to expose app.UseSwagger() and app.UseSwaggerUI() publicly
*/