using System.Collections.Generic;
using Windows.Devices.Geolocation;
using System.Collections.ObjectModel;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class Player
    {
        public string Name;
        public string Email;
        public string Password;
        
        public int Level;
        public int Coinz { get; set; }
        public Team Team;

        public int Experience;
        public double ExpToNextLvl;

        public ObservableCollection<PowerUp> Powers;
        public BasicGeoposition Geoposition { get; set; }

        public Player() { }

        public Player(string name, string password, string email)
        {
            Name = name;
            Email = email;
            Password = password;

            Level = 1;
            Coinz = 50;
            Experience = 0;
            ExpToNextLvl = 100;

            Team = new NoTeam(0);
            Powers = new ObservableCollection<PowerUp>();
        }

        public bool CheckLogin(string name, string password)
        {
            return Name.Equals(name) && Password.Equals(password);
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

        public override string ToString()
        {
            return  $"[{Name}]" +
                    $"[{Email}]" +
                    $"[{Password}]" +
                    $"[{Level}]" +
                    $"[{Coinz}]" +
                    $"[{Experience}]" +
                    $"[{ExpToNextLvl}]" +
                    $"[{Team.Name}]" +
                    $"[{Powers.Count}]";
        }
    }
}
