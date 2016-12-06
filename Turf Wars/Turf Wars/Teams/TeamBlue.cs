using System.Collections.Generic;

namespace Turf_war_UWP.Teams
{
    public class TeamBlue : Team
    {
        public TeamBlue(int totalPoints) : base(totalPoints)
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