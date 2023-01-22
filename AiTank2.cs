using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class AiTank2 : AiTank
    {
        /// <summary>
        /// creates a aitank model 2
        /// it uses the base constructor but gives it the graphics for aitank model 2
        /// </summary>
        /// <param name="position">the position where the tank will be created</param>
        public AiTank2(Vector2 position) : 
            base(position, AssetController.GetInstance().getTexture2D(graphicsAssets.Tank3Chassis),
                AssetController.GetInstance().getTexture2D(graphicsAssets.Tank3Turret))
        {
            maxSpeed = 1.5f; // sets the maxspeed as 1.5
            maxHP = 100; // sets the max hp as 100
            currentHP = maxHP; // sets the current hp as 100
            fireCooldown = 1.6f; // sets the firecooldown to 1.6
        }

    }
}
