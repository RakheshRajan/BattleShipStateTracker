using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Tests.Helpers
{
    public class TestFixture : IDisposable
    {
        public IConfiguration MockConfiguration { get; set; }
        public IUnitProvider MockUnitProvider { get; set; }
        public TestFixture()
        {
            MockConfiguration = GetMockConfiguration();
            MockUnitProvider = GetMockUnitProvider(MockConfiguration);
        }

        private static IUnitProvider GetMockUnitProvider(IConfiguration MockConfiguration)
        {
            return new UnitProvider(MockConfiguration);
        }

        private static IConfiguration GetMockConfiguration()
        {
            var configurations = new Dictionary<string, string>
            {
                {"GameSettings:BoardSettings:xCordinate","9" },
                {"GameSettings:BoardSettings:yCordinate","9" },
                {"GameSettings:BoardName","Board 1" },
                {"GameSettings:DefaultPlayerName","Player1" },
                {"GameSettings:ShipSettings:ShipSize","5" },
                {"GameSettings:ShipSettings:ShipDirection","Horizontal" },
                {"GameSettings:ShipSettings:NumberOfShips","10" },
            };

            IConfiguration mockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(configurations)
                .Build();

            return mockConfiguration;
        }

        /// <summary>
        /// Get a Mock Board
        /// </summary>
        /// <returns></returns>
        public static Board GetMockBoard()
        {
            var boardProvider = new BoardProvider(GetMockUnitProvider(GetMockConfiguration()));
            var result = boardProvider.CreateBoard(1, "Board 1");
            return result;
        }

        /// <summary>
        /// Get an empty board  object
        /// </summary>
        /// <returns></returns>
        public static Board GetMockEmptyBoard()
        {
            return new Board()
            {
                Id = 0,
                Name = string.Empty,
                Units = new List<Unit>()
            };
        }

        public void Dispose()
        {
            MockConfiguration = null;
        }
    }
}
