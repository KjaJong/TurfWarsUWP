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
    static class TestDriverMapCommanderAndSuch
    {
        public static async void TestAll(MapControl toUse)
        {

            GameLogic g = new GameLogic();
            MapControl mc = toUse;
            MapCommander m = new MapCommander(mc, g);

            await m.InitializeBingMapsAsync();

            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = 51.6611540;
            pos.Longitude = 4.8584070;

            Geopoint point = new Geopoint(pos);


            m.CreateGeofence("TestFence 1", point, 20.0, TimeSpan.Zero);
            m.CreateMapIcon("TestPoint 1", point);

            Debug.WriteLine("Everything has been intialized. Starting the s̶t̶a̶l̶k̶i̶n̶g̶  monitoring of senpai and the geofence (*^^*)");
            g.AddCapturePoint(point, 100);
        }
    }
}
