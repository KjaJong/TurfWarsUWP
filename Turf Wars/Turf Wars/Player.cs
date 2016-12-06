using System.Collections.Generic;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class Player
    {
        public string Name;
        public string Password;
        public int Level;
        public int Coinz;
        public readonly Team Team;

        private int _experience;
        private int _expToNextLvl;

        public List<PowerUp> Powers;

        public Player(string name, string password, Team team)
        {
            Name = name;
            Password = password;

            Level = 1;
            Coinz = 0;
            _experience = 0;
            _expToNextLvl = 100;

            Team = team;

        }
    }
}
