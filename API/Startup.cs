using BL.AppServices;
using BL.Bases;
using BL.Config;
using BL.Interfaces;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API
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

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new CustomTimeSpanConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddHttpContextAccessor(); //allow me to get user information such as id
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            
            services.AddIdentityCore<ApplicationUserIdentity>().AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<VezeetaContext>()
            .AddDefaultTokenProviders();


            // DI
            services.AddDbContext<VezeetaContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("CS"));
            });
            services.AddIdentity<ApplicationUserIdentity, IdentityRole>()
                .AddEntityFrameworkStores<VezeetaContext>();

            services.AddScoped<UserManager<ApplicationUserIdentity>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<GeneralAppService>();
            services.AddScoped<CityAppService>();
            services.AddScoped<SpecialtyAppService>();
            services.AddScoped<AreaAppService>();
            services.AddScoped<ClinicServicesAppServices>();
            services.AddScoped<SupSpecializationAppService>();
            services.AddScoped<DoctorAttachmentAppService>();
            services.AddScoped<DoctorAppService>();
            services.AddScoped<AccountAppService>();
            services.AddScoped<ClinicAppService>();
            services.AddScoped<ClinicImagesAppService>();
            services.AddScoped<WorkingDayAppService>();
            services.AddScoped<DayShiftAppService>();
            services.AddScoped<DoctorServiceAppService>();
            services.AddScoped<Doctor_DoctorServiceAppService>();


            
            services.AddScoped<DoctorSubSpecializationAppService>();
            services.AddScoped<RoleAppService>();
            services.AddScoped<ClinicClinicServiceAppService>();
            services.AddScoped<ReservationAppService>();
            services.AddScoped<OfferAppService>();
            services.AddScoped<SubOfferAppService>();
            services.AddScoped<MakeOfferAppService>();
            services.AddScoped<MakeOfferImageAppService>();
            services.AddScoped<ReserveOfferAppService>();
            services.AddScoped<RatingAppService>();
            services.AddScoped<OfferRatingAppService>();



            services.AddTransient<IMailService, SendGridMailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            RoleAppService _roleAppService,
            AccountAppService _accountAppService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(
                options => options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

            // make uploaded images stored in the Resources folder 
            //  make Resources folder it servable as well
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // create custom roles 
            _roleAppService.CreateRoles().Wait();
            // add custom first admin
            //accountAppService.CreateFirstAdmin().Wait();
        }
    }
}
