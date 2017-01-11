using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Turf_Wars.Annotations;

namespace Turf_Wars.Powers
{
    public abstract class PowerUp : INotifyPropertyChanged
    {
        public enum PowerUps
        {
            Attacker,
            Australian,
            Deceit,
            Tank
        }
        public int Cost { get; }

        private string _timeLeft = "Cooldown: 0 s";

        public string TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                if (value == _timeLeft) return;
                _timeLeft = value;
                OnPropertyChanged();
            }
        }

        public bool IsBought { get; set; }
        public string Name { get; set; }
        public PowerUps PowerUpType;
        
        public TimeSpan CoolDownTime { get; set; }
        public DateTime ActivationTime { get; set; }
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
        public abstract void CoolDown();
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}