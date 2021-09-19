using BattleShipStateTracker.Models;
using BattleShipStateTracker.Provider;
using BattleShipStateTracker.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;
using FluentAssertions;


namespace BattleShipStateTracker.Tests.Providers
{
    public class UnitProviderTests : IClassFixture<TestFixture>
    {
        public TestFixture TestFixture { get; }
        public IConfiguration configuration;
        public IUnitProvider UnitProvider;

        public UnitProviderTests(TestFixture testFixture)
        {
            TestFixture = testFixture;
            configuration = testFixture.MockConfiguration;
            UnitProvider = new UnitProvider(configuration);
        }

        /// <summary>
        /// Checks if CreateUnits returns a object.
        /// </summary>
        [Fact]
        public void CreateUnits_ShouldNotbeNull_ForStandardInput()
        {
            var result = UnitProvider.CreateUnits();
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Checks if CreateUnits returns more than 0 units for standard input.
        /// </summary>
        [Fact]
        public void CreateUnits_ShouldReturnUnits_ForStandardInput()
        {
            var result = UnitProvider.CreateUnits();
            result.Should().HaveCount(r => r > 0);
        }
    }
}
