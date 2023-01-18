using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
    public enum graphicsAssets { Tank1Chassis, Tank1Turret, StandardProjectile, Grass, GrassBorder, Brick, Castle, Dirt, dBrick, Brick64, dBrickHalf,dBrickDestroyed }
    internal class AssetController
    {
        private Dictionary<graphicsAssets, Texture2D> graphics;
        private static AssetController instance;
        private AssetController()
        {
            graphics = new Dictionary<graphicsAssets, Texture2D>();
            graphics.Add(graphicsAssets.Tank1Chassis, Game1.contentManager.Load<Texture2D>(@"graphics/tank1"));


            graphics.Add(graphicsAssets.Grass, Game1.contentManager.Load<Texture2D>(@"graphics/grass32"));
            graphics.Add(graphicsAssets.GrassBorder, Game1.contentManager.Load<Texture2D>(@"graphics/grass32wB"));
            graphics.Add(graphicsAssets.Brick, Game1.contentManager.Load<Texture2D>(@"graphics/brick32"));
            graphics.Add(graphicsAssets.Castle, Game1.contentManager.Load<Texture2D>(@"graphics/Castle96"));
           
            
            graphics.Add(graphicsAssets.dBrick, Game1.contentManager.Load<Texture2D>(@"graphics/dBrick"));
            graphics.Add(graphicsAssets.Brick64, Game1.contentManager.Load<Texture2D>(@"graphics/brick64"));
            graphics.Add(graphicsAssets.dBrickHalf, Game1.contentManager.Load<Texture2D>(@"graphics/dbrick64_half"));
            graphics.Add(graphicsAssets.dBrickDestroyed, Game1.contentManager.Load<Texture2D>(@"graphics/dbrick64destroyed"));
            graphics.Add(graphicsAssets.Dirt, Game1.contentManager.Load<Texture2D>(@"graphics/dirt32"));
        }
        public static AssetController GetInstance()
        {
            if (instance == null)
            {
                instance = new AssetController();
            }
            return instance;
            
        }

        public Texture2D getTexture2D(graphicsAssets enum1)
        {
            return graphics[enum1];
        }

    }
    
}
