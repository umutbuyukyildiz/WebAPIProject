using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProject.Domain;
using WebAPIProject.Domain.Repositories;
using WebAPIProject.Domain.Services;
using WebAPIProject.Domain.UnitOfWork;
using WebAPIProject.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPIProject.Security.Token;

namespace WebAPIProject
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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            



            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            


            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>

             {

                 builder

                  .AllowAnyMethod()

                  .AllowAnyHeader()

                  .WithOrigins("http://localhost:44379", "http://localhost:44374")

                  .AllowCredentials()

                  .SetIsOriginAllowed((host) => true);



             }));

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtbeareroptions =>
            {
                jwtbeareroptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience=tokenOptions.Audience,
                    IssuerSigningKey=SignHandler.GetSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew=TimeSpan.Zero

                    
                   
                };




            });






            services.AddControllers();

            services.AddDbContext<WebAPIProjectContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);

            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("ApiCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
            
        }
    }
}
