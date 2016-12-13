using System.Collections.Generic;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class Player
    {
        public string Name;
        public string Email;
        private readonly string _password;
        
        public int Level;
        public int Coinz;
        public Team Team;

        private int _experience;
        private double _expToNextLvl;

        public static List<PowerUp> Powers = new List<PowerUp>();

        public Player(string name, string password, string email)
        {
            Name = name;
            Email = email;
            _password = password;

            Level = 1;
            Coinz = 0;
            _experience = 0;
            _expToNextLvl = 100;

            Team = new NoTeam(0);
        }

        public bool CheckLogin(string name, string password)
        {
            return Name == name && _password == password;
        }

        public void AddExperience(int exp)
        {
            _experience += exp;
            if (!(_experience >= _expToNextLvl)) return;

            Level++;
            _experience = _experience - (int)_expToNextLvl;
            _expToNextLvl *= 1.5;
        }
    }
}
