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

        public Level(int number, Vector2 startPosition, Tank tank)
        {
            if(number == 1)
            {
                this.obstacle = new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.Castle), false, 100, 96, 96, new Vector2(592, 368));            
                this.map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.GrassBorder), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));   //TODO: Move to Level for easy implement of different skins
            this.startPosition = startPosition;

            }
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