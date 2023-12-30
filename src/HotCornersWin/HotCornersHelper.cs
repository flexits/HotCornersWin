﻿using System.Windows.Forms;

namespace HotCornersWin
{
    /// <summary>
    /// A helper class to determine whether a hot corner of a display was reached.
    /// </summary>
    public class HotCornersHelper
    {
        public delegate void CornerReachedHandler(Corners corner);

        /// <summary>
        /// The event will be invoked by the CornerHitTest method  
        /// if the mouse cursor have reached a screen corner.
        /// </summary>
        public event CornerReachedHandler? CornerReached;

        private readonly Dictionary<Point, Corners> _cornerCoords = new();

        private int _cornerAreaSize;

        private Corners _currentPosition = Corners.None;

        /// <summary>
        /// Dimensions (Width, Height) and locations (X, Y) of system screen(s).
        /// </summary>
        public Rectangle[] Screens
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
        /// A screen corner is a square area with side of the give size.
        /// </summary>
        public int CornerAreaSize
        {
            get { return _cornerAreaSize; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), 
                        "Corner area size value must be positive");
                }
                _cornerAreaSize = value;
            }
        }

        public HotCornersHelper(Rectangle[] screens, int cornerAreaSize = 5)
        {
            Screens = screens;
            CornerAreaSize = cornerAreaSize;
        }

        /// <summary>
        /// Test if the mouse cursor is in a screen corner.
        /// Invokes CornerReached event.
        /// The event will be invoked only once, 
        /// a repeated invocation is possible only after the cursor 
        /// leaves the corner area.
        /// </summary>
        /// <param name="coords">The cursor coordinates.</param>
        public void CornerHitTest(Point coords)
        {
            // hit test cursor
            Corners newPosition = Corners.None;
            foreach(var pair in _cornerCoords)
            {
                Point diff = coords.AbsDiff(pair.Key);
                if (diff.X <= _cornerAreaSize && diff.Y <= _cornerAreaSize)
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