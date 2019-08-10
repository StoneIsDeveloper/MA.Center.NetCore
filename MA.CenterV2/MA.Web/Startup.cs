using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MA.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MA.Web.Data.Entity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using MA.Web.Models;

namespace MA.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                     Configuration.GetConnectionString("DefaultConnection")));

            //注册DbContext
            services.AddDbContext<BloggingContext>(options =>
                      options.UseSqlServer(
                          Configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Swagger API
            // 版本控制
           // services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            });

            services.AddSwaggerGen(
               options =>
               {
                   options.SwaggerDoc("v1", new Info { Title = "DemoAPI", Version = "v1" });
                   // resolve the IApiVersionDescriptionProvider service
                   // note: that we have to build a temporary service provider here because one has not been created yet
                   //  var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                   // add a swagger document for each discovered API version
                   // note: you might choose to skip or document deprecated API versions differently
                   // foreach (var description in provider.ApiVersionDescriptions)
                   //{
                   //    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                   //}

                   // add a custom operation filter which sets default values
                   //  options.OperationFilter<SwaggerDefaultValues>();

                   //
                   // integrate xml comments
                   // options.IncludeXmlComments(XmlCommentsFilePath);
                   //options.IncludeXmlComments(XmlModelsFilePath);
               });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(option=> {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoAPI V1");
            });

        }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        static string XmlModelsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = "";//typeof(UserInfoModel).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"TB_AspNetCore_Swagger v{description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "多版本管理（点右上角版本切换）<br/>",
                Contact = new Contact() { Name = "TopBrids_1373978075@qq.com" }
            };

            if (description.IsDeprecated)
            {
                info.Description += "<br/><b>TopBrids</b>";
            }

            return info;
        }
    }
}
