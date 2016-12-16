using System;
using System.Collections.Generic;

namespace Turf_Wars.Teams
{
    class TeamRed : Team
    {
        public TeamRed(int totalPoints) : base(totalPoints)
        {
        }

        protected override List<Player> GetAllPlayers()
        {
            throw new NotImplementedException();
        }

        protected override List<Player> GetAllPlayers(Pup currentPoint)
        {
            throw new NotImplementedException();
        }
    }
}
