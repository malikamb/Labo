using Labo.API.DependencyInjections;
using Labo.API.Extensions;
using Labo.DAL.Contexts;
using Labo.IL.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TournamentContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
});
builder.Services.AddRepositories();
builder.Services.AddBusinessServices();

JwtConfiguration jwtConfig = builder.Configuration.GetSection("JwtSettings")
    .Get<JwtConfiguration>()
    ?? throw new Exception("Jwt Config is missing");
builder.Services.AddJwt(jwtConfig);

MailerConfig mailerConfig = builder.Configuration.GetSection("Smtp")
    .Get<MailerConfig>()
    ?? throw new Exception("Mailer Config is missing");
builder.Services.AddMailer(mailerConfig);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme, ConfigureJwt
);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ConfigureSwagger);

builder.Services.AddCors(options
    => options.AddDefaultPolicy(b =>
    {
        b.AllowAnyHeader();
        b.AllowAnyMethod();
        b.AllowAnyOrigin();
    }));


WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseException();

app.MapControllers();

app.Run();


void ConfigureSwagger(SwaggerGenOptions options)
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tournament Management API", Version = "v1" });

    OpenApiSecurityScheme securitySchema = new()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    options.AddSecurityDefinition("Bearer", securitySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, ["Bearer"] }
    });
}
void ConfigureJwt(JwtBearerOptions options)
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = jwtConfig.ValidateIssuer,
        ValidateAudience = jwtConfig.ValidateAudience,
        ValidateLifetime = jwtConfig.ValidateLifeTime,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Signature)),
    };
}