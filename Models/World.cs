using System.Collections.Generic;

namespace CoordinateTracker.Models
{
    public class World
    {
        public string Name { get; }
        public List<Coordinate> Coordinates { get; set; }

        public static List<World> Worlds { get; } = new List<World>();

        /// <summary>
        /// Initializes a new instance of <see cref="World"/>.
        /// </summary>
        /// <param name="name">The name of the world.</param>
        public World(string name)
        {
            if (string.IsNullOrEmpty(name))
                name = "Unknown";

            Name = name;

            Coordinates = new List<Coordinate>();
            Worlds.Add(this);
        }
    }
}
