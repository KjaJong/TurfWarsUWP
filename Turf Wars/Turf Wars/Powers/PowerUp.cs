using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Turf_Wars.Annotations;

namespace Turf_Wars.Powers
{
    public abstract class PowerUp
    {
        public enum PowerUps
        {
            Attacker,
            Australian,
            Deceit,
            Tank
        }
        public int Cost { get; }
        public bool IsBought { get; set; }
        public string Name { get; set; }
        public PowerUps PowerUpType;

        //TODO: Must be a time.
        public TimeSpan CoolDown { get; set; }
        public bool Active { get; set; }
        public int LevelRestriction { get; set; }
        public string Description { get; }

        protected PowerUp(int cost, string description)
        {
            Cost = cost;
            Description = description;
        }

        public abstract void Activate();
        public abstract void Buy();
    }
}