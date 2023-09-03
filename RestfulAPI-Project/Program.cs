using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestfulAPI_Project.Infrastructure.Context;
using RestfulAPI_Project.Infrastructure.Repositories.Concretes;
using RestfulAPI_Project.Infrastructure.Repositories.Interfaces;
using RestfulAPI_Project.Infrastructure.Settings;
using System.Reflection;
using System.Text;

namespace RestfulAPI_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            builder.Services.AddAutoMapper(typeof(RestfulAPI_Project.Infrastructure.AutoMapper.Mapper));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new OpenApiInfo() 
                {
                    Title = "Restful API",
                    Version = "v1",
                    Description = "Restful API",
                    Contact = new OpenApiContact()
                    {
                        Email = "sinaemre.bekar@bilgeadam.com",
                        Name = "Sina Emre BEKAR",
                        Url = new Uri("https://github.com/sinaemre")
                    },
                    License = new OpenApiLicense() 
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement() 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Auth",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(xmlCommentFullPath);
            });

            var appSettingSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingSection);

            var appSettigns = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettigns.SecretKey);

            builder.Services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options2 => 
                {
                    options2.RequireHttpsMetadata = true;
                    options2.SaveToken = true;
                    options2.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        //Token deðerinin bu uygulamaya ait olup olmadýðýný anlamamýzý saðlayan Security Key doðrulamasý
                        ValidateIssuerSigningKey = true,
                        
                        //Security Key doüðrulamasý için SymetricSecurityKey nesnesi aracýlýðý ile mevcut keyi belirtiyoruz.
                        IssuerSigningKey = new SymmetricSecurityKey(key),

                        //Uygulamadaki token'ýn Audience deðerini belirledik.(Audience: Eriþebilecek kimlikler)
                        ValidateAudience = false,

                        //Uygulamadaki tokenen'ýn issuer deðerini belirledik. (Issuer: Token deðerini daðýtacak kiþiler)
                        ValidateIssuer = false
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
        }
    }
}