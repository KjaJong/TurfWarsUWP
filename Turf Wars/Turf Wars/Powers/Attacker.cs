using System;
using Turf_Wars.Pages;

namespace Turf_Wars.Powers
{
    public class Attacker : PowerUp
    {
        public Attacker(int cost, string description) : base(cost, description)
        {
            Name = "Attacker";
            CoolDown = TimeSpan.FromSeconds(7);
            LevelRestriction = 1;
            PowerUpType = PowerUps.Attacker;
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