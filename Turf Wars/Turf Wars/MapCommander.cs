using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Maps;
using Turf_Wars.Pages;

namespace Turf_Wars
{
    class MapCommander
    {
        private readonly MapControl _controller;
        private readonly GameLogic _gameLogic;

        public BingMapsWrapper Wrapper { get; set; }

        public MapCommander(MapControl c, GameLogic g)
        {
            _controller = c;
            Wrapper = new BingMapsWrapper(_controller);
            _gameLogic = g;
            FinnishSetup();
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
            var fence = new Geofence(fenceId, new Geocircle(p.Position, radius), states, false, t);
            GeofenceMonitor.Current.Geofences.Add(fence);
            foreach (Geofence g in GeofenceMonitor.Current.Geofences)
            {
                Debug.WriteLine(g.Id);
            }
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

        public async Task InitializeBingMapsAsync()
        {
            // Clear the geofence and add the trigger method
            GeofenceMonitor.Current.Geofences.Clear();
            GeofenceMonitor.Current.GeofenceStateChanged += OnGeofenceStateChangedAsync;
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

        public async void OnGeofenceStateChangedAsync(GeofenceMonitor sender, object e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var report in sender.ReadReports())
                {
                    var state = report.NewState;

                    var geofence = report.Geofence;

                    if (state == GeofenceState.Removed)
                    {
                        GeofenceMonitor.Current.Geofences.Remove(geofence);
                    }
                    else if (state == GeofenceState.Entered)
                    {
                        Debug.WriteLine("Senpai entered the fence! (♥ω♥*)");
                        if (GamePage.Player.Team is Teams.TeamBlue) { _gameLogic.CurrentPoint.BluePlayersInZone.Add(GamePage.Player); }
                        if (GamePage.Player.Team is Teams.TeamYellow) { _gameLogic.CurrentPoint.YellowPlayersInZone.Add(GamePage.Player); }
                        if (GamePage.Player.Team is Teams.TeamRed) { _gameLogic.CurrentPoint.RedPlayersInZone.Add(GamePage.Player); }
                    }
                    else if (state == GeofenceState.Exited)
                    {
                        Debug.WriteLine("Senpai left ( ≧Д≦)");
                        if (GamePage.Player.Team is Teams.TeamBlue) { _gameLogic.CurrentPoint.BluePlayersInZone.Remove(GamePage.Player); }
                        if (GamePage.Player.Team is Teams.TeamYellow) { _gameLogic.CurrentPoint.YellowPlayersInZone.Remove(GamePage.Player); }
                        if (GamePage.Player.Team is Teams.TeamRed) { _gameLogic.CurrentPoint.RedPlayersInZone.Remove(GamePage.Player); }
                    }
                }
            });
        }

        private async void FinnishSetup()
        {
            var access = await Geolocator.RequestAccessAsync();
            access = GeolocationAccessStatus.Allowed;
        }

        private async void reloadMap(Geopoint g)
        {
            //await _controller.TrySetViewBoundsAsync();
        }
    }
}
