using System;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Tank : PowerUp
    {
        public Tank(int cost, string description) : base(cost, description)
        {
            Name = "Tank";
            CoolDown = TimeSpan.FromSeconds(5);
            LevelRestriction = 2;
            PowerUpType = PowerUps.Tank;
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