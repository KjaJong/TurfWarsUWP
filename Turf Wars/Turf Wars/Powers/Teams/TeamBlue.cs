using System.Collections.Generic;

namespace Turf_Wars.Teams
{
    public class TeamBlue : Team
    {
        public TeamBlue()
        {
            Name = "Blue team";
        }

        protected override List<Player> GetAllPlayers()
        {
            throw new System.NotImplementedException();
        }

        protected override List<Player> GetAllPlayers(Pup currentPoint)
        {
            throw new System.NotImplementedException();
        }
    }
}