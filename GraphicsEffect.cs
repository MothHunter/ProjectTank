using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal class GraphicsEffect
    {
        Texture2D sprite;
        Vector2 position;
        float lifeTime;             // seconds effect exists
        float remainingLifeTime;    // countdown for lieftime
        float sizeChange;           // how much the effect changes size every frame; 1 to remain same size
        Vector2 size;               // size the effect starts at
        Vector2 scale;              // scale for drawing; calculated as effect size / sprite size
        Vector2 offset;             // for drawing; how much to offset visuals from position

        public GraphicsEffect(Texture2D sprite, Vector2 position, float lifeTime, float sizeChange, Vector2 size)
        {
            this.sprite = sprite;
            this.position = position;
            this.lifeTime = lifeTime;
            this.remainingLifeTime = lifeTime;
            this.sizeChange = sizeChange;
            this.size = size;
            scale = new Vector2(size.X / sprite.Width, size.Y / sprite.Height);
            offset = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            remainingLifeTime -= gameTime.ElapsedGameTime.Milliseconds / 1000f;
            scale *= sizeChange;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0f, offset, scale, SpriteEffects.None, 1f);
        }

        public float GetRemainingLifetime() { return remainingLifeTime; }
    }
}
