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
    internal class Level
    {
        Map map;
        Obstacle obstacle;
        Vector2 startPosition;
        Tank tank;

        public Level(Map map, Obstacle obstacle, Vector2 startPosition, Tank tank)
        {
            this.map = map;
            this.obstacle = obstacle;
            this.startPosition = startPosition;
            this.tank = tank;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            obstacle.Draw(spriteBatch);
            tank.Draw(spriteBatch);

        }

        public void Update(GameTime gameTime)
        {

        }

        /*int Width;
        int Height;
        float rotation = 0f;
        Vector2 position;
        Vector2 drawOffset;
        Texture2D sprite;


        public Level(Vector2 position, Texture2D sprite)
        {
            drawOffset = new Vector2(0, 0);
            this.position = position;
            this.sprite = sprite;

        }
        public void Draw(SpriteBatch spriteBatch)
        {

                spriteBatch.Draw(sprite, position, null, Color.White, rotation, drawOffset, new Vector2(0, 0), SpriteEffects.None, 1.0f);
               

        }
        */
    }
}