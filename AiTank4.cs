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
        /// <summary>
        /// creates a aitank model 4
        /// it uses the base constructor but gives it the graphics for aitank model 4
        /// </summary>
        /// <param name="position">the position where the tank will be created</param>
        public AiTank4(Vector2 position) :
            base(position, AssetController.GetInstance().getTexture2D(graphicsAssets.Tank4Chassis),
                AssetController.GetInstance().getTexture2D(graphicsAssets.Tank4Turret))
        {
            maxSpeed = 2f; // sets the max speed to 2
            maxHP = 100; // sets the max hp as 100
            currentHP = maxHP; // sets the current hp to 100
            fireCooldown = 1.6f; // sets the firecooldown to 1.6
        }

        protected override void ShootStandard() // overrides the standard shot of aitank
        {
            if (fireCooldownCountdown > 0) // checks if the firecooldown is ready or not if not return
            {
                return;
            }
            fireCooldownCountdown = fireCooldown; // sets the firecooldown
            Texture2D projectileSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.StandardProjectile); // loads the graphic for the projectile
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16; // calculates the offset
            // create 3 projectile the middle one aimed at the player while the 2 on the side with a rotation
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation(), 10f, 0, 35));
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation() + 0.2f, 10f, 0, 35));
            Level.projectiles.Add(new Projectile(position + offset, projectileSprite, turret.GetRotation() - 0.2f, 10f, 0, 35));
            Game1.shotSound.Play();
        }
    }
}
