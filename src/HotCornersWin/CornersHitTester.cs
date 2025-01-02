using System.Diagnostics;

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
                _screens.Clear();
                _corners.Clear();
                _screens.AddRange(value);
                // Calculate each screen's corners coordinates and add to the enumeration.
                foreach (var screen in value)
                {
                    Dictionary<Point, Corners> cornerCoordinates = [];
                    Point coordLT = new(screen.X, screen.Y);
                    cornerCoordinates.TryAdd(coordLT, Corners.LeftTop);
                    Point coordLB = new(screen.X, screen.Height + screen.Y);
                    cornerCoordinates.TryAdd(coordLB, Corners.LeftBottom);
                    Point coordRT = new(screen.Width + screen.X, screen.Y);
                    cornerCoordinates.TryAdd(coordRT, Corners.RightTop);
                    Point coordRB = new(screen.Width + screen.X, screen.Height + screen.Y);
                    cornerCoordinates.TryAdd(coordRB, Corners.RightBottom);
                    _corners.Add(cornerCoordinates);
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

        /// <summary>
        /// System screens dimensions.
        /// </summary>
        private static readonly List<Rectangle> _screens = [];
        /// <summary>
        /// System screen corners and their coordinates.
        /// </summary>
        private static readonly List<Dictionary<Point, Corners>> _corners = [];
        /// <summary>
        /// Index of the screen that current cursor is located in or -1 if not determined.
        /// </summary>
        private static int _screenIndex = -1;
        /// <summary>
        /// Radius of the area near a corner that a hit is detected in.
        /// </summary>
        private static int _cornerRadius = 5;

        /// <summary>
        /// Test if the mouse cursor is within a screen corner.
        /// </summary>
        /// <param name="coords">The cursor coordinates.</param>
        /// <returns>The screen corner that matches the supplied coordinates,
        /// otherwise Corners.None.</returns>
        public static Corners HitTest(Point coords)
        {
            //Debug.WriteLine($"{coords.X}; {coords.Y}");
            for (int i=0; i< _screens.Count; i++)
            {
                if (_screens[i].Contains(coords))
                {
                    _screenIndex = i;
                    break;
                }
                _screenIndex = -1;
            }
            if (_screenIndex < 0 || _screenIndex >= _corners.Count)
            {
                return Corners.None;
            }
            foreach (var pair in _corners[_screenIndex])
            {
                Point diff = coords.AbsDiff(pair.Key);
                if (diff.X <= _cornerRadius && diff.Y <= _cornerRadius)
                {
                    if (diff.Hypotenuse() <= _cornerRadius)
                    {
                        //Debug.WriteLine($"Screen {_screenIndex} hit");
                        return pair.Value;
                    }
                }
            }
            return Corners.None;
        }
    }
}
