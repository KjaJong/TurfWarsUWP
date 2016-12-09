using System.Collections.Generic;
using Turf_Wars.Pages;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class GameLogic
    {
        public PUP CurrentPoint { get; set; }

        public static List<Player> Players = new List<Player>();
        //private readonly BingMapsWrapper MapWrapper;
        private readonly IPageHandler _pageHandler;

        public GameLogic()
        {
        }
    }
}