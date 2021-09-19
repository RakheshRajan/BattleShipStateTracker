using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using BattleShipStateTracker.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;

namespace BattleShipStateTracker.Tests.Providers
{
    public class ShipProviderTests : IClassFixture<TestFixture>
    {
        public TestFixture TestFixture { get; }
        public IConfiguration Configuration;
        IShipProvider MockShipProvider { get; set; }


        public ShipProviderTests(TestFixture testFixture)
        {
            TestFixture = testFixture;
            Configuration = testFixture.MockConfiguration;
            MockShipProvider = new ShipProvider(Configuration);
        }

        /// <summary>
        /// Should return a value when standard input is provided
        /// </summary>
        [Fact]
        public void CreateShips_ShouldNotbeNull_ForStandardInput()
        {            
            Board mockBoard = TestFixture.GetMockBoard();
            var result = MockShipProvider.CreateShips(int.Parse(Configuration["GameSettings:ShipSettings:NumberOfShips"].ToString()), mockBoard);
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Should return null value when empty board is provided
        /// </summary>
        [Fact]
        public void CreateShips_ShouldBeNull_WhenNoBoardProvided()
        {
            Board mockBoard = TestFixture.GetMockEmptyBoard();
            var result = MockShipProvider.CreateShips(int.Parse(Configuration["GameSettings:ShipSettings:NumberOfShips"].ToString()), mockBoard);
            result.Should().BeNull();
        }

        /// <summary>
        /// Should return a board when standard input is provided
        /// </summary>
        [Fact]
        public void CreateShips_ShouldReturnBoard_ForStandardInput()
        {
            Board mockBoard = TestFixture.GetMockBoard();
            var result = MockShipProvider.CreateShips(int.Parse(Configuration["GameSettings:ShipSettings:NumberOfShips"].ToString()), mockBoard);
            result.Should().BeOfType<Board>();
        }

        /// <summary>
        /// Should return a board with ships when standard input is provided
        /// </summary>
        [Fact]
        public void CreateShips_ShouldReturnBoardWithShipsAllocated_ForStandardInput()
        {
            Board mockBoard = TestFixture.GetMockBoard();
            var result = MockShipProvider.CreateShips(int.Parse(Configuration["GameSettings:ShipSettings:NumberOfShips"].ToString()), mockBoard);
            result.Units.Where(x => x.Ship.Id != 0).Should().HaveCount(s => s > 0);
        }
    }
}
