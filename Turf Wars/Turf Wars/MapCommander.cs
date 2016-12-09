using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Xaml.Controls.Maps;

namespace Turf_Wars
{
    class MapCommander
    {
        private readonly MapControl _controller;
        private List<Geopoint> _points;
        private List<Geofence> _fences;

        public MapCommander(MapControl c)
        {
            _controller = c;
            _points = new List<Geopoint>();
            _fences = new List<Geofence>();
        }

        /// <summary>
        /// This adds a geopoint to the list
        /// </summary>
        /// <param name="latitude"> Latitude of the point. </param> //TODO enter a reference to the paramater between which it must fall
        /// <param name="longitude"> Longitude of the point </param> //TODO see latitude
        public void AddPoint(double latitude, double longitude)
        {
            _points.Add(new Geopoint(new BasicGeoposition()
            {
                Latitude = latitude,
                Longitude = longitude
            }));
        }

        ///<summary>
        /// This creates a geofence for the list. The point are never single use, so manual removal is neccesary.
        /// </summary>
        /// <param name="fenceId">The name of the fence</param>
        /// <param name="p">The location of the fence as a geopoint</param>
        /// <param name="radius">The radius of the circle of the fence</param>
        /// <param name="state">The monitor state. It can monitor for entry, exit, removal of the point or have no monitor</param>
        /// <param name="t">The timespan that indicates how long a device needs to be inside the fence to trigger the event.</param>
        public void CreateGeofence(string fenceId, Geopoint p, double radius, MonitoredGeofenceStates state, TimeSpan t)
        {
            BasicGeoposition bgp = new BasicGeoposition();
            bgp.Latitude = p.Position.Latitude;
            bgp.Longitude = p.Position.Longitude;
            bgp.Altitude = p.Position.Altitude;

            _fences.Add(new Geofence(fenceId, new Geocircle(bgp, radius), state, false, t));
        }
    }
}
