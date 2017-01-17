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

        private bool _gatekeeper;
        //private AutoResetEvent waitEvent = new AutoResetEvent(false);

        public List<Player> RedPlayersInZone;
        public List<Player> YellowPlayersInZone;
        public List<Player> BluePlayersInZone;

        private readonly CapturePoint _currentPoint;
        private readonly Timer _timer;
        private readonly Timer _fightTimer;

        //private readonly GeoLocation location { get;}

        public Pup(CapturePoint c)
        {
            RedPlayersInZone = new List<Player>();
            YellowPlayersInZone = new List<Player>();
            BluePlayersInZone = new List<Player>();
            _currentPoint = c;

            //So you can't start or stop this thing (explains why I loved the other timer). This is wonky but should do the trick.
            _timer = new Timer(TickFuckingTock, null, Timeout.Infinite, Timeout.Infinite);
            _fightTimer = new Timer(Fight, null, Timeout.Infinite, Timeout.Infinite);
            StartCapture();
        }

        /// <summary>
        /// The method used to score points for the alloted time.
        /// </summary>
        private void StartCapture()
        {
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1));
            _fightTimer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(5));
            //waitEvent.WaitOne();
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
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                  CoreDispatcherPriority.Normal,
            () =>
            {
                _fightTimer.Dispose();
                AwardMoneyAndExp();
                //waitEvent.Set();
                _timer.Dispose();
            });
        }

        private async void Fight(object state)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                  CoreDispatcherPriority.Normal,
            () =>
            {
                if (_gatekeeper) //is true when all points in the reward are spend
                {
                    int redScore;
                    int blueScore;
                    int yellowScore;

                    CalcBonus(out redScore, out blueScore, out yellowScore);

                    if (Math.Max(blueScore, redScore) == blueScore &&
                    Math.Max(blueScore, yellowScore) == blueScore)
                    {
                        BlueScore += 2 * blueScore;
                        RedScore -= blueScore;
                        YellowScore -= blueScore;
                    }
                    else if (Math.Max(yellowScore, redScore) == yellowScore &&
                             Math.Max(yellowScore, blueScore) == yellowScore)
                    {
                        YellowScore += 2 * yellowScore;
                        RedScore -= yellowScore;
                        BlueScore -= yellowScore;
                    }
                    else if (Math.Max(redScore, blueScore) == redScore &&
                             Math.Max(redScore, yellowScore) == redScore)
                    {
                        RedScore += 2 * redScore;
                        BlueScore -= redScore;
                        YellowScore -= redScore;
                    }
                }
                else
                {
                    int redScore;
                    int blueScore;
                    int yellowScore;

                    CalcBonus(out redScore, out blueScore, out yellowScore);

                    if (blueScore >= yellowScore && blueScore >= redScore) BlueScore++;
                    if (yellowScore >= blueScore && yellowScore >= redScore) YellowScore++;
                    if (redScore >= yellowScore && redScore >= blueScore) RedScore++;

                    if (RedScore + BlueScore + YellowScore >= _currentPoint.Reward) { _gatekeeper = true; }
                }
            });
        }

        private void CalcBonus(out int tempRed , out int tempBlue , out int tempYellow )
        {
            tempBlue = BluePlayersInZone.Count;
            tempRed = RedPlayersInZone.Count;
            tempYellow = YellowPlayersInZone.Count;

            int blueBonus = 0;
            int redBonus = 0;
            int yellowBonus = 0;

           #region Logic hell for bonus things

            foreach (Player pr in RedPlayersInZone)
            {
                foreach (Powers.PowerUp ap in pr.Powers)
                {
                    switch (ap.PowerUpType)
                    {
                        case Powers.PowerUp.PowerUps.Attacker:
                            if (ap.Active)
                            {
                                redBonus++;
                            }
                            break;

                        case Powers.PowerUp.PowerUps.Tank:
                            if (ap.Active)
                            {
                                blueBonus--;
                                yellowBonus--;
                            }
                            break;
                    }
                }
            }

            foreach (Player pb in BluePlayersInZone)
            {
                foreach (Powers.PowerUp ap in pb.Powers)
                {
                    switch (ap.PowerUpType)
                    {
                        case Powers.PowerUp.PowerUps.Attacker:
                            if (ap.Active)
                            {
                                blueBonus++;
                            }
                            break;

                        case Powers.PowerUp.PowerUps.Tank:
                            if (ap.Active)
                            {
                                redBonus--;
                                yellowBonus--;
                            }
                            break;
                    }
                }
            }

            foreach (Player py in YellowPlayersInZone)
            {
                foreach (Powers.PowerUp ap in py.Powers)
                {
                    switch (ap.PowerUpType)
                    {
                        case Powers.PowerUp.PowerUps.Attacker:
                            if (ap.Active)
                            {
                                yellowBonus++;
                            }
                            break;

                        case Powers.PowerUp.PowerUps.Tank:
                            if (ap.Active)
                            {
                                redBonus--;
                                blueBonus--;
                            }
                            break;
                    }
                }

                tempRed += redBonus;
                tempBlue += blueBonus;
                tempYellow += yellowBonus;
            }
            #endregion
        }
        #endregion
    }
}