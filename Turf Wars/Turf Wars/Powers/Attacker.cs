﻿namespace Turf_war_UWP.Powers
{
    public class Attacker : PowerUp
    {
        public Attacker(int cost, double coolDown, int levelRestriction, string description) : base(cost, coolDown, levelRestriction, description)
        {
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }

        public override void Buy()
        {
            throw new System.NotImplementedException();
        }
    }
}