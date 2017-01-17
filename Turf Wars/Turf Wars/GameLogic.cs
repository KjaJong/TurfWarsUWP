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

        private List<Pup> _points;

        public static ObservableCollection<PowerUp> PowerUps = new ObservableCollection<PowerUp>()
        {
                new Attacker(10, "Attack the infidels"),
                new Australian(100, "'Stralia!!"),
                new Deceit(1000, "Trator was a traitor!!! *gasp*"),
                new Tank(20, "Combine this with a medic (We don't have one, though)")
        };

        private readonly BingMapsWrapper MapWrapper;

        public GameLogic()
        {
            _points = new List<Pup>();
        }

        public void AddCapturePoint(Geopoint g, int reward)
        {
            var pup =  new Pup(new CapturePoint(g, reward));
            CurrentPoint = pup;
           _points.Add(pup);
        }

        public static void ResetPowerUps()
        {
            PowerUps = new ObservableCollection<PowerUp>()
            {
                new Attacker(10, "Attack the infidels"),
                new Australian(100, "'Stralia!!"),
                new Deceit(1000, "Trator was a traitor!!! *gasp*"),
                new Tank(20, "Combine this with a medic (We don't have one, though)")
            };
        }

        public void CleanFromList(Pup p)
        {
            _points.Remove(p);
        }
    }

    public class CapturePoint
    {
        public Geopoint Point { get; }
        public readonly int Reward;

        public CapturePoint(Geopoint point, int reward)
        {
            Point = point;
            Reward = reward;
        }
    }
}