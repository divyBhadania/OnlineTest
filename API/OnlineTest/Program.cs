using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OnlineTest;
using OnlineTest.Model.Interface;
using OnlineTest.Model.Repository;
using OnlineTest.Service.Interface;
using OnlineTest.Service.Services;
using OnlineTest.Services.Interface;
using OnlineTest.Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Dependency injection Services and Repository
builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRTokenRepository, RTokenRepository>();
builder.Services.AddScoped<IRTokenService, RTokenService>();
builder.Services.AddScoped<IUserRolesServices, UserRolesServices>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
//builder.Services.Configure<JWTConfigDTO>(builder.Configuration.GetSection("JWTConfig"));
#endregion

#region JWT Config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnlineTest", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your Bearer in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
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
});
ConfigureJwtAuthService(builder.Services);
#endregion

builder.Services.AddDbContext<OnlineTestContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLAuth"), b => b.MigrationsAssembly("OnlineTest.Model"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

#region Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
app.Use(async (context , next) =>
{
    try
    {
        await next(context);
    }
    catch(Exception e)
    {
        context.Response.StatusCode = 501;
    }
});
#endregion

void ConfigureJwtAuthService(IServiceCollection services)
{
    var audienceConfig = builder.Configuration.GetSection("JWTConfig");
    var symmetricKeyAsBase64 = audienceConfig["SecretKey"];
    var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
    var signingKey = new SymmetricSecurityKey(keyByteArray);

    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,
        ValidateIssuer = true,
        ValidIssuer = audienceConfig["Issuer"],
        // Not working as of now.
        ValidateAudience = false,
        ValidAudience = audienceConfig["Audience"],
        ValidateLifetime = true,
        RoleClaimType = "Role",
        ClockSkew = TimeSpan.Zero
    };

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = tokenValidationParameters;
        o.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = 401;
                context.Response.Headers.Append("UnAuthorized", "");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    data = "",
                    status = 200,
                    message = "You are not Authorized to use API."
                }));
            }
        };
    });
}