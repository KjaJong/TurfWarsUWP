using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;

namespace Turf_Wars
{
    public class BingMapsWrapper
    {
        public MapControl BingMapsControl { get; }
        private Geolocator _geolocator;
        public MapIcon UserLocationIcon { get; set; }
        public GeolocationAccessStatus AccessStatus { get; set; }
        public BasicGeoposition UserPosition { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bingMapsControl"></param>
        public BingMapsWrapper(MapControl bingMapsControl)
        {
            BingMapsControl = bingMapsControl;
            UserLocationIcon = new MapIcon() { Visible = false, Title = "Position", NormalizedAnchorPoint = new Point(0.5, 1.0) };
            bingMapsControl.MapElements.Add(UserLocationIcon);

            AccessStatus = GeolocationAccessStatus.Unspecified;
        }

        /// <summary>
        /// Setup the bing maps api wrapper.
        /// </summary>
        /// <returns>true if setup was succesfull</returns>
        public async Task<bool> InitializeAsync()
        {
            try
            {
                BingMapsControl.Loaded += MapLoadedAsync;

                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Exception: {exception.Message}\n{exception.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// The method which is called by the map when it is loaded.
        /// This method will 
        /// it should NOT be called manually!
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">The event arguments given by the maps</param>
        private async void MapLoadedAsync(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Map loaded");
            AccessStatus = await Geolocator.RequestAccessAsync();
            switch (AccessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    _geolocator = new Geolocator() { DesiredAccuracyInMeters = 5, ReportInterval = 2000 };
                    await FocusOnUserLocationAsync();
                    _geolocator.PositionChanged += OnPositionChangedAsync;
                    //SetRoute(); TO-DO: Add list of monuments for testing
                    break;
                case GeolocationAccessStatus.Denied:
                    //throw new NotImplementedException();
                    break;
                case GeolocationAccessStatus.Unspecified:
                    throw new NotImplementedException();
                    break;
            }
        }

        /// <summary>
        /// Method which is called by the geolocator if the position of the user has changed.
        /// This method should NOT be called manually!
        /// It will update the _userLocationIcon based on the new location
        /// </summary>
        /// <param name="sender">The geolocator which has triggerd the event</param>
        /// <param name="e">The event given by the geolocator</param>
        private async void OnPositionChangedAsync(Geolocator sender, PositionChangedEventArgs e)
        {
            await GetUserLocationAsync();
        }

        /// <summary>
        /// Focus the map view on the user location
        /// </summary>
        public async Task FocusOnUserLocationAsync()
        {
            var position = await GetUserLocationAsync();
            await BingMapsControl.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(position, 1000));
        }

        /// <summary>
        /// Get the current location of the user
        /// </summary>
        /// <returns>The location of the user</returns>
        public async Task<Geopoint> GetUserLocationAsync()
        {
            var position = await _geolocator.GetGeopositionAsync();
            var basicPosition = new BasicGeoposition()
            {
                Altitude = position.Coordinate.Point.Position.Altitude,
                Latitude = position.Coordinate.Point.Position.Latitude,
                Longitude = position.Coordinate.Point.Position.Longitude
            };
            UserPosition = basicPosition;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    UserLocationIcon.Location = position.Coordinate.Point;
                    if (!UserLocationIcon.Visible)
                    {
                        UserLocationIcon.Visible = true;
                    }
                });
            return new Geopoint(basicPosition);
        }

    }
}