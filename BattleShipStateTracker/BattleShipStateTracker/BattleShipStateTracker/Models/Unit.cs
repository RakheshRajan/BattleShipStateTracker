namespace BattleShipStateTracker.Models
{
    public class Unit
    {
        public int XCordinate { get; set; }
        public int YCordinate { get; set; }
        public bool IsActive { get; set; }

        public Ship Ship { get; set; }
    }
}
