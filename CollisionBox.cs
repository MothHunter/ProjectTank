using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class CollisionBox
    {
        private float rotation;
        private Vector2 center;
        private float width;
        private float height;

        public float GetRotation () { return rotation; }
        public Vector2 GetCenter() { return center; }
        public float GetWidth () { return width; }
        public float GetHeight () { return height; }

        public CollisionBox(Vector2 center, float rotation, float width, float height)
        {
            this.center = center;
            this.rotation = rotation;
            this.width = width;
            this.height = height;
        }

        public void Update(float rotation, Vector2 center)
        {
            this.rotation = rotation;
            this.center = center;
        }

        public bool Contains(Vector2 position)
        {
            // note: ignores rotation of the collisionbox!
            if (position.X >= center.X - width/2 && position.X <=center.X + width/2 &&
                position.Y >= center.Y - height/2 && position.Y <= center.Y + height/2) 
            {
                return true;
            }
            return false;
        }

        public bool Collides(CollisionBox box2)
        {
            // note: ignores rotation of both collisionboxes!
            float b1X1 = center.X - width / 2;
            float b1X2 = center.X + width / 2;
            float b1Y1 = center.Y - height / 2;
            float b1Y2 = center.Y + height / 2;

            float b2X1 = box2.GetCenter().X - box2.GetWidth() / 2;
            float b2X2 = box2.GetCenter().X + box2.GetWidth() / 2;
            float b2Y1 = box2.GetCenter().Y - box2.GetHeight() / 2;
            float b2Y2 = box2.GetCenter().Y + box2.GetHeight() / 2;


            if (((b1Y1 > b2Y1 && b1Y1 < b2Y2) || (b1Y2 > b2Y1 && b1Y2 < b2Y2)) &&
               ((b1X1 > b2X1 && b1X1 < b2X2) || (b1X2 > b2X1 && b1X2 < b2X2)))
            {
                return true;
            }

            return false;
        }
    }
}
