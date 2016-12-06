using System.Collections.Generic;

namespace Turf_Wars.Teams
{
    public class TeamYellow : Team
    {
        public TeamYellow(int totalPoints) : base(totalPoints)
        {
        }

        protected override List<Player> GetAllPlayers()
        {
            throw new System.NotImplementedException();
        }

        protected override List<Player> GetAllPlayers(PUP currentPoint)
        {
            throw new System.NotImplementedException();
        }
    }
}