using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Foundation.Metadata;
using Turf_Wars.Powers;
using Turf_Wars.Teams;

namespace Turf_Wars.DataWriting
{
    public class SaveLoadUtil
    {
        /// <summary>
        /// Methode for saving an entire player in the local data storage.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static async Task SavePlayerNames(Player player)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var playerNamesFile =
                await storageFolder.CreateFileAsync($"{player.Name}.txt",
                    Windows.Storage.CreationCollisionOption.ReplaceExisting);

            var names = player.ToString();

            foreach (var p in player.Powers)
            {
                names += $"[{(int)p.PowerUpType}]";
            }

            Debug.WriteLine(names);

            await Windows.Storage.FileIO.WriteTextAsync(playerNamesFile, names);
        }

        /// <summary>
        /// Methode for loading an entire player from the local data storage.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<Player> LoadPlayerNames(string name)
        {
            try
            {
                var playerNames = new List<string>();

                var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var playerNamesFile =
                    await storageFolder.GetFileAsync($"{name}.txt");

                var text = await Windows.Storage.FileIO.ReadTextAsync(playerNamesFile);

                var texts = text.Split('[', ']');
                foreach (var s in texts)
                {
                    if (s.Equals("")) continue;
                    playerNames.Add(s);
                }

                Team team = null;
                if (playerNames[7].Equals("No team")) team = new NoTeam(0);
                if (playerNames[7].Equals("Blue team")) team = new TeamBlue(0);
                if (playerNames[7].Equals("Red team")) team = new TeamRed(0);
                if (playerNames[7].Equals("Yellow team")) team = new TeamYellow(0);

                var powerUps = new ObservableCollection<PowerUp>();

                for (var i = 1; i <= int.Parse(playerNames[8]); i++)
                {
                    powerUps.Add(GameLogic.PowerUps[int.Parse(playerNames[i + 8])]);
                    GameLogic.PowerUps[int.Parse(playerNames[i + 8])].IsBought = true;
                }

                var player = new Player()
                {
                    Name = playerNames[0],
                    Email = playerNames[1],
                    Password = playerNames[2],
                    Level = int.Parse(playerNames[3]),
                    Coinz = int.Parse(playerNames[4]),
                    Experience = int.Parse(playerNames[5]),
                    ExpToNextLvl = double.Parse(playerNames[6]),
                    Team = team,
                    Powers = powerUps
                };

                player.Powers = new ObservableCollection<PowerUp>(player.Powers.OrderBy(x => x.PowerUpType)); //LINQ
                return player;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                return null;
            }
        }

        public static async void DeleteAllStorage()
        {
            await Windows.Storage.ApplicationData.Current.ClearAsync();
        }
    }
}