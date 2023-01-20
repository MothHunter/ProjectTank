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

        float rotation;
        float speed = 10f;
        int damage = 50;
        int remainingBounces = 1;
        Vector2 position;

        Texture2D sprite;
        Vector2 drawOffset;

        public Projectile(Vector2 position, Texture2D sprite, float rotation)
        {
            this.position = position;
            this.sprite = sprite;
            this.rotation = rotation;
            this.drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

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

        public void update()
        {
            position += Utility.radToV2(rotation) * speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1, 1), SpriteEffects.None, 1.0f);
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public int GetDamage()
        {
            return damage;
        }

        public int GetRemainingBounces() { return remainingBounces; }

        public void Reflect(CollisionBox box)
        {
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
                rotation = Utility.V2ToRad(new Vector2(currentVector.X, - currentVector.Y));
            }
        }
    }
}
