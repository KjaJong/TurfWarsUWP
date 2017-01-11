using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    /// <summary>
    /// Contains the scoring mechanic for the pop up point
    /// </summary>
    public class Pup
    {
        private int RedScore { get; set; }
        private int BlueScore { get; set; }
        private int YellowScore { get; set; }

        public List<Player> RedPlayersInZone;
        public List<Player> YellowPlayersInZone;
        public List<Player> BluePlayersInZone;

        private readonly CapturePoint _currentPoint;
        private readonly Timer _timer;
        private readonly Timer _territoryTimer;
        private readonly Timer _fightTimer;

        private readonly ManualResetEvent _timerEvent = new ManualResetEvent(false);
        //private readonly GeoLocation location { get;}

        void TerritoryTask()
        {
            _territoryTimer.Change(TimeSpan.FromSeconds(1).Milliseconds,
                Timeout.Infinite);
            //TODO if this fucks up change this shit
            while (BlueScore + YellowScore + RedScore < _currentPoint.Reward) {Task.Delay(250);}
            _timerEvent.Set();
        }

        public Pup(CapturePoint c)
        {
            RedPlayersInZone = new List<Player>();
            YellowPlayersInZone = new List<Player>();
            BluePlayersInZone = new List<Player>();
            _currentPoint = c;

            //So you can't start or stop this thing (explains why I loved the other timer). This is wonky but should do the trick.
            _timer = new Timer(TickFuckingTock, null, Timeout.Infinite, Timeout.Infinite);
            _territoryTimer = new Timer(DistibruteTerritory, null, Timeout.Infinite, Timeout.Infinite);
            _fightTimer = new Timer(Fight, null, Timeout.Infinite, Timeout.Infinite);
            StartCapture();
        }

        /// <summary>
        /// The method used to score points for the alloted time.
        /// </summary>
        private void StartCapture()
        {
            _timer.Change(TimeSpan.FromMinutes(5), Timeout.InfiniteTimeSpan);

            Task territoryTimerTask = new Task(TerritoryTask);
            territoryTimerTask.Start();

            _timerEvent.WaitOne();
            _timerEvent.Reset();

            _territoryTimer.Dispose();
            _fightTimer.Change(TimeSpan.FromSeconds(5).Milliseconds, Timeout.Infinite);
        }

        /// <summary>
        /// Awards money and experience based on points captured
        /// </summary>
        private void AwardMoneyAndExp()
        {
            foreach (Player p in RedPlayersInZone)
            {
                p.Coinz += RedScore/2;
                p.AddExperience(RedScore);
            }

            foreach (Player p in BluePlayersInZone)
            {
                p.Coinz += BlueScore/2;
                p.AddExperience(BlueScore);
            }

            foreach (Player p in YellowPlayersInZone)
            {
                p.Coinz = YellowScore/2;
                p.AddExperience(YellowScore);
            }
        }

        #region Timer shenennigans. Contains all the tick events, so to speak (they aren't ticks)
        private async void TickFuckingTock(object state)
        {
            await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                _fightTimer.Dispose();
                AwardMoneyAndExp();
                _timer.Dispose();
            });
        }

        private async void DistibruteTerritory(object state)
        {
            await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                if (BlueScore >= YellowScore && BlueScore >= RedScore) BlueScore++;
                if (YellowScore >= BlueScore && YellowScore >= RedScore) YellowScore++;
                if (RedScore >= YellowScore && RedScore >= BlueScore) RedScore++;
            });
        }

        private async void Fight(object state)
        {
            await Window.Current.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            () =>
            {
                if (Math.Max(BluePlayersInZone.Count, RedPlayersInZone.Count) == BluePlayersInZone.Count &&
                Math.Max(BluePlayersInZone.Count, YellowPlayersInZone.Count) == BluePlayersInZone.Count)
                {
                    BlueScore += 2 * BluePlayersInZone.Count;
                    RedScore -= BluePlayersInZone.Count;
                    YellowScore -= BluePlayersInZone.Count;
                }
                else if (Math.Max(YellowPlayersInZone.Count, RedPlayersInZone.Count) == YellowPlayersInZone.Count &&
                         Math.Max(YellowPlayersInZone.Count, BluePlayersInZone.Count) == YellowPlayersInZone.Count)
                {
                    YellowScore += 2 * YellowPlayersInZone.Count;
                    RedScore -= YellowPlayersInZone.Count;
                    BlueScore -= YellowPlayersInZone.Count;
                }
                else if (Math.Max(RedPlayersInZone.Count, BluePlayersInZone.Count) == RedPlayersInZone.Count &&
                         Math.Max(RedPlayersInZone.Count, YellowPlayersInZone.Count) == RedPlayersInZone.Count)
                {
                    RedScore += 2 * RedPlayersInZone.Count;
                    BlueScore -= RedPlayersInZone.Count;
                    YellowScore -= RedPlayersInZone.Count;
                }
            });
        }
        #endregion
    }
}