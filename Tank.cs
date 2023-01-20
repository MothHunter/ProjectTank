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
        protected float maxSpeed = 6f;
        protected float acceleration = 0.15f;
        protected float turnRate = 0.05f;
        protected float rotation = 0f;
        protected int currentHP;
        protected int maxHP;
        protected float hightTank = 20;
        protected float widthTank = 20;

        protected float fireCooldown = 0.8f;
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
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(1,1), SpriteEffects.None, 1.0f);
            turret.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            getInput();

        }

        protected virtual void ShootStandard()
        {
            Texture2D projectileSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.StandardProjectile);
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation(), 10f));
            Game1.projectileCount++;
        }

        public void getHit(Projectile projectile)
        {
            currentHP -= projectile.GetDamage();
            if (currentHP <= 0)
            {
                if (isAlive)
                {
                    Level.dead += 1;
                }
                isAlive = false;
            }
        }

        public abstract void getInput();

        public CollisionBox GetCollisionBox()
        {
            return tankCollision;
        }
    }
}
