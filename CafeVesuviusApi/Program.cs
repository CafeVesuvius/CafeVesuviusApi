using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CafeVesuviusApi.Context;
using CafeVesuviusApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Name=ConnectionString");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins", builder => { builder.AllowAnyOrigin(); });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "CafeVesuvius", Version = "v1" });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "This site uses Bearer token and you have to pass" + 
                      "it as Bearer<<space>>Token",
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
                Reference=new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var jwtKey = builder.Configuration.GetValue<string>("JwtSettings:Key");
var keyBytes = Encoding.ASCII.GetBytes(jwtKey);

TokenValidationParameters tokenValidation = new TokenValidationParameters
{
    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
    ValidateLifetime = true,
    ValidateAudience = false,
    ValidateIssuer = false,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddSingleton(tokenValidation);
builder.Services.AddAuthentication(authOptions =>
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = tokenValidation;
        jwtOptions.Events = new JwtBearerEvents();
        jwtOptions.Events.OnTokenValidated = async (context) =>
        {
            var authService = context.Request.HttpContext.RequestServices.GetService<IAuthenticationRepository>();
            var jwtToken = context.SecurityToken as JwtSecurityToken;
            var ipAddress = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (!await authService.IsTokenValid(jwtToken.RawData, ipAddress))
                context.Fail("Invalid Token Details");
        };
    });
    
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddDbContext<CafeVesuviusContext>(x => x.UseSqlServer(connectionString,builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }));
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
