using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turf_war_UWP.Powers;
using Turf_war_UWP.Teams;

namespace Turf_war_UWP
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

            Team = team;

            _experience = 0;
            _expToNextLvl = 100;
        }
    }
}
