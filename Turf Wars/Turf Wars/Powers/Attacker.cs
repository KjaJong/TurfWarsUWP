namespace Turf_Wars.Powers
{
    public class Attacker : PowerUp
    {
        public Attacker(int cost, string description) : base(cost, description)
        {
            Name = "Attacker";
            CoolDown = 7;
            LevelRestriction = 1;
            PowerUpType = PowerUps.Attacker;
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