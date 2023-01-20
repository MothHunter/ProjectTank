using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class AiTank4 : AiTank
    {
        public AiTank4(Vector2 position) :
            base(position, AssetController.GetInstance().getTexture2D(graphicsAssets.Tank4Chassis),
                AssetController.GetInstance().getTexture2D(graphicsAssets.Tank4Turret))
        {
            maxSpeed = 3f;
            maxHP = 100;
            currentHP = maxHP;
            fireCooldown = 1.6f;
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
