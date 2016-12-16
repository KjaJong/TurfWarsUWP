using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Maps;

namespace Turf_Wars
{
    class MapCommander
    {
        private readonly MapControl _controller;
        private List<Geofence> _fences;

        public MapCommander(MapControl c)
        {
            _controller = c;
            _fences = new List<Geofence>();
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChanged;
        }

        ///<summary>
        /// This creates a geofence for the list. The point are never single use, so manual removal is neccesary.
        /// </summary>
        /// <param name="fenceId">The name of the fence</param>
        /// <param name="p">The location of the fence as a geopoint</param>
        /// <param name="radius">The radius of the circle of the fence</param>
        /// <param name="t">The timespan that indicates how long a device needs to be inside the fence to trigger the event.</param>
        public void CreateGeofence(string fenceId, Geopoint p, double radius, TimeSpan t)
        {
            MonitoredGeofenceStates states = MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited | MonitoredGeofenceStates.Removed;
            _fences.Add(new Geofence(fenceId, new Geocircle(p.Position, radius), states, false, t));
        }

        /// <summary>
        /// Creates a map icon on a point
        /// </summary>
        /// <param name="title">Name of the point</param> todo so how are we going to generate these
        /// <param name="p">the point for which the icon is needed</param>
        public void CreateMapIcon(string title, Geopoint p)
        {
            MapIcon m = new MapIcon();
            m.Location = p;
            m.NormalizedAnchorPoint = new Point(0.5, 1.0);
            m.Title = title;

            _controller.MapElements.Add(m);
        }

        public void CenterOnPoint(Geopoint g)
        {
            _controller.Center = g;
        }

        public void UpdatePoints(List<CapturePoint> capturePoints)
        {
            int count = 1;
            _controller.MapElements.Clear();
            foreach (CapturePoint c in capturePoints)
            {
                CreateMapIcon(count.ToString(), c.Point);
                count++;
            }
        }

        public async void OnGeofenceStateChanged(GeofenceMonitor sender, object e)
        {
            var reports = sender.ReadReports();

            foreach (GeofenceStateChangeReport report in reports)
            {
                GeofenceState state = report.NewState;

                Geofence geofence = report.Geofence;

                if (state == GeofenceState.Removed)
                {
                    // Remove the geofence from the geofences collection.
                    GeofenceMonitor.Current.Geofences.Remove(geofence);
                }
                else if (state == GeofenceState.Entered)
                {
                    //TODO (also for exiting) recognize the player that enters and add them (or remove, depending on which trigger) sorted on team
                }
                else if (state == GeofenceState.Exited)
                {
                    // Your app takes action based on the exited event.

                    // NOTE: You might want to write your app to take a particular
                    // action based on whether the app has internet connectivity.

                }
            }
        }
    }
}
