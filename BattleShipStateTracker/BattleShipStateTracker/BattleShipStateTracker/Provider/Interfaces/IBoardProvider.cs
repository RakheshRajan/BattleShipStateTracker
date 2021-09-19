using BattleShipStateTracker.Models;

namespace BattleShipStateTracker.Provider
{
    public interface IBoardProvider
    {
        Board CreateBoard(int id, string boardName);
    }
}
