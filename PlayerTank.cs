﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    internal class PlayerTank : Tank
    {
        public PlayerTank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite) : base(position, tankSprite, turretSprite)
        {
            maxHP = 100;
            currentHP = maxHP;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            // draw health bar
            Texture2D pixel = AssetController.GetInstance().getTexture2D(graphicsAssets.Pixel);
            Rectangle frame = new Rectangle((int)(position.X - (sprite.Width / 2) - 16), (int)(position.Y - sprite.Width / 2),
                                            8, sprite.Height);
            Rectangle life = new Rectangle((int)(position.X - (sprite.Width / 2) - 15), (int)((position.Y - sprite.Width / 2) + 1),
                                            6, sprite.Height - 2);
            Rectangle spentLife = new Rectangle((int)(position.X - (sprite.Width / 2) - 15), (int)((position.Y - sprite.Width / 2) + 1),
                                            6, (sprite.Height - 2) * (maxHP - currentHP) / maxHP);
            spriteBatch.Draw(pixel, frame, Color.Black);
            spriteBatch.Draw(pixel, life, Color.Green);
            spriteBatch.Draw(pixel, spentLife, Color.Red);
        }


        public override void getInput()
        {

            // handle inputs from keyboard
            InputController input = InputController.GetInstance();
            if (input.GetKeyDown(Keys.W))
            {
                //Speed up
                speed = Math.Min(speed + acceleration, maxSpeed);
            }
            else if (input.GetKeyDown(Keys.S))
            {
                //slow down
                speed = Math.Max(speed - acceleration, -maxSpeed / 2);
            }
            else if (speed < 0)
            {
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
                for (int i = 0; i < Level.obstacles.Count; i++)
                {
                    if (tankCollision.Collides(Level.obstacles[i].GetCollisionBox()))
                    {
                        position = positionOld;
                        tankCollision.Update(rotation, positionOld);
                        speed = 0;
                    }
                }
            }

            // handle inputs from mouse
            float turretRotation = turret.GetRotation();

            turret.Update(position, Utility.V2ToRad(input.GetCursorPosition() - position));

            if (input.GetLeftClick())
            {
                ShootStandard();
            }
        }
    }
}
