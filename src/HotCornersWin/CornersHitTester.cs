namespace HotCornersWin
{
    /// <summary>
    /// A helper class to determine whether a hot corner of a display was reached.
    /// </summary>
    public static class CornersHitTester
    {
        /// <summary>
        /// Dimensions (Width, Height) and locations (X, Y) of system screen(s).
        /// </summary>
        public static Rectangle[] Screens
        {
            set
            {
                _cornerCoords.Clear();
                // Calculate each screen's corners coordinates and add to the enumeration.
                foreach (var screen in value)
                {
                    Point coordLT = new(screen.X, screen.Y);
                    _cornerCoords.TryAdd(coordLT, Corners.LeftTop);
                    Point coordLB = new(screen.X, screen.Height + screen.Y);
                    _cornerCoords.TryAdd(coordLB, Corners.LeftBottom);
                    Point coordRT = new(screen.Width + screen.X, screen.Y);
                    _cornerCoords.TryAdd(coordRT, Corners.RightTop);
                    Point coordRB = new(screen.Width + screen.X, screen.Height + screen.Y);
                    _cornerCoords.TryAdd(coordRB, Corners.RightBottom);
                }
            }
        }

        /// <summary>
        /// A radius of a screen corner in pixels.
        /// </summary>
        public static int CornerRadius
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "Corner area size value must be positive");
                }
                _cornerRadius = value;
            }
        }

        private static readonly Dictionary<Point, Corners> _cornerCoords = new();

        private static int _cornerRadius = 5;

        /// <summary>
        /// Test if the mouse cursor is within a screen corner.
        /// </summary>
        /// <param name="coords">The cursor coordinates.</param>
        /// <returns>The screen corner that matches the supplied coordinates,
        /// otherwise Corners.None.</returns>
        public static Corners HitTest(Point coords)
        {
            foreach (var pair in _cornerCoords)
            {
                Point diff = coords.AbsDiff(pair.Key);
                if (diff.X <= _cornerRadius && diff.Y <= _cornerRadius)
                {
                    if (diff.Hypotenuse() <= _cornerRadius)
                    {
                        return pair.Value;
                    }
                }
            }
            return Corners.None;
        }
    }
}
