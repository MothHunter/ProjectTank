using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
    internal static class Map
    {
        // Textures for the Background and the Border; If uncommenting "Border" a grid will be shown on the Background
        static Texture2D sprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Grass/*Border*/);
        static Texture2D border = AssetController.GetInstance().getTexture2D(graphicsAssets.Brick);
        
        //Border CollisionBoxes
        static CollisionBox top = new CollisionBox(new Vector2(608, 16), 0f, 1216, 32);
        static CollisionBox bottom = new CollisionBox(new Vector2(608, 784), 0f, 1216, 32);
        static CollisionBox left = new CollisionBox(new Vector2(16, 400), 0f, 32, 800);
        static CollisionBox right = new CollisionBox(new Vector2(1200, 400), 0f, 32, 800);

        // Adding CollisionBoxes as Obstacles in the List for collsion checking 
        public static void addToList()
        {
            Obstacle temp =new Obstacle(top);
            temp =new Obstacle(bottom);
            temp =new Obstacle(left);
            temp =new Obstacle(right);
        }

        // Drawing the Map
        public static void Draw(SpriteBatch spriteBatch)
        {
            // Drawing Background
            for (int i = 32; i < 1184; i += 32)
            {
                for (int j = 32; j < 768; j += 32)
                {
                    spriteBatch.Draw(sprite, new Rectangle(i, j, 32, 32), Color.White);
                }
            }

            // Drawing Border
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
        
    }
}