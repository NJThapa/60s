using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace Dlvr.SixtySeconds.Api
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

            // services.AddCors(options =>
            // {
            //     options.AddDefaultPolicy(builder =>
            //         builder.SetIsOriginAllowed(_ => true)
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .AllowCredentials());
            // });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "60 Seconds API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("add:user", policy => policy.Requirements.Add(new HasScopeRequirement("add:user", Configuration["Auth0:Domain"])));
                options.AddPolicy("remove:user", policy => policy.Requirements.Add(new HasScopeRequirement("remove:user", Configuration["Auth0:Domain"])));
                options.AddPolicy("add:script", policy => policy.Requirements.Add(new HasScopeRequirement("add:script", Configuration["Auth0:Domain"])));
                options.AddPolicy("remove:script", policy => policy.Requirements.Add(new HasScopeRequirement("remove:script", Configuration["Auth0:Domain"])));
                options.AddPolicy("add:task", policy => policy.Requirements.Add(new HasScopeRequirement("add:task", Configuration["Auth0:Domain"])));
                options.AddPolicy("remove:task", policy => policy.Requirements.Add(new HasScopeRequirement("remove:task", Configuration["Auth0:Domain"])));
                options.AddPolicy("add:user", policy => policy.Requirements.Add(new HasScopeRequirement("add:user", Configuration["Auth0:Domain"])));
                options.AddPolicy("update:task", policy => policy.Requirements.Add(new HasScopeRequirement("update:task ", Configuration["Auth0:Domain"])));
                options.AddPolicy("update:script", policy => policy.Requirements.Add(new HasScopeRequirement("update:script", Configuration["Auth0:Domain"])));
                options.AddPolicy("read:script", policy => policy.Requirements.Add(new HasScopeRequirement("read:script", Configuration["Auth0:Domain"])));
                options.AddPolicy("read:scenario", policy => policy.Requirements.Add(new HasScopeRequirement("read:scenario", Configuration["Auth0:Domain"])));
                options.AddPolicy("add:comment", policy => policy.Requirements.Add(new HasScopeRequirement("add:comment", Configuration["Auth0:Domain"])));
                options.AddPolicy("update:comment", policy => policy.Requirements.Add(new HasScopeRequirement("update:comment", Configuration["Auth0:Domain"])));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddControllers();
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureSqlSixDevCon")));           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "60 Seconds V1");
            });

            //app.UseCors();
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
