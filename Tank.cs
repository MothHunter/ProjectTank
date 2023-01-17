﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static ProjectTank.Game1;

namespace ProjectTank
{
    internal class Tank
    {
        float speed = 0;
        float maxSpeed = 10f;
        float acceleration = 0.5f;
        float turnRate = 0.05f;
        float rotation = 0f;
        int hitpoints = 100;
        float hightTank = 20;
        float widthTank = 20;

        float fireCooldown = 0.8f;
        Texture2D sprite;
        Vector2 drawOffset;
        Vector2 position;

        bool isAlive = true;

        Turret turreet = new Turret();

        CollisionBox tankCollision;

        public Tank(Vector2 position, Texture2D sprite)
        {
            this.position = position;
            this.sprite = sprite;
            drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.tankCollision = new CollisionBox(position, rotation, widthTank, hightTank);
        }
    



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1,1), SpriteEffects.None, 1.0f);
        }

        public void Update(GameTime gameTime)
        {
            InputController input = InputController.GetInstance();
            if (input.GetKeyDown(Keys.W))
            {
                //Speed up
                speed = Math.Min(speed + acceleration, maxSpeed);
            }
            else if (input.GetKeyDown(Keys.S))
            {
                //slow down
                speed = Math.Max(speed - acceleration, -maxSpeed/2);
            }
            else if (speed < 0){
                speed = Math.Min(speed + acceleration * 0.35f, 0);
            }
            else if (speed > 0)
            {
                speed = Math.Max(speed - acceleration * 0.35f, 0);
            }
            if (input.GetKeyDown(Keys.A))
            {
                rotation = (rotation - turnRate) % (2 * (float)Math.PI);
            }
            if (input.GetKeyDown(Keys.D))
            {
                rotation = (rotation + turnRate) % (2 * (float)Math.PI);
            }
            if (speed != 0)
            {
                Vector2 positionOld = position;
                position += Utility.radToV2(rotation) * speed;
                tankCollision.Update(rotation, position);
                for (int i = 0; i < Game1.indestructible.Count; i++)
                {
                    if (tankCollision.Collides(Game1.indestructible[i]))
                    {
                        position = positionOld;
                        tankCollision.Update(rotation, positionOld);
                        speed = 0;
                    }
                }
                for (int i = 0; i < Game1.destructible.Count; i++)
                {
                    if (tankCollision.Collides(Game1.destructible[i]))
                    {
                        position = positionOld;
                        tankCollision.Update(rotation, positionOld);
                        speed = 0;
                    }
                }

            }
        }

        public void getHit(Projectile projectile)
        {

        }
        
        public void getInput()
        {

        }

        public CollisionBox GetCollisionBox()
        {
            return tankCollision;
        }
    }
}
