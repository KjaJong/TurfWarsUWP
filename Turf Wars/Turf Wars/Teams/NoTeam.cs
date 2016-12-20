using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turf_Wars.Teams
{
    class NoTeam : Team
    {
        public NoTeam(int totalPoints) : base(totalPoints)
        {
            Name = "No team";
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
