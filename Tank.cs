using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    internal abstract class Tank
    {
        protected float speed = 0;
        protected float maxSpeed = 4f;
        protected float acceleration = 0.15f;
        protected float turnRate = 0.05f;
        protected float rotation = 0f;
        protected int currentHP;
        protected int maxHP;
        protected float hightTank = 30;
        protected float widthTank = 30;

        protected float fireCooldown = 0.8f;
        protected float fireCooldownCountdown = 0f;
        protected Texture2D sprite;
        protected Vector2 drawOffset;
        protected Vector2 position;

        public bool isAlive = true;

        protected Turret turret;

        protected CollisionBox tankCollision;

        public Tank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite)
        {
            this.position = position;
            this.sprite = tankSprite;
            drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.tankCollision = new CollisionBox(position, rotation, widthTank, hightTank);
            this.turret = new Turret(position, turretSprite, rotation);

        }




        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, isAlive?Color.White:Color.Black, rotation, drawOffset, new Vector2(1,1), SpriteEffects.None, 1.0f);
            turret.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (!isAlive)
            {
                return;
            }
            if (fireCooldownCountdown > 0)
            {
                fireCooldownCountdown -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
            getInput(gameTime);
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
                foreach (Tank otherTank in Level.aitanks)
                {
                    if (otherTank != this)
                    {
                        if (tankCollision.Collides(otherTank.GetCollisionBox()))
                        {
                            position = positionOld;
                            tankCollision.Update(rotation, positionOld);
                            speed = 0;
                        }
                    }
                }
            }

            // turret Update needs to be called twice (in GetInput and here); refactor?
            turret.Update(position, turret.GetRotation());
        }

        public void SpeedUp()
        {
            speed = Math.Min(speed + acceleration, maxSpeed);
        }

        public void Reverse()
        {
            speed = Math.Max(speed - acceleration, -maxSpeed / 2);
        }

        // no forward or reverse input => tank slowly comes to a halt
        public void Roll()
        {
            if (speed < 0)
            {
                speed = Math.Min(speed + acceleration * 0.35f, 0);
            }
            else if (speed > 0)
            {
                speed = Math.Max(speed - acceleration * 0.35f, 0);
            }
        }

        public void RotateLeft()
        {
            rotation = (rotation - turnRate) % (2 * (float)Math.PI);
        }


        public void RotationRight()
        {
            rotation = (rotation + turnRate) % (2 * (float)Math.PI);
        }

        public void RotateTurret(Vector2 target)
        {
            float turretRotation = turret.GetRotation();
            turret.Update(position, Utility.V2ToRad(target - position));
        }

        protected virtual void ShootStandard()
        {
            if (fireCooldownCountdown > 0)
            {
                return;
            }
            fireCooldownCountdown = fireCooldown;
            Texture2D projectileSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.StandardProjectile);
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation()));
            Game1.projectileCount++;
        }

        public abstract void getHit(int damage);


        public abstract void getInput(GameTime gameTime);

        public CollisionBox GetCollisionBox()
        {
            return tankCollision;
        }

        public Vector2 GetPosition() { return position; }

        public int GetMaxHP() { return maxHP; }
        public int GetCurrentHP() { return currentHP; }

    }
}
