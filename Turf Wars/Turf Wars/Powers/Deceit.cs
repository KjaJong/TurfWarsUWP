using System;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Deceit : PowerUp
    {
        public Deceit(int cost, string description) : base(cost, description)
        {
            Name = "Deceit";
            CoolDown = TimeSpan.FromSeconds(2);
            LevelRestriction = 5;
            PowerUpType = PowerUps.Deceit;
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