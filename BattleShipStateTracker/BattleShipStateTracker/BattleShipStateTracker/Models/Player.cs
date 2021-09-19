namespace BattleShipStateTracker.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Board board { get; set; }
    }
}
