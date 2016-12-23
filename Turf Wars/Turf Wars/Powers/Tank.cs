using System;

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
            throw new System.NotImplementedException();
        }

        public override void Buy()
        {
            IsBought = true;
        }
    }
}