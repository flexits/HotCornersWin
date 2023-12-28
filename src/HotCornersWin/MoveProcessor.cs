namespace HotCornersWin
{
    public class MoveProcessor
    {
        public delegate void CornerReachedHandler(Corners corner);

        public event CornerReachedHandler? CornerReached;

        private const int SENSITIVITY = 2;

        private readonly Rectangle _screenBounds;

        private Corners _currentPosition;

        public MoveProcessor(Rectangle screenBounds)
        {
            _screenBounds = screenBounds;
            _currentPosition = Corners.None;
        }

        public void CornerHitTest(Point coords)
        {
            Corners newPosition = Corners.None;
            if (coords.Y - _screenBounds.Y < SENSITIVITY)
            {
                if (coords.X - _screenBounds.X < SENSITIVITY)
                {
                    // LeftTop x=0 y=0
                    newPosition = Corners.LeftTop;
                }
                else if (_screenBounds.Width - coords.X < SENSITIVITY)
                {
                    // RightTop x=width y=0
                    newPosition = Corners.RightTop;
                }
            }
            else if (_screenBounds.Height - coords.Y < SENSITIVITY)
            {
                if (coords.X - _screenBounds.X < SENSITIVITY)
                {
                    // LeftBottom x=0 y=height
                    newPosition = Corners.LeftBottom;
                }
                else if (_screenBounds.Width - coords.X < SENSITIVITY)
                {
                    // RightBottom x=width y=height
                    newPosition = Corners.RightBottom;
                }
            }
            if (newPosition == Corners.None)
            {
                _currentPosition = Corners.None;
            }
            else if (_currentPosition == Corners.None)
            {
                _currentPosition = newPosition;
                CornerReached?.Invoke(newPosition);
            }
        }

        /*
        public void CornerHitTest(Point coords)
        {
            Corners newPosition = Corners.None;
            if (coords.Y < SENSITIVITY)
            {
                if (coords.X < SENSITIVITY)
                {
                    // LeftTop x=0 y=0
                    newPosition = Corners.LeftTop;
                }
                else if (_screenSize.Width - coords.X < SENSITIVITY)
                {
                    // RightTop x=width y=0
                    newPosition = Corners.RightTop;
                }
            }
            else if (_screenSize.Height - coords.Y < SENSITIVITY)
            {
                if (coords.X < SENSITIVITY)
                {
                    // LeftBottom x=0 y=height
                    newPosition = Corners.LeftBottom;
                }
                else if (_screenSize.Width - coords.X < SENSITIVITY)
                {
                    // RightBottom x=width y=height
                    newPosition = Corners.RightBottom;
                }
            }
            if (newPosition == Corners.None)
            {
                _currentPosition = Corners.None;
            }
            else if (_currentPosition == Corners.None)
            {
                _currentPosition = newPosition;
                CornerReached?.Invoke(newPosition);
            }
        }
        */
    }
}
