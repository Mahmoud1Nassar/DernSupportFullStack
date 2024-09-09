using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DernSupportBackEnd.Data; 
using DernSupportBackEnd.Models; 
using DernSupportBackEnd.Repositories.Interfaces; 
using DernSupportBackEnd.Repositories.Services;
using DernSupportBackEnd.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configure SQL Server and DbContext
builder.Services.AddDbContext<DernDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DernDbContext>()
    .AddDefaultTokenProviders();

// JWT configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,  // No issuer validation
        ValidateAudience = false, // No audience validation
        ValidateLifetime = true, // Ensure token hasn't expired
        ValidateIssuerSigningKey = true, // Validate the token's signing key
        IssuerSigningKey = new SymmetricSecurityKey(key), // Symmetric security key for signing
        ClockSkew = TimeSpan.Zero // No delay in expiration time
    };
});

// Add CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Register Services and Interfaces for Dependency Injection
builder.Services.AddScoped<ISupportRequest, SupportRequestService>();
builder.Services.AddScoped<IAppointment, AppointmentService>();
builder.Services.AddScoped<ISparePart, SparePartService>();
builder.Services.AddScoped<IQuote, QuoteService>();
builder.Services.AddScoped<IAuth, AuthService>();

// Add controllers
builder.Services.AddControllers();

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dern Support API", Version = "v1" });

    // Define the JWT security scheme for Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Make sure Swagger includes the security scheme in API calls
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
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dern Support API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Ensure this is before authorization
app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy"); // Apply CORS if needed

// Run the application
app.Run();
