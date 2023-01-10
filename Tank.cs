using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
    internal class Tank
    {
        float speed = 0;
        float maxSpeed = 10f;
        float acceleration = 0.5f;
        float turnRate = 0.5f;
        float rotation = 0f;
        int hitpoints = 100;

        float fireCooldown = 0.8f;

        Texture2D sprite;
        Vector2 drawOffset;
        Vector2 position;

        bool isAlive = true;

        Turret turreet = new Turret();

        public Tank(Vector2 position, Texture2D sprite)
        {
            this.position = position;
            this.sprite = sprite;
            drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }
    



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1,1), SpriteEffects.None);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void getHit(Projectile projectile)
        {

        }

        public void getInput()
        {

        }
    }
}
