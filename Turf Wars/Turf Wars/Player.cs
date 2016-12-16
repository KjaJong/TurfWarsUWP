using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public int Experience;
        public double ExpToNextLvl;

        public ObservableCollection<PowerUp> Powers;

        public Player(string name, string password, string email)
        {
            Name = name;
            Email = email;
            _password = password;

            Level = 1;
            Coinz = 50;
            Experience = 0;
            ExpToNextLvl = 100;

            Team = new NoTeam(0);
            Powers = new ObservableCollection<PowerUp>();
        }

        public bool CheckLogin(string name, string password)
        {
            return Name.Equals(name) && _password.Equals(password);
        }

        public void AddExperience(int exp)
        {
            Experience += exp;
            if (!(Experience >= ExpToNextLvl)) return;

            Level++;
            Coinz += 10*Level;
            Experience = Experience - (int)ExpToNextLvl;
            ExpToNextLvl *= 1.5;
        }
    }
}
