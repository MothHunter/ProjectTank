using System;
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
        Texture2D sprite;       // the graphic of the turret
        Texture2D destroyedSprite;
        Vector2 drawOffset;     // drawoffset for drawing
        Vector2 position;       // center of the turret
        float rotation = 0f;    // direction turret is pointing in
        bool isAlive = true;    // whether turret is still active


        public Turret(Vector2 position, Texture2D sprite, float rotation)
        {
            this.position = position;
            this.sprite = sprite;
            this.rotation = rotation;
            this.drawOffset = new Vector2(sprite.Width / 2, sprite.Height / 2);
            destroyedSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.TankDestroyed);
        }

        /// <summary>
        /// Disables the turret; called by the corresponding tank on its destruction
        /// </summary>
        public void Die()
        {
            isAlive = false;
            // create explosion effect on destruction
            GraphicsEffect graphicsEffect = new GraphicsEffect(AssetController.GetInstance()
                .getTexture2D(graphicsAssets.Explosion),
                                    position, 0.8f, 1.03f, new Vector2(16, 16));
            Level.graphicsEffects.Add(graphicsEffect);
            sprite = destroyedSprite;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // dead turrets are drawn in black
            spriteBatch.Draw(sprite, position, null, Color.White, 
                rotation, drawOffset, new Vector2(1, 1), SpriteEffects.None, 1.0f);
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
