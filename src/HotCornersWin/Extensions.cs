﻿namespace HotCornersWin
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Calculates absolute difference between points' X and Y coordinates.
        /// </summary>
        /// <returns>new Point(|X2-X1|,|Y2-Y1|)</returns>
        public static Point AbsDiff(this Point op1, Point op2)
        {
            return new Point(Math.Abs(op1.X - op2.X), Math.Abs(op1.Y - op2.Y));
        }

        /// <summary>
        /// Calculates approximate length of the vector 0;0->X;Y
        /// </summary>
        public static int Hypotenuse(this Point op)
        {
            return Convert.ToInt32(Math.Sqrt(op.X * op.X + op.Y * op.Y));
        }
    }
}
