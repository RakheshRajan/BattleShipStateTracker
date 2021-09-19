using BattleShipStateTracker.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BattleShipStateTracker.Provider
{
    public class UnitProvider : IUnitProvider
    {
        public int xMaxCordinate;
        public int yMaxCordinate;
        public IConfiguration Configuration { get; }

        public UnitProvider(IConfiguration configuration)
        {
            Configuration = configuration;
            xMaxCordinate = int.Parse(Configuration["GameSettings:BoardSettings:xCordinate"].ToString());
            yMaxCordinate = int.Parse(Configuration["GameSettings:BoardSettings:yCordinate"].ToString());
        }

        /// <summary>
        /// This function is responsible for creating the units required for the board.
        /// </summary>
        /// <returns></returns>
        public List<Unit> CreateUnits()
        {
            List<Unit> units = new();
            //Move board limit to appsettings.
            for (int i = 0; i <= yMaxCordinate; i++)
            {
                for (int j = 0; j <= xMaxCordinate; j++)
                {
                    units.Add(CreateUnit(j, i));
                }
            }
            return units;
        }

        /// <summary>
        /// Function that creats a unit.
        /// </summary>
        /// <param name="XCordinate"></param>
        /// <param name="YCordinate"></param>
        /// <returns></returns>
        private static Unit CreateUnit(int XCordinate, int YCordinate)
        {
            return new Unit()
            {
                IsActive = true,
                XCordinate = XCordinate,
                YCordinate = YCordinate,
                Ship = new()
            };
        }
    }
}
