using BattleShipStateTracker.Models;
using System.Collections.Generic;

namespace BattleShipStateTracker.Provider
{
    public interface IUnitProvider
    {
        List<Unit> CreateUnits();
    }
}
