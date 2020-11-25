namespace CoordinateTracker.Models
{
    public class Coordinate
    {
        public string Name { get; }
        public double XCoordinate { get; }
        public double YCoordinate { get; }
        public double ZCoordinate { get; }
        public string FullCoordinate { get; private set; }

        private readonly bool _doesYCoordinateExist;

        /// <summary>
        /// Initializes a new <see cref="Coordinate"/> object with an X, Y, and Z value.
        /// </summary>
        /// <param name="xCoordinate">The X coordinate</param>
        /// <param name="yCoordinate">The Y coordinate</param>
        /// <param name="zCoordinate">The Z coordinate</param>
        /// <param name="name">(Optional) The name of the coordinate.</param>
        public Coordinate(double xCoordinate, double yCoordinate, double zCoordinate, string name = "Unknown")
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            ZCoordinate = zCoordinate;
            Name = name;

            _doesYCoordinateExist = true;

            SetFullCoordinate();
        }

        /// <summary>
        /// Initializes a new <see cref="Coordinate"/> object with an X and Z value.
        /// </summary>
        /// <param name="xCoordinate">The X coordinate</param>
        /// <param name="zCoordinate">The Z coordinate</param>
        /// <param name="name">(Optional) The name of the coordinate.</param>
        public Coordinate(double xCoordinate, double zCoordinate, string name = "Unknown")
        {
            XCoordinate = xCoordinate;
            ZCoordinate = zCoordinate;
            Name = name;

            _doesYCoordinateExist = false;

            SetFullCoordinate();
        }

        /// <summary>
        /// Sets the <see cref="FullCoordinate"/> property
        /// which represents the full coordinates as a string.
        /// </summary>
        private void SetFullCoordinate()
        {
            FullCoordinate = _doesYCoordinateExist ? $"X: {XCoordinate} | Y: {YCoordinate} | Z: {ZCoordinate}" :
                $"X: {XCoordinate} | Y: ? | Z: {ZCoordinate}";
        }
    }
}
