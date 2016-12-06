using System.Collections.Generic;
using Turf_Wars.Pages;

namespace Turf_Wars
{
    public class GameLogic
    {
        public PUP CurrentPoint { get; set; }
        
        private List<Player> players;
        //private readonly BingMapsWrapper MapWrapper;
        private readonly IPageHandler pageHandler;

        public GameLogic()
        {
            
        }
    }
}