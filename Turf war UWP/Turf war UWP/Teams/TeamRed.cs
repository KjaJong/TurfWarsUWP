using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turf_war_UWP.Teams
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

        protected override List<Player> GetAllPlayers(PUP currentPoint)
        {
            throw new NotImplementedException();
        }
    }
}
