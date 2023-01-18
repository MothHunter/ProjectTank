using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static ProjectTank.Game1;

namespace ProjectTank
{
    internal class Map
    {
        Texture2D sprite;
        Texture2D border;

        CollisionBox top = new CollisionBox(new Vector2(608, 16), 0f, 1216, 32);
        CollisionBox bottom = new CollisionBox(new Vector2(608, 784), 0f, 1216, 32);
        CollisionBox left = new CollisionBox(new Vector2(16, 400), 0f, 32, 800);
        CollisionBox right = new CollisionBox(new Vector2(1200, 400), 0f, 32, 800);


        public Map(Texture2D sprite,Texture2D border)
        {
            this.sprite = sprite;
            this.border = border;
            addToList();
        }

        public void addToList()
        {
            Obstacle temp =new Obstacle(top);
            temp =new Obstacle(bottom);
            temp =new Obstacle(left);
            temp =new Obstacle(right);
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