using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Turf_Wars
{
    static class DataOpslag
    {
        private static readonly StorageFolder Storage = ApplicationData.Current.LocalFolder;
        private static StorageFile _playerData;

        /// <summary>
        /// The method to write to a file.
        /// </summary>
        /// <param name="name">The name of the file. ONLY INPUT SOMETHING HERE WHEN YOU NEED A NEW FILE!</param>
        /// <param name="input">The text to write to the file.</param>
        public static async void WriteToFile(string name, string input)
        {
            if (String.IsNullOrEmpty(name))
            {
                _playerData = await Storage.CreateFileAsync(_playerData.Name, CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                _playerData = await Storage.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
            }
            await FileIO.WriteTextAsync(_playerData, input);
        }

        /// <summary>
        /// Reads from the playerdata contained in the called instance.
        /// </summary>
        /// <returns>A task to read data</returns>
        public static async Task ReadFromFile()
        {
            await FileIO.ReadTextAsync(_playerData);
        }
    }
}
