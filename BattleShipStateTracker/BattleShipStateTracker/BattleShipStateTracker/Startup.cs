using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace BattleShipStateTracker
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
            services.AddSingleton<IPlayerProvider, PlayerProvider>(provider =>
            {
                //This section is to initialize the board with 10x10 units
                //and allocate ships to it. 
                //This code is only needed till the game is dependent on the inmemory collection.  
                UnitProvider unitProvider = new(Configuration);
                BoardProvider boardProvider = new(unitProvider);
                ShipProvider shipProvider = new(Configuration);
                Board board = boardProvider.CreateBoard(1, Configuration["GameSettings:BoardName"].ToString());
                board = shipProvider.CreateShips(int.Parse(Configuration["GameSettings:ShipSettings:NumberOfShips"].ToString()), board);
                return new PlayerProvider(board, Configuration);
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BattleShipStateTracker", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BattleShipStateTracker v1"));
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
