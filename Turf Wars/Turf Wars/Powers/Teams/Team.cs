﻿using System.Collections.Generic;

namespace Turf_Wars.Teams
{
    public abstract class Team
    {
        public int TotalPoints { get; set; }
        public string Name { get; set; }

        protected abstract List<Player> GetAllPlayers();
        protected abstract List<Player> GetAllPlayers(Pup currentPoint);
    }
}