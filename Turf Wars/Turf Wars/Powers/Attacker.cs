using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Attacker : PowerUp
    {
        public Attacker(int cost, string description) : base(cost, description)
        {
            Name = "Attacker";
            CoolDownTime = TimeSpan.FromSeconds(7);
            LevelRestriction = 1;
            PowerUpType = PowerUps.Attacker;
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