using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using PickPoint.Lib.Helpers.Auth;
using PickPoint.Lib.Serializers;
using PickPoint.RestApi.Middleware;
using WebApiContrib.Core.Formatter.Protobuf;

namespace PickPoint.RestApi.Service;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => false;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.AddSwaggerDocument(document =>
        {
            document.DocumentName = "v1";
            document.Title = "PickPoint API";
            document.Version = "1.0";
            document.PostProcess = document1 =>
            {
                var openApiServer = new OpenApiServer {Url = "https://api.example.com"};
                document1.Servers.Add(openApiServer);
            };

            document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            document.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new List<string>(),
                new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid JWT token into field",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(typeof(DateTime), new BsonUtcDateTimeSerializer());

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthTokenStore.Issuer,
                    ValidateAudience = true,
                    ValidAudience = AuthTokenStore.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthTokenStore.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddProtobufFormatters()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app
            .UseMiddleware<PickPointErrorHandlerMiddleware>()
            .UseForwardedHeaders(new ForwardedHeadersOptions
                {ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto})
            .UseCors("AllowAll")
            .UseOpenApi(a =>
            {
                a.PostProcess = (document, _) =>
                {
                    document.Schemes = new[] {OpenApiSchema.Http, OpenApiSchema.Https};
                    var openApiServer = new OpenApiServer {Url = "https://api.example.com"};
                    document.Servers.Add(openApiServer);
                };
            })
            .UseSwaggerUi3()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}