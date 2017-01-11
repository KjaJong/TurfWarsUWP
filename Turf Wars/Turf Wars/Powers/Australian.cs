using System;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Australian : PowerUp
    {
        public Australian(int cost, string description) : base(cost, description)
        {
            Name = "Australian";
            CoolDown = TimeSpan.FromSeconds(1);
            LevelRestriction = 6;
            PowerUpType = PowerUps.Australian;
        }

        public override void Activate()
        {
            if (!GamePage.Player.IsInGeofence) return;
            throw new System.NotImplementedException();
        }

        public override void Buy()
        {
            IsBought = true;
        }
    }
}