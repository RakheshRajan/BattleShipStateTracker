using BattleShipStateTracker.Models;

namespace BattleShipStateTracker.Provider
{
    public interface IShipProvider
    {
        Board CreateShips(int numberOfShips, Board board);
    }
}
