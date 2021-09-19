using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using BattleShipStateTracker.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using FluentAssertions;


namespace BattleShipStateTracker.Tests.Providers
{
    public class BoardProviderTests : IClassFixture<TestFixture>
    {
        public TestFixture TestFixture { get; }
        public IUnitProvider UnitProvider { get; }

        public IConfiguration Configuration { get; set; }

        public IBoardProvider BoardProvider { get; set; }

        public BoardProviderTests(TestFixture testFixture)
        {
            TestFixture = testFixture;
            Configuration = testFixture.MockConfiguration;
            UnitProvider = new UnitProvider(Configuration);
            BoardProvider = new BoardProvider(UnitProvider);
        }

        /// <summary>
        /// Should not get null value from CreateBoard for standard inputs
        /// </summary>
        [Fact]
        public void CreateBoard_ShouldNotBeNull_ForStandardInput()
        {
            var result = BoardProvider.CreateBoard(1, Configuration["GameSettings:BoardName"].ToString());
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Should get Board object as return value for standard inputs 
        /// </summary>
        [Fact]
        public void CreateBoard_ShouldReturnBoard_ForStandardInput()
        {
            var result = BoardProvider.CreateBoard(1, Configuration["GameSettings:BoardName"].ToString());
            result.Should().BeOfType<Board>();
        }

        /// <summary>
        ///  Should get proper board name as return value for standard inputs 
        /// </summary>
        [Fact]
        public void CreateBoard_ShouldReturnProperName_ForStandardInput()
        {
            var result = BoardProvider.CreateBoard(1, Configuration["GameSettings:BoardName"].ToString());
            result.Name.Should().BeEquivalentTo(Configuration["GameSettings:BoardName"].ToString());
        }

        /// <summary>
        ///  Should get proper board id as return value for standard inputs 
        /// </summary>
        [Fact]
        public void CreateBoard_ShouldReturnProperId_ForStandardInput()
        {
            var result = BoardProvider.CreateBoard(1, Configuration["GameSettings:BoardName"].ToString());
            result.Id.Should().Equals(1);
        }

        /// <summary>
        /// Should return atleast 1 unit for standard inputs 
        /// </summary>
        [Fact]
        public void CreateBoard_ShouldReturnUnits_ForStandardInput()
        {
            var result = BoardProvider.CreateBoard(1, Configuration["GameSettings:BoardName"].ToString());
            result.Units.Should().HaveCount(r => r > 0);
        }

        /// <summary>
        ///  Should return empty board for blank board name 
        /// </summary>
        [Fact]
        public void CreateBoard_ShouldReturnEmptyBoard_ForBlankBoardName()
        {
            var result = BoardProvider.CreateBoard(1, string.Empty);
            result.Should().BeNull();
        }
    }
}
