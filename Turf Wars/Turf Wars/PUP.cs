using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        private readonly DispatcherTimer _timer;
        private bool _gateKeeper = true;
        //private readonly GeoLocation location { get;}

        public Pup(CapturePoint c)
        {
            RedPlayersInZone = new List<Player>();
            YellowPlayersInZone = new List<Player>();
            BluePlayersInZone = new List<Player>();
            _currentPoint = c;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 5, 0);

            _timer.Tick += (sender, args) =>
            {
                _timer.Stop();
                AwardMoneyAndExp();
                _gateKeeper = false;
            };

            StartCapture();
        }

        /// <summary>
        /// The method used to score points for the alloted time.
        /// </summary>
        private void StartCapture()
        {
            _timer.Start();

            while (_gateKeeper && (BlueScore + YellowScore + RedScore <= _currentPoint.Reward))
            {
                DispatcherTimer pointsCheck = new DispatcherTimer();
                pointsCheck.Interval = new TimeSpan(0, 0, 1);

                pointsCheck.Tick += (sender, args) =>
                {
                    if (BlueScore >= YellowScore && BlueScore >= RedScore) BlueScore++;
                    if (YellowScore >= BlueScore && YellowScore >= RedScore) YellowScore++;
                    if (RedScore >= YellowScore && RedScore >= BlueScore) RedScore++;
                };
            }

            while (_gateKeeper)
            {
                DispatcherTimer rewardTimer = new DispatcherTimer();
                rewardTimer.Interval = new TimeSpan(0, 0, 5);

                while (Math.Max(BluePlayersInZone.Count, RedPlayersInZone.Count) == BluePlayersInZone.Count &&
                       Math.Max(BluePlayersInZone.Count, YellowPlayersInZone.Count) == BluePlayersInZone.Count)
                {
                    rewardTimer.Tick += (sender, args) =>
                    {
                        BlueScore += 2*BluePlayersInZone.Count;
                        RedScore -= BluePlayersInZone.Count;
                        YellowScore -= BluePlayersInZone.Count;
                    };
                }

                while (Math.Max(YellowPlayersInZone.Count, RedPlayersInZone.Count) == YellowPlayersInZone.Count &&
                       Math.Max(YellowPlayersInZone.Count, BluePlayersInZone.Count) == YellowPlayersInZone.Count)
                {
                    rewardTimer.Tick += (sender, args) =>
                    {
                        YellowScore += 2 * YellowPlayersInZone.Count;
                        RedScore -= YellowPlayersInZone.Count;
                        BlueScore -= YellowPlayersInZone.Count;
                    };
                }

                while (Math.Max(RedPlayersInZone.Count, BluePlayersInZone.Count) == RedPlayersInZone.Count &&
                       Math.Max(RedPlayersInZone.Count, YellowPlayersInZone.Count) == RedPlayersInZone.Count)
                {
                    rewardTimer.Tick += (sender, args) =>
                    {
                        RedScore += 2 * RedPlayersInZone.Count;
                        BlueScore -= RedPlayersInZone.Count;
                        YellowScore -= RedPlayersInZone.Count;
                    };
                }
            }
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
    }
}