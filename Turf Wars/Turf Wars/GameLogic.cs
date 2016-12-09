using System.Collections.Generic;
using Windows.Devices.Geolocation.Geofencing;
using Turf_Wars.Pages;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class GameLogic
    {
        public PUP CurrentPoint { get; set; }

        private List<CapturePoint> _points;

        public static List<Player> Players = new List<Player>();
        private readonly BingMapsWrapper MapWrapper;
        private readonly IPageHandler _pageHandler;

        public GameLogic()
        {
            _points = new List<CapturePoint>();
        }

        public void AddCapturePoint(Geofence f, int reward)
        {
            _points.Add(new CapturePoint(f, reward));
        }
    }

    public class CapturePoint
    {
        public Geofence Point { get; }
        public readonly int reward;

        public CapturePoint(Geofence fence, int reward)
        {
            Point = fence;
            this.reward = reward;
        }
    }
}