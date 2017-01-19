using System.Collections.Generic;
using Windows.Devices.Geolocation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Turf_Wars.Annotations;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars
{
    public class Player : INotifyPropertyChanged
    {
        private int _coinz;

        public string Name;
        public string Email;
        public string Password;
        
        public int Level;

        public int Coinz
        {
            get { return _coinz; }
            set
            {
                if(value == _coinz) return;
                _coinz = value;
                OnPropertyChanged();
            }
        }

        public Team Team;

        public int Experience;
        public double ExpToNextLvl;
        public bool IsInGeofence;

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

            Team = new NoTeam();
            Powers = new ObservableCollection<PowerUp>();
        }

        public bool CheckLogin(string name, string password)
        {
            return Name.Equals(name) && Password.Equals(password);
        }

        public void AddExperience(int exp)
        {
            Experience += exp;
            if (Experience < ExpToNextLvl) return;

            Experience = 0;
            Level++;
            Coinz += 10*Level;
            ExpToNextLvl *= 1.5;
            AddExperience(exp - (int)ExpToNextLvl);
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
