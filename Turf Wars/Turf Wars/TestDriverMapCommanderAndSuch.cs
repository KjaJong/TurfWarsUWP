using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    static class TestDriverMapCommanderAndSuch
    {
        public static void TestAll()
        {
            GameLogic g = new GameLogic();
            MapControl mc = new MapControl();
            MapCommander m = new MapCommander(mc, g);
            Player p = new Player("Henk", "henk", "Henk@henk.com");

            BasicGeoposition playerPos = new BasicGeoposition();
            playerPos.Latitude = 51.5836920;
            playerPos.Longitude = 4.7963210;
            p.Geoposition = playerPos;

            BasicGeoposition pos = new BasicGeoposition();
            pos.Latitude = 51.6616390;
            pos.Longitude = 4.8586370;

            Geopoint point = new Geopoint(pos);

            m.CreateGeofence("TestFence 1", point, 5.0, TimeSpan.FromSeconds(3));
            m.CreateMapIcon("TestPoint 1", point);

            Debug.WriteLine("Everything has been intialized. Starting the s̶t̶a̶l̶k̶i̶n̶g̶  monitoring of senpai and the geofence (*^^*)");
            Test1(p, playerPos, pos);
        }

        private static void Test1(Player p, BasicGeoposition start, BasicGeoposition moveTo)
        {
            int count = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(6);
            timer.Tick += (sender, args) =>
            {
                Debug.WriteLine("Tick tock senpai!");
                if(count % 2 == 0) { p.Geoposition = start; }
                else { p.Geoposition = moveTo; }
                count++;
            };

            timer.Start();
        }
    }
}
