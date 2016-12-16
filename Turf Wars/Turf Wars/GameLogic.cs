using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Turf_Wars.Pages;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class GameLogic
    {
        public Pup CurrentPoint { get; set; }

        private List<CapturePoint> _points;

        public static List<Player> Players = new List<Player>();
        private readonly BingMapsWrapper MapWrapper;
        private readonly IPageHandler _pageHandler;

        public GameLogic()
        {
            _points = new List<CapturePoint>();
        }

        public void AddCapturePoint(Geofence f, Geopoint g, int reward)
        {
            _points.Add(new CapturePoint(f, g, reward));
        }
    }

    public class CapturePoint
    {
        public Geofence Fence { get; }
        public Geopoint Point { get; }
        public readonly int Reward;

        public CapturePoint(Geofence fence, Geopoint point, int reward)
        {
            Fence = fence;
            Point = point;
            Reward = reward;
        }
    }
}