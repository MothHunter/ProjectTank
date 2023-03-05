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
        /// <summary>
        /// creates a aitank model 3
        /// it uses the base constructor but gives it the graphics for aitank model 3
        /// </summary>
        /// <param name="position">the position where the tank will be created</param>
        public AiTank3(Vector2 position) :
       base(position, AssetController.GetInstance().getTexture2D(graphicsAssets.Tank2Chassis),
           AssetController.GetInstance().getTexture2D(graphicsAssets.Tank2Turret))
        {
            maxHP = 150; // sets the max hp to 150
            currentHP = maxHP; // sets the current hp to 150
            fireCooldown = 1.6f; // sets the firecooldown to 1.6
            maxSpeed = 0f; // sets the max speed to 0 as to not beeing able to move
        }

        protected override void ShootStandard() // overrides the standard shot of aitank
        {
            if (fireCooldownCountdown > 0) // checks if the firecooldown is ready or not if not return
            {
                return;
            }
            fireCooldownCountdown = fireCooldown; // sets the firecooldown
            // loads the graphic for the projectile
            Texture2D projectileSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.StandardProjectile);
            // calculates the offset
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;
            // create 3 projectile the middle one aimed at the player while the 2 on the side with a rotation
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation(), 10f, 0, 35));
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation() + 0.2f, 10f, 0, 35));
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation() - 0.2f, 10f, 0, 35));
            Game1.shotSound.Play();
        }
    }
}
