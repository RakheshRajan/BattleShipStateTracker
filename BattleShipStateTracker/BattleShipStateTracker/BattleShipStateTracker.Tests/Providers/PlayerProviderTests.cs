using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using BattleShipStateTracker.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using FluentAssertions;


namespace BattleShipStateTracker.Tests.Providers
{
    public class PlayerProviderTests : IClassFixture<TestFixture>
    {
        public TestFixture TestFixture { get; }
        public IPlayerProvider PlayerProvider { get; set; }

        public IConfiguration Configuration { get; set; }

        public Board Board { get; set; }



        public PlayerProviderTests(TestFixture testFixture)
        {
            TestFixture = testFixture;
            Configuration = testFixture.MockConfiguration;
            PlayerProvider = new PlayerProvider(TestFixture.GetMockBoard(), Configuration);
        }

        /// <summary>
        /// CreatePlayer should return true for known players.
        /// </summary>
        [Fact]
        public void CreatePlayer_ShouldReturnTrue_ForStandardInput()
        {
            bool result = PlayerProvider.CreatePlayer(Configuration["GameSettings:DefaultPlayerName"].ToString());
            result.Should().BeTrue();
        }

        /// <summary>
        /// CreatePlayer should return false for unknown/empty players.
        /// </summary>
        [Fact]
        public void CreatePlayer_ShouldReturnFalse_ForEmptyPlayer()
        {
            bool result = PlayerProvider.CreatePlayer(string.Empty);
            result.Should().BeFalse();
        }

        /// <summary>
        /// CreatePlayer should return false for emptyboard.
        /// </summary>
        [Fact]
        public void CreatePlayer_ShouldReturnFalse_ForEmptyBoard()
        {
            IPlayerProvider playerProvider = new PlayerProvider(TestFixture.GetMockEmptyBoard(), Configuration);
            bool result = playerProvider.CreatePlayer(Configuration["GameSettings:DefaultPlayerName"].ToString());
            result.Should().BeFalse();
        }

        /// <summary>
        /// GetPlayer should return value for standard input.
        /// </summary>
        [Fact]
        public void GetPlayer_ShouldNotbeNull_ForStandardInput()
        {
            Player player = PlayerProvider.GetPlayer(Configuration["GameSettings:DefaultPlayerName"].ToString());
            player.Should().NotBeNull();
        }

        /// <summary>
        /// GetPlayer should be null for unknown/empty players.
        /// </summary>
        [Fact]
        public void GetPlayer_ShouldbeNull_ForUnknownPlayer()
        {
            Player player = PlayerProvider.GetPlayer(string.Empty);
            player.Should().BeNull();
        }

        /// <summary>
        /// Fire should return false for cordinates without Ship.
        /// </summary>
        [Fact]
        public void Fire_ShouldReturnFalse_ForPlayerWithOutShips()
        {
            Player player = PlayerProvider.GetPlayer(Configuration["GameSettings:DefaultPlayerName"].ToString());
            FireResult fireResult = PlayerProvider.Fire(60, 60, player);
            fireResult.Should().NotBeNull();
            fireResult.Succeded.Should().BeFalse();
        }

        /// <summary>
        /// Fire should return false for empty players.
        /// </summary>
        [Fact]
        public void Fire_ShouldReturnFalse_ForEmptyPlayer()
        {
            Player player = new();
            FireResult fireResult = PlayerProvider.Fire(60, 60, player);
            fireResult.Should().NotBeNull();
            fireResult.Succeded.Should().BeFalse();
        }
    }
}
