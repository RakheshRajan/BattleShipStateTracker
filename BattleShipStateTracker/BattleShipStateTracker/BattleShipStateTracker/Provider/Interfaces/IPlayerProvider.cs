using BattleShipStateTracker.Models;

namespace BattleShipStateTracker.Provider
{
    public interface IPlayerProvider
    {
        bool CreatePlayer(string name);
        Player GetPlayer(string name);
        public FireResult Fire(int x, int y, Player player);
    }
}
