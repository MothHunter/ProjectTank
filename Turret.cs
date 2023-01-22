﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    internal class Turret
    {
        Texture2D sprite;
        Vector2 drawOffset;
        Vector2 position;
        float rotation = 0f;
        bool isAlive = true;


        public Turret(Vector2 position, Texture2D sprite, float rotation)
        {
            this.position = position;
            this.sprite = sprite;
            this.rotation = rotation;
            this.drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        public void Die()
        {
            isAlive = false;
            GraphicsEffect graphicsEffect = new GraphicsEffect(AssetController.GetInstance().getTexture2D(graphicsAssets.Explosion),
                                    position, 0.8f, 1.03f, new Vector2(16, 16));
            Level.graphicsEffects.Add(graphicsEffect);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, isAlive ? Color.White : Color.Black, rotation, drawOffset, new Vector2(1, 1), SpriteEffects.None, 1.0f);
        }

        public void Update (Vector2 position, float rotate)
        {
            if (!isAlive) return;
            this.position = position;
            this.rotation = rotate;
        }

        public float GetRotation()
        {
            return rotation;
        }

    }


}
