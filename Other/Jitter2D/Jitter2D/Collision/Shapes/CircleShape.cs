﻿/* Copyright (C) <2009-2011> <Thorben Linneweber, Jitter Physics>
* 
*  This software is provided 'as-is', without any express or implied
*  warranty.  In no event will the authors be held liable for any damages
*  arising from the use of this software.
*
*  Permission is granted to anyone to use this software for any purpose,
*  including commercial applications, and to alter it and redistribute it
*  freely, subject to the following restrictions:
*
*  1. The origin of this software must not be misrepresented; you must not
*      claim that you wrote the original software. If you use this software
*      in a product, an acknowledgment in the product documentation would be
*      appreciated but is not required.
*  2. Altered source versions must be plainly marked as such, and must not be
*      misrepresented as being the original software.
*  3. This notice may not be removed or altered from any source distribution. 
*/

#region Using Statements
using System;
using System.Collections.Generic;

using Jitter2D.Dynamics;
using Jitter2D.LinearMath;
using Jitter2D.Collision.Shapes;
#endregion

namespace Jitter2D.Collision.Shapes
{

    /// <summary>
    /// A <see cref="Shape"/> representing a sphere.
    /// </summary>
    public class CircleShape : Shape
    {
        private float radius = 1.0f;

        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        public float Radius { get { return radius; } set { radius = value; UpdateShape(); } }

        /// <summary>
        /// Creates a new instance of the SphereShape class.
        /// </summary>
        /// <param name="radius">The radius of the sphere</param>
        public CircleShape(float radius)
        {
            this.radius = radius;
            this.type = ShapeType.Circle;
            this.UpdateShape();
        }

        /// <summary>
        /// Returns true if the point is inside the circle.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>True if the point is inside the circle.</returns>
        public override bool PointInsideLocal(JVector point)
        {
            return (point.LengthSquared() <= (radius * radius));
        }

        /// <summary>
        /// Should return true if the point is inside the circles world space.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="position">World position of circle.</param>
        /// <param name="orientation">World orientation of circle.</param>
        /// <returns>True if the point is inside the shape.</returns>
        public override bool PointInsideWorld(JVector point, JVector position, JMatrix orientation)
        {
            return ((point - position).LengthSquared() <= (radius * radius));
        }

        /// <summary>
        /// SupportMapping. Finds the point in the shape furthest away from the given direction.
        /// Imagine a plane with a normal in the search direction. Now move the plane along the normal
        /// until the plane does not intersect the shape. The last intersection point is the result.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="result">The result.</param>
        public override void SupportMapping(ref JVector direction, out JVector result)
        {
            result = direction;
            result.Normalize();

            JVector.Multiply(ref result, radius, out result);
        }

        /// <summary>
        /// Calculates the bounding box of the sphere.
        /// </summary>
        /// <param name="orientation">The orientation of the shape.</param>
        /// <param name="box">The resulting axis aligned bounding box.</param>
        public override void GetBoundingBox(ref float orientation, out JBBox box)
        {
            box.Min.X = -radius -0.01f;
            box.Min.Y = -radius -0.01f;
            box.Max.X = radius +0.01f;
            box.Max.Y = radius +0.01f;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CalculateMassInertia()
        {
            mass = Density * JMath.Pi * radius * radius;

            // inertia about the local origin
            inertia = mass * Radius * Radius / 4f;
        }

        public override void UpdateAxes(float orientation)
        {
            throw new NotImplementedException();
        }
    }

}
