using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Devices.Geolocation.Geofencing;
using Turf_Wars.Pages;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class GameLogic
    {
        public PUP CurrentPoint { get; set; }

        private List<CapturePoint> _points;

        public static List<Player> Players = new List<Player>();
        public static ObservableCollection<PowerUp> PowerUps;
        private readonly BingMapsWrapper MapWrapper;


        public GameLogic()
        {
            _points = new List<CapturePoint>();

            PowerUps = new ObservableCollection<PowerUp>()
            {
                new Attacker(100, "Something"),
                new Australian(10, "Stralia motherfucker"),
                new Deceit(1000, "Something"),
                new Tank(20, "Something")
            };
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