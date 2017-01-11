using System;
using System.Threading.Tasks;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Tank : PowerUp
    {
        public Tank(int cost, string description) : base(cost, description)
        {
            Name = "Tank";
            CoolDownTime = TimeSpan.FromSeconds(5);
            LevelRestriction = 2;
            PowerUpType = PowerUps.Tank;
        }
        public override void Activate()
        {
            if (GamePage.Player.IsInGeofence || Active) return;
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