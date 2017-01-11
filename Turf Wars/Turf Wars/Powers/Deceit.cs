using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Core;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Deceit : PowerUp
    {
        private bool _isActive;
        public Deceit(int cost, string description) : base(cost, description)
        {
            Name = "Deceit";
            CoolDownTime = TimeSpan.FromSeconds(300);
            LevelRestriction = 5;
            PowerUpType = PowerUps.Deceit;
        }

        public override void Activate()
        {
            if (_isActive) return;

            _isActive = true;
            ActivationTime = DateTime.Now;

            CoolDown();
        }

        public override void Buy()
        {
            IsBought = true;
        }

        public override async void CoolDown()
        {
            await Task.Run(() =>
            {
                CoolDownAsync();
            });
        }

        public async void CoolDownAsync()
        {
            while (Active)
            {
                if (DateTime.Now >= ActivationTime + CoolDownTime) Active = false;
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                  CoreDispatcherPriority.Normal,
                  SetTimeLeft);
            }

        }

        public void SetTimeLeft()
        {
            try
            {
                var tempTime = CoolDownTime - (DateTime.Now - ActivationTime);
                TimeLeft = $"Cooldown: {tempTime.Seconds} s";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
        }
    }
}