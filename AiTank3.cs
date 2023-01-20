using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class AiTank3 : AiTank
    {
        public AiTank3(Vector2 position) :
       base(position, AssetController.GetInstance().getTexture2D(graphicsAssets.Tank2Chassis),
           AssetController.GetInstance().getTexture2D(graphicsAssets.Tank2Turret))
        {
            maxHP = 150;
            currentHP = maxHP;
            fireCooldown = 1.6f;
            maxSpeed = 0f;
        }

        protected override void ShootStandard()
        {
            if (fireCooldownCountdown > 0)
            {
                return;
            }
            fireCooldownCountdown = fireCooldown;
            Texture2D projectileSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.StandardProjectile);
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation(), 10f, 0, 35));
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation() + 0.2f, 10f, 0, 35));
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation() - 0.2f, 10f, 0, 35));
            Game1.projectileCount++;
        }
    }
}
