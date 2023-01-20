using System;
using System.Collections.Generic;
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
        float rotation;
        float speed = 10f;
        int damage = 50;
        Vector2 position;
        float expRadius = 32;
        float fuseTime;

        Texture2D sprite;
        Vector2 drawOffset;

        public SpecialShot(float rotation, float speed, int damage, Vector2 position, Vector2 target)
        {
            this.rotation = rotation;
            this.speed = speed;
            this.damage = damage;
            this.position = position;
            // fuse time = distance / (speed in pixel/frame)
            this.fuseTime = ((target - position).Length() / (speed / 60f));
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
            fuseTime -= gameTime.ElapsedGameTime.Milliseconds / 1000f;

            if (fuseTime <= 0 )
            {
                Explode();
            }

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
        public void Explode()
        {
            List<Vector2> points = new List<Vector2>();
            List<Obstacle> hitObstacles = new List<Obstacle>();
            List<Tank> hitTanks = new List<Tank>();

            points.Add(position);
            for (int i = 0; i < 8; i++)
            {
                points.Add(Utility.radToV2((float)Math.PI * 2) * i / 8f);
            }

            foreach (Vector2 point in points)
            {
                foreach (Obstacle obstacle in Level.obstacles)
                {
                    if (obstacle.GetCollisionBox().Contains(position))
                    {
                        if (!obstacle.IsDestroyed())
                        {
                            hitObstacles.Add(obstacle);
                        }
                    }
                }
                foreach (AiTank aiTank in Level.aitanks)
                {
                    if (aiTank.GetCollisionBox().Contains(position))
                    {
                        hitTanks.Add(aiTank);
                    }
                }
                if (Level.tank.GetCollisionBox().Contains(position))
                {
                    hitTanks.Add(Level.tank);
                }
            }
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

            Level.specialShot = null;
        }
    }
}
