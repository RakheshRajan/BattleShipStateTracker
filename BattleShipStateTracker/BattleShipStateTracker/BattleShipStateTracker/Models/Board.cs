﻿using System.Collections.Generic;

namespace BattleShipStateTracker.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Unit> Units = new();
    }
}
