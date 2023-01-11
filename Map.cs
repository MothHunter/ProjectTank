using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    internal class Map
    {
        Texture2D sprite;
        Texture2D border;
        Vector2 position;
        Vector2 dimension;

        
        public Map(Texture2D sprite,Texture2D border)
        {
            this.sprite = sprite;
            this.border = border;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 32; i < 1184; i += 32)
            {
                for (int j = 32; j < 768; j += 32)
                {
                    spriteBatch.Draw(sprite, new Rectangle(i, j, 32, 32), Color.White);
                }
            }
            for (int i = 0; i < 2016; i = i + 32)
            {
                if (i >= 1200)
                {
                    spriteBatch.Draw(border, new Rectangle(0, i - 1216, 32, 32), Color.White);
                    spriteBatch.Draw(border, new Rectangle(1184, i - 1216, 32, 32), Color.White);
                }

                spriteBatch.Draw(border, new Rectangle(i, 768, 32, 32), Color.White);
                spriteBatch.Draw(border, new Rectangle(i, 0, 32, 32), Color.White);

            }
        }
        
        public void Update(GameTime gameTime)
        {

        }
    }
}