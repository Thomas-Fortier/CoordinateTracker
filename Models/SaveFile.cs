using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoordinateTracker.Models
{
    internal class SaveFile
    {
        private static string _path;

        /// <summary>
        /// Creates a new save file to save to.
        /// </summary>
        /// <param name="path">The path for the save file.</param>
        public SaveFile(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Save all worlds and their coordinates to the text file.
        /// </summary>
        /// <param name="worlds">The worlds to save.</param>
        public void Save(List<World> worlds)
        {
            using var streamWriter = new StreamWriter(_path);

            foreach (var world in worlds)
            {
                var coordinates = world.Coordinates;

                streamWriter.WriteLine('!' + world.Name);

                foreach (var coordinate in coordinates)
                {
                    streamWriter.WriteLine($"#{coordinate.Name},{coordinate.XCoordinate},{coordinate.YCoordinate},{coordinate.ZCoordinate}");
                }
            }
        }

        /// <summary>
        /// Loads the data from the text file.
        /// </summary>
        /// <returns>True if values were loaded. False if the file is empty.</returns>
        public static bool Load()
        {
            _path = "SaveFile.txt";

            CreateSaveFile();

            using var streamReader = new StreamReader(_path);

            const char worldCharacter = '!';
            const char coordinateCharacter = '#';
            const char separationValue = ',';
            var count = 0;
            string line;
            World world = null;

            while ((line = streamReader.ReadLine()) != null)
            {
                count++;

                if (line[0] == worldCharacter)
                    world = new World(RemoveSpecialCharacter(line, worldCharacter));

                if (line[0] != coordinateCharacter) continue;

                var splitValues = line.Split(separationValue);
                var name = RemoveSpecialCharacter(splitValues[0], coordinateCharacter);
                var xCoordinate = double.Parse(splitValues[1]);
                var yCoordinate = double.Parse(splitValues[2]);
                var zCoordinate = double.Parse(splitValues[3]);

                world?.Coordinates.Add(new Coordinate(xCoordinate, yCoordinate, zCoordinate, name));
            }

            return count != 0;
        }

        /// <summary>
        /// Removes specific special characters from a string.
        /// </summary>
        /// <param name="text">The text to evaluate.</param>
        /// <param name="specialCharacter">The special character to remove.</param>
        /// <returns>Returns a new string with the removed characters.</returns>
        private static string RemoveSpecialCharacter(string text, char specialCharacter)
        {
            var newText = text.Where(character => character != specialCharacter)
                .Aggregate("", (current, character) => current + character);

            return newText;
        }

        /// <summary>
        /// Creates a save file if it does not currently exist.
        /// </summary>
        private static void CreateSaveFile()
        {
            if (File.Exists(_path)) return;
            var file = File.CreateText(_path);
            file.Close();
        }
    }
}
