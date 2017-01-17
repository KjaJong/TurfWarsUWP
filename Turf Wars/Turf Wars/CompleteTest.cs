using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    static class CompleteTest
    {
        public static void TestAll(MapControl mc)
        {

            GameLogic g = new GameLogic();
            MapCommander m = new MapCommander(mc, g);

            m.InitializeBingMapsAsync();

            BasicGeoposition pos = new BasicGeoposition //OOSTERHOUT
            {
                Latitude = 51.6611540,
                Longitude = 4.8584070
            };

            BasicGeoposition pos2 = new BasicGeoposition //BREDA
            {
                Latitude = 51.5836920,
                Longitude = 4.7963210
            };

            Geopoint point = new Geopoint(pos);
            Geopoint point2 = new Geopoint(pos2);

            m.CreateGeofence("TestFence 1", point, 20.0, TimeSpan.Zero);
            m.CreateMapIcon("TestPoint 1", point);
            g.AddCapturePoint(point2, 5);

            Debug.WriteLine("Everything has been intialized. Starting the s̶t̶a̶l̶k̶i̶n̶g̶  monitoring of senpai and the geofence (*^^*)");
        }
    }
}
