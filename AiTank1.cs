using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class AiTank1 : AiTank
    {
        /// <summary>
        /// creates a aitank model 1
        /// it uses the base constructor but gives it the graphics for aitank model 1
        /// </summary>
        /// <param name="position">the position where the tank will be created</param>
        public AiTank1(Vector2 position) : 
            base(position, AssetController.GetInstance().getTexture2D(graphicsAssets.Tank2Chassis),
                AssetController.GetInstance().getTexture2D(graphicsAssets.Tank2Turret))
        {
            maxHP = 150; // sets the max hp to 150
            currentHP = maxHP; // sets the current hp to 150
            fireCooldown = 1.6f; // sets the firecooldown to 1.6
            maxSpeed = 0f; // as aitank1 cant move its max speed is 0
        }
    }
}
