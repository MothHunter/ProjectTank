using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    public class Projectile
    {

        float rotation; // direction in which the projectile travels
        float speed = 10f; // speed at which the projectile travels
        int damage = 50; // damage the projectile does on impact
        int remainingBounces = 1; // remaining bounces off of indestructable objects
        Vector2 position; // current position of the projectile

        Texture2D sprite; // graphic
        Vector2 drawOffset; // offset

        /// <summary>
        /// Constructor 1 used for the standard shot
        /// </summary>
        /// <param name="position">position of the projectile</param>
        /// <param name="sprite">the graphic for projectile</param>
        /// <param name="rotation">the direction the projectile travels</param>
        public Projectile(Vector2 position, Texture2D sprite, float rotation)
        {
            this.position = position;
            this.sprite = sprite;
            this.rotation = rotation;
            this.drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        /// <summary>
        /// constructor 2 used for "shotgun shot"
        /// </summary>
        /// <param name="position">position of the projectile</param>
        /// <param name="sprite">graphic</param>
        /// <param name="rotation">direction the projectile travels to</param>
        /// <param name="speed">speed at which the projectile is traveling</param>
        /// <param name="maxBounce">maximum bounces off of indestructable objects</param>
        /// <param name="damage">damage the projectile does on impact</param>
        public Projectile(Vector2 position, Texture2D sprite, float rotation, float speed, 
                        int maxBounce, int damage)
        {
            this.position = position;
            this.sprite = sprite;
            this.rotation = rotation;
            this.speed = speed;
            remainingBounces= maxBounce;
            this.damage = damage;
            this.drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
        /// <summary>
        /// updates the position of the bullet by adding the speed multiplied by the rotation to the position
        /// </summary>
        public void update()
        {
            position += Utility.radToV2(rotation) * speed;
        }
        /// <summary>
        /// draws the projectile
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1, 1), SpriteEffects.None, 1.0f);
        }
        /// <summary>
        /// returns the current posiotion of the projectile as a Vector2
        /// </summary>
        /// <returns>the current position of the projectile</returns>
        public Vector2 getPosition()
        {
            return position;
        }
        /// <summary>
        /// returns the damage the bullet does as a int
        /// </summary>
        /// <returns>damage as int</returns>
        public int GetDamage()
        {
            return damage;
        }
        /// <summary>
        /// returns the remaining bounces the bullet has off of undestructable objects before exploding
        /// </summary>
        /// <returns>remaining bounces</returns>
        public int GetRemainingBounces() { return remainingBounces; }
        /// <summary>
        /// given a collisionBox the function bounces the projectile off of the collisionbox changing its rotation
        /// </summary>
        /// <param name="box">the collisionbox the projectile is reflected off of</param>
        public void Reflect(CollisionBox box)
        {
            // lowers the bounces remaining by 1
            remainingBounces -= 1;

            // find out which side the shot collided with
            float distanceLeftX = Math.Abs(position.X - (box.GetCenter().X - box.GetWidth() / 2));
            float distanceRighX = Math.Abs(position.X - (box.GetCenter().X + box.GetWidth() / 2));
            float distanceTopY = Math.Abs(position.Y - (box.GetCenter().Y - box.GetHeight() / 2));
            float distanceBottomY = Math.Abs(position.Y - (box.GetCenter().Y + box.GetHeight() / 2));

            float xDistance = Math.Min(distanceLeftX, distanceRighX);
            float yDistance = Math.Min(distanceTopY, distanceBottomY);

            Vector2 currentVector = Utility.radToV2(rotation);

            // set position back a bit so the shot does not go into the object first
            position -= Utility.radToV2(rotation) * (speed/2f);

            if (xDistance < yDistance)
            {
                // the shot was closer to an vertical edge than to a horizontal edge => reflect horizontally
                rotation = Utility.V2ToRad(new Vector2(- currentVector.X, currentVector.Y));
            }
            else
            {
                // the shot was closer to an horizontally edge than to a vertical edge => reflect vertical
                rotation = Utility.V2ToRad(new Vector2(currentVector.X, - currentVector.Y));
            }
        }
    }
}
