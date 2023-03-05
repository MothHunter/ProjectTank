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
        protected float speed = 0; // speed at which the tank currently moves
        protected float maxSpeed = 4f; // speed at which the tank can move
        protected float acceleration = 0.15f; // acceleration of the tank when it moves forward
        protected float turnRate = 0.05f; // turnrate at which the tank turns
        protected float rotation = 0f; // initial rotation of the tank
        protected int currentHP; // current Hitpoints of the tank
        protected int maxHP; // maximum hitpoints of the tank
        protected float hightTank = 30; // hight of the tank
        protected float widthTank = 30; // width of the tank

        protected float fireCooldown = 0.8f; // firecooldown the time it takes to reload the tank so it can not shoot for this amount of time
        protected float fireCooldownCountdown = 0f; // firecooldown remaining since the last shot; the tank can fire if firecooldowncountdown is 0
        protected Texture2D sprite; // graphics
        protected Texture2D destroyedSprite;
        protected Vector2 drawOffset; // offset
        protected Vector2 position; // position of the tank

        public bool isAlive = true; // bool if the tank is alive (hp over 0)

        protected Turret turret; // the turret on top of the tank

        protected CollisionBox tankCollision; // collisionbox if the tank

        /// <summary>
        /// standard constructor
        /// </summary>
        /// <param name="position">starting position of the tank</param>
        /// <param name="tankSprite">graphic of the tank</param>
        /// <param name="turretSprite">graphic of the turret</param>
        public Tank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite)
        {
            this.position = position;
            this.sprite = tankSprite;
            drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.tankCollision = new CollisionBox(position, rotation, widthTank, hightTank);
            this.turret = new Turret(position, turretSprite, rotation);
            destroyedSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.TankDestroyed);
        }

        /// <summary>
        /// Draws the tanks and calls the draw methode of the turret
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1,1), SpriteEffects.None, 1.0f);
            turret.Draw(spriteBatch);
        }

        /// <summary>
        /// updates the tank position and the turret position and then checks for collisions
        /// if a collision occurres the position will be reset
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (!isAlive) // checks if the tank is alive if not return
            {
                sprite = destroyedSprite;
                return;
            }
            if (fireCooldownCountdown > 0) //if the firecooldownCountdown is bigger than 0 it is reduced
            {
                fireCooldownCountdown -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
            }
            getInput(gameTime); // checks which mousebuttons an keys are pressed
            if (speed != 0) // checks for collisions if the tank is moving
            {                
                Vector2 positionOld = position; // saves the position before updateing it
                position += Utility.radToV2(rotation) * speed; // adds rotation multiplied by speed to the position to move
                tankCollision.Update(rotation, position); // updates the position of the collision box
                for (int i = 0; i < Level.obstacles.Count; i++) // goes through the list ov every obstacle and checks for collisions
                {
                    if (tankCollision.Collides(Level.obstacles[i].GetCollisionBox())) // if the tank collides the position is reset
                    {
                        position = positionOld;
                        tankCollision.Update(rotation, positionOld);
                        speed = 0;
                    }
                }
                foreach (Tank otherTank in Level.aitanks) // goes through the list of every tank and checks for collisions
                {
                    if (otherTank != this) // sorts out this tank
                    {
                        if (tankCollision.Collides(otherTank.GetCollisionBox())) // if the tank collides the position is reset
                        {
                            position = positionOld;
                            tankCollision.Update(rotation, positionOld);
                            speed = 0;
                        }
                    }
                }
            }

            // turret Update needs to be called twice (in GetInput and here)
            // to prevent turret jiggling around the tank; refactor?
            turret.Update(position, turret.GetRotation());
        }

        /// <summary>
        /// increases the speed till it hits the maximum speed
        /// </summary>
        public void SpeedUp()
        {
            speed = Math.Min(speed + acceleration, maxSpeed);
        }

        /// <summary>
        /// decreases the speed till it hits halve the maximum speed in the negative
        /// </summary>
        public void Reverse()
        {
            speed = Math.Max(speed - acceleration, -maxSpeed / 2);
        }

        /// <summary>
        /// no forward or reverse input => tank slowly comes to a halt
        /// </summary>
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

        // rotates the tank to the left
        public void RotateLeft()
        {
            rotation = (rotation - turnRate) % (2 * (float)Math.PI);
        }

        // rotates the tank to the right
        public void RotationRight()
        {
            rotation = (rotation + turnRate) % (2 * (float)Math.PI);
        }

        /// <summary>
        /// Rotates the turret in the direction of the target coordinates
        /// </summary>
        /// <param name="target">target coordinates</param>
        public void RotateTurret(Vector2 target)
        {
            float turretRotation = turret.GetRotation();
            turret.Update(position, Utility.V2ToRad(target - position));
        }

        /// <summary>
        /// Fires a standars shot originating fromdirectly in front of the tank.
        /// Checks for fire cooldown first
        /// </summary>
        protected virtual void ShootStandard()
        {
            if (fireCooldownCountdown > 0)
            {
                return;
            }
            fireCooldownCountdown = fireCooldown;

            // get graphic for shot
            Texture2D projectileSprite = AssetController.GetInstance()
                .getTexture2D(graphicsAssets.StandardProjectile);

            // calculate offset for where to create the shot relative to tank position
            // so that the shot is not created on top of the tank
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;

            // add shot to list of projectiles managed by the Level class
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, 
                turret.GetRotation()));

            Game1.shotSound.Play();
        }

        /// <summary>
        /// Abstract method for reacting to a hit.
        /// Implement in subclasses
        /// </summary>
        /// <param name="damage"></param>
        public abstract void getHit(int damage);

        /// <summary>
        /// Abstract method for tank controll
        /// Implement in subclasses
        /// </summary>
        /// <param name="gameTime"></param>
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
