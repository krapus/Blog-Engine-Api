using BlogEngine.DataAccess.ExternalServices.Http.Implementation;
using BlogEngine.DataAccess.ExternalServices.Http.Interface;
using BlogEngine.DataAccess.ExternalServices.Users.Implementation;
using BlogEngine.DataAccess.ExternalServices.Users.Interface;
using BlogEngine.DataAccess.Models;
using BlogEngine.Model.Settings;
using BlogEngine.Service.Comments.Implementation;
using BlogEngine.Service.Comments.Interface;
using BlogEngine.Service.Posts.Implementation;
using BlogEngine.Service.Posts.Interface;
using BlogEngine.Service.Users.Implementation;
using BlogEngine.Service.Users.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Text;
using Microsoft.OpenApi.Models;

namespace BlogEngine
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
            services.AddCors();

            services.AddDbContextPool<ZemogaBlogEngineContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONECTION_STRING")));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddOptions();

            // DI DataAccess Layer
            services.AddScoped<IAccountExternalService, AccountExternalService>();
            services.AddScoped<DataAccess.Repository.Posts.Interface.IPostRepository, DataAccess.Repository.Posts.Implementation.PostRepository>();
            services.AddScoped<DataAccess.Repository.Users.Interface.IUserRepository, DataAccess.Repository.Users.Implementation.UserRepository>();
            services.AddScoped<DataAccess.Repository.Roles.Interface.IRolRepository, DataAccess.Repository.Roles.Implementation.RolRepository>();
            services.AddScoped<DataAccess.Repository.Comments.Interface.ICommentRepository, DataAccess.Repository.Comments.Implementation.CommentRepository>();

            //DI Service Layer
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();

            // Add HttpClient Factory
            services.AddHttpClient<IHttpService, HttpService>()
            .AddPolicyHandler(GetRetryPolicy());

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Zemoga Blog Engine API",
                    Version = "v1",
                    Description = "Build a minimal Blog Engine / CMS backend API, that allows to create, edit and publish text based posts, with an approval flow where two different user types may interact",
                    Contact = new OpenApiContact
                    {
                        Name = "Dario Sierra",
                        Email = string.Empty                        
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zemoga Blog Engine API V1");

                // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2000));
        }
    }
}
