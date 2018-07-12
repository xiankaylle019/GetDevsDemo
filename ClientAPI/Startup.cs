using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClientAPI.Data;
using ClientAPI.Data.Shared.DTOs;
using ClientAPI.Data.Shared.Mapping;
using ClientAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceCollectionLayer;

namespace ClientAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
          
            var key = System.Text.Encoding.ASCII.GetBytes (Configuration.GetSection ("AppSettings:Token").Value);

            services.AddDbContext<DataContext> (db => db.UseSqlServer(Configuration.GetConnectionString ("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(
                x => {
                   
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.SignIn.RequireConfirmedEmail = false;
                    x.SignIn.RequireConfirmedPhoneNumber = false;
                }
            ).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();


            services.AddBusinessLayerServices();
            
            services.AddDataLayerServices();
           
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

              services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                    });
                });
            
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.Audience = "http://localhost:5001/";
                    options.Authority = "http://localhost:5000/";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters {                   
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseHsts();
            }
            AutoMapperConfig.Execute();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            // app.UseStaticFiles();
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
