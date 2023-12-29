using System.Windows.Forms;

namespace HotCornersWin
{
    public class MoveProcessor
    {
        public delegate void CornerReachedHandler(Corners corner);

        /// <summary>
        /// The event will be invoked by the CornerHitTest method  
        /// if the mouse cursor have reached a screen corner.
        /// </summary>
        public event CornerReachedHandler? CornerReached;

        public required Rectangle[] Screens
        {
            set
            {
                _cornerCoords.Clear();
                // Calculate each screen's corners coordinates and add to the enumeration.
                foreach (var screen in value)
                {
                    Point coordLT = new(screen.X, screen.Y);
                    Point coordLB = new(screen.X, screen.Height + screen.Y);
                    Point coordRT = new(screen.Width + screen.X, screen.Y);
                    Point coordRB = new(screen.Width + screen.X, screen.Height + screen.Y);
                    _cornerCoords.TryAdd(coordLT, Corners.LeftTop);
                    _cornerCoords.TryAdd(coordLB, Corners.LeftBottom);
                    _cornerCoords.TryAdd(coordRT, Corners.RightTop);
                    _cornerCoords.TryAdd(coordRB, Corners.RightBottom);
                }
            }
        }

        // TODO settings
        private const int SENSITIVITY = 5;
        
        private readonly Dictionary<Point, Corners> _cornerCoords = new();

        private Corners _currentPosition = Corners.None;

        /// <summary>
        /// Test if the mouse cursor is in a screen corner.
        /// Invokes CornerReached event.
        /// </summary>
        /// <param name="coords">The cursor coordinates.</param>
        public void CornerHitTest(Point coords)
        {
            // hit test cursor
            Corners newPosition = Corners.None;
            foreach(var pair in _cornerCoords)
            {
                Point diff = coords.AbsDiff(pair.Key);
                if (diff.X <= SENSITIVITY && diff.Y <= SENSITIVITY)
                {
                    newPosition = pair.Value;
                    break;
                }
            }
            if (newPosition == Corners.None)
            {
                _currentPosition = Corners.None;
            }
            else if (_currentPosition == Corners.None) // invoke only once
            {
                _currentPosition = newPosition;
                CornerReached?.Invoke(newPosition);
            }
        }
    }
}
