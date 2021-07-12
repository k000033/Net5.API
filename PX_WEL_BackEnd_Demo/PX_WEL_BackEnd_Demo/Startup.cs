using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;  //IHttpContextAccessor
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using PX_WEL_BE_Lib.EnvControl;
using PX_WEL_BE_Lib.Service.WELAppInfoService;
using PX_WEL_BE_Lib.Service.DbInfo;
using PX_WEL_BE_Lib.Service.LoggingRecordService;
using PX_WEL_BE_Lib.Service.TravelService;
using PX_WEL_BE_Lib.Service.TimeMagicianService;


namespace PX_WEL_BackEnd_Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // ---------ArtificialLib    Zone-----------------
        SwitchEnv SwitchBtn = new SwitchEnv();

        //------------------------------------------------


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PX_WEL_BackEnd_Demo", Version = "v1" });
            });

            // Customed DI------------------------------------------------
            services.AddSingleton<ILoggingRecordService, LoggingRecordService>();
            services.AddSingleton<ITravelService, TravelService>();
            services.AddSingleton<ISwitchEnv, SwitchEnv>();
            services.AddSingleton<IDbConn, DbConn>();
            services.AddSingleton<IAppInfoService, AppInfoService>();


            // IHttpContextAccessor
            services.AddHttpContextAccessor();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PX_WEL_BackEnd_Demo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
