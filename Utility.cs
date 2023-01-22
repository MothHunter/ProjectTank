using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ProjectTank
{
    internal static class Utility
    {
        /// <summary>
        /// Method <c>radToV2</c> creates a normalized (length = 1) Vector2 from an angle as radians.
        /// Resulting Vector2 already takes into account that x-axis is pointing downwards
        /// </summary>
        /// <param name="radians">angle</param>
        /// <returns>Vector2 of length 1 pointing in the direction given by the radians</returns>
        public static Vector2 radToV2(float radians)
        {
            return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }

        /// <summary>
        /// Method <c>V2ToRad</c> calculates the angle in radians from a vector.
        /// Takes into account that x-axis is pointing downwards.
        /// </summary>
        /// <param name="vector2">the vector</param>
        /// <returns>angle in radians</returns>
        public static float V2ToRad(Vector2 vector2)
        {
            return (float)Math.Atan2(vector2.Y, vector2.X);
        }
    }
}
