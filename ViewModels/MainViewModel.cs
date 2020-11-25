using System.Linq;
using CoordinateTracker.Models;
using CoordinateTracker.Views;

namespace CoordinateTracker.ViewModels
{
    internal class MainViewModel
    {
        public bool CanAddCoordinates { get; set; }
        public World CurrentWorld;

        /// <summary>
        /// Creates a new view model for the Main view.
        /// </summary>
        public MainViewModel()
        {
            CanAddCoordinates = false;
        }

        /// <summary>
        /// Gets the world with a specified name.
        /// </summary>
        /// <param name="name">The name of the world</param>
        /// <returns>The found world.</returns>
        public World GetWorld(string name)
        {
            World foundWorld = null;

            foreach (var world in World.Worlds.Where(world => world.Name == name))
                foundWorld = world;

            return foundWorld;
        }

        /// <summary>
        /// Parses the coordinates entered by the user.
        /// </summary>
        /// <param name="rawCoordinates">The coordinates entered.</param>
        /// <returns>The array of coordinates.</returns>
        public double[] ParseRawCoordinates(string[] rawCoordinates)
        {
            var coordinates = new double[rawCoordinates.Length];

            if (coordinates.Length < 2 || coordinates.Length > 3)
            {
                MainView.DisplayError("There should be either two or three coordinates.");
                return null;
            }

            if (!rawCoordinates.Where((t, index) => !double.TryParse(t, out coordinates[index])).Any())
                return coordinates;

            MainView.DisplayError("The coordinates entered are not valid.");
            return null;
        }
    }
}
