using System;
using System.Threading.Tasks;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Deceit : PowerUp
    {
        public Deceit(int cost, string description) : base(cost, description)
        {
            Name = "Deceit";
            CoolDownTime = TimeSpan.FromSeconds(2);
            LevelRestriction = 5;
            PowerUpType = PowerUps.Deceit;
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