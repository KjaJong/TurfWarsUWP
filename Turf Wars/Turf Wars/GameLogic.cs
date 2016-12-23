using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Turf_Wars.Pages;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class GameLogic
    {
        public Pup CurrentPoint { get; set; }

        private List<CapturePoint> _points;

        public static List<Player> Players = new List<Player>();
        public static ObservableCollection<PowerUp> PowerUps;
        private readonly BingMapsWrapper MapWrapper;

        public GameLogic()
        {
            _points = new List<CapturePoint>();
            PowerUps = new ObservableCollection<PowerUp>()
            {
                new Attacker(10, "Attack the infidels"),
                new Australian(100, "'Stralia!!"),
                new Deceit(1000, "Trator was a traitor!!! *gasp*"),
                new Tank(20, "Combine this with a medic (We don't have one, though)")
            };
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