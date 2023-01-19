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
        public AiTank2(Vector2 position, Texture2D tankSprite, Texture2D turretSprite) : base(position, tankSprite, turretSprite)
        {
            maxSpeed = 3f;
            maxHP = 100;
            currentHP = maxHP;
            fireCooldown = 1.6f;
        }
    }
}
