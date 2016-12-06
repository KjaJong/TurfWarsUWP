namespace Turf_Wars.Powers
{
    public abstract class PowerUp
    {
        public readonly int Cost;
        public bool IsBought;

        //TODO: Must be a time.
        public double CoolDown;
        public int LevelRestriction;
        public readonly string Description;

        protected PowerUp(int cost, double coolDown, int levelRestriction, string description)
        {
            Cost = cost;
            CoolDown = coolDown;
            LevelRestriction = levelRestriction;
            Description = description;
        }

        public abstract void Activate();
        public abstract void Buy();
    }
}