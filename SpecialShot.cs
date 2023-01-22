using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace ProjectTank
{
    internal class SpecialShot
    {
        float rotation;         // direction the shot is pointing
        float speed = 10f;      // movement speed of the shot in pixels per frame
        int damage = 70;        // damage caused
        Vector2 position;       // current position
        float expRadius = 32;   // radius in which the explosion affects objects that can be dammaged
        float fuseTime;         // timer (nr. of frames) used to explode the shot at the target coordinates

        Texture2D sprite;       // graphic
        Vector2 drawOffset;     // offset to draw the graphic relative to the position

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rotation">direction</param>
        /// <param name="speed">speed (pixels/frame)</param>
        /// <param name="damage"></param>
        /// <param name="position">Coordinates where the hot is created</param>
        /// <param name="target">Coordinates the shot is aimed at</param>
        public SpecialShot(float rotation, float speed, int damage, Vector2 position, Vector2 target)
        {
            this.rotation = rotation;
            this.speed = speed;
            this.damage = damage;
            this.position = position;
            // fuse time in frames = distance / (speed in pixel/frame)
            this.fuseTime = ((target - position).Length() / speed);
            this.sprite = AssetController.GetInstance().getTexture2D(graphicsAssets.SpecialShot);
            this.drawOffset = new Vector2(24, 8);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1, 1), SpriteEffects.None, 1.0f);
        }

        public void Update(GameTime gameTime)
        {
            position += Utility.radToV2(rotation) * speed;
            fuseTime--;

            // if the fuse time is up, the shot has reached its target coordinates
            // before hitting an object
            if (fuseTime <= 0 )
            {
                Explode();
            }

            // if the shot hits an obstacle it explodes
            foreach (Obstacle obstacle in Level.obstacles)
            {
                if (obstacle.GetCollisionBox().Contains(position))
                {
                    if (!obstacle.IsDestroyed())
                    {
                        Explode();
                    }
                }
            }

            // if the shot hits a tank it explodes
            foreach (AiTank aiTank in Level.aitanks)
            {
                if (aiTank.GetCollisionBox().Contains(position))
                {
                    Explode();
                }
            }
            if (Level.tank.GetCollisionBox().Contains(position))
            {
                Explode();
            }
        }


        public Vector2 getPosition()
        {
            return position;
        }
        public int GetDamage()
        {
            return damage;
        }

        /// <summary>
        /// Explodes the shot, causing damage to all destructible obstacles and tanks within radius.
        /// </summary>
        public void Explode()
        {
            // affected objects are identified by creating 8 points around the center of the explosion
            // at a distance of explosionRadius, and checking each of them against the collision
            // boxes of all tanks and obstacles.
            // Given the minimum size of 32x32 pixels for all relevant objects, at least one of the
            // detection points should be inside the collision box of any object within radius
            List<Vector2> points = new List<Vector2>();
            List<Obstacle> hitObstacles = new List<Obstacle>();
            List<Tank> hitTanks = new List<Tank>();

            points.Add(position); // the center of the explosion

            // add 8 points around position for collision detection
            for (int i = 0; i < 8; i++)
            {
                // create normalized direction vectors
                Vector2 direction = Utility.radToV2((float)Math.PI * 2 * (i / 8f));
                // create points around "position" in "direction" at distance of "explosionRadius"
                points.Add(position + (direction * expRadius));
            }

            // check all points for collisions
            foreach (Vector2 point in points)
            {
                foreach (Obstacle obstacle in Level.obstacles)
                {
                    if (obstacle.GetCollisionBox().Contains(point))
                    {
                        if (!obstacle.IsDestroyed())
                        {
                            hitObstacles.Add(obstacle);
                        }
                    }
                }
                foreach (AiTank aiTank in Level.aitanks)
                {
                    if (aiTank.GetCollisionBox().Contains(point))
                    {
                        hitTanks.Add(aiTank);
                    }
                }
                if (Level.tank.GetCollisionBox().Contains(point))
                {
                    hitTanks.Add(Level.tank);
                }
            }

            // do dammage to each affected tank and obstacle (but only once each)
            foreach (Tank tank in hitTanks.Distinct())
            {
                tank.getHit(damage);
            }
            foreach (Obstacle obstacle in hitObstacles.Distinct())
            {
                if (obstacle.IsDestructible())
                {
                    obstacle.OnHit(damage);
                }
            }

            Level.specialShot = null; // remove the shot
            // add an explosion effect at the position
            GraphicsEffect graphicsEffect = new GraphicsEffect(AssetController.GetInstance().getTexture2D(graphicsAssets.Explosion),
                                                position, 0.7f, 1.02f, new Vector2(32, 32));
            Level.graphicsEffects.Add(graphicsEffect);
        }
    }
}
