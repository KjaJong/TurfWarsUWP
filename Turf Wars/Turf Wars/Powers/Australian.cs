using System;
using System.Threading.Tasks;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Australian : PowerUp
    {
        public Australian(int cost, string description) : base(cost, description)
        {
            Name = "Australian";
            CoolDownTime = TimeSpan.FromSeconds(1);
            LevelRestriction = 6;
            PowerUpType = PowerUps.Australian;
        }

        public override void Activate()
        {
            if (Active || GamePage.Player.IsInGeofence) return;
            foreach (var p in GamePage.Player.Powers) if (p.Active) return;

            Active = true;
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

        public void CoolDownAsync()
        {
            while (Active)
            {
                if (DateTime.Now >= ActivationTime + CoolDownTime) Active = false;
            }
        }
    }
}