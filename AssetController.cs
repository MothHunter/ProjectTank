using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
    public enum graphicsAssets { Tank1Chassis, Tank1Turret, Tank2Chassis, Tank2Turret, 
                                Tank3Chassis, Tank3Turret, Tank4Chassis, Tank4Turret, 
                                StandardProjectile, SpecialShot, Explosion,
                                Grass, GrassBorder, Brick, Castle, Dirt, dBrick,
                                Brick64, dBrickHalf, dBrickDestroyed, Pixel, menuBackground, DarkGrey, LightGrey, IconSpecialShot}
    internal class AssetController
    {
        private Dictionary<graphicsAssets, Texture2D> graphics;
        private static AssetController instance;
        private AssetController()
        {
            graphics = new Dictionary<graphicsAssets, Texture2D>();

            // tank graphics
            graphics.Add(graphicsAssets.Tank1Chassis, Game1.contentManager.Load<Texture2D>(@"graphics/tank1"));
            graphics.Add(graphicsAssets.Tank1Turret, Game1.contentManager.Load<Texture2D>(@"graphics/turret1"));
            graphics.Add(graphicsAssets.Tank2Chassis, Game1.contentManager.Load<Texture2D>(@"graphics/tank2"));
            graphics.Add(graphicsAssets.Tank2Turret, Game1.contentManager.Load<Texture2D>(@"graphics/turret2"));
            graphics.Add(graphicsAssets.Tank3Chassis, Game1.contentManager.Load<Texture2D>(@"graphics/tank3"));
            graphics.Add(graphicsAssets.Tank3Turret, Game1.contentManager.Load<Texture2D>(@"graphics/turret3"));
            graphics.Add(graphicsAssets.Tank4Chassis, Game1.contentManager.Load<Texture2D>(@"graphics/tank4"));
            graphics.Add(graphicsAssets.Tank4Turret, Game1.contentManager.Load<Texture2D>(@"graphics/turret4"));
            graphics.Add(graphicsAssets.StandardProjectile, Game1.contentManager.Load<Texture2D>(@"graphics/projectile1"));
            graphics.Add(graphicsAssets.SpecialShot, Game1.contentManager.Load<Texture2D>(@"graphics/specialShot"));
            graphics.Add(graphicsAssets.Explosion, Game1.contentManager.Load<Texture2D>(@"graphics/explosion"));

            // map graphics
            graphics.Add(graphicsAssets.Grass, Game1.contentManager.Load<Texture2D>(@"graphics/grass32"));
            graphics.Add(graphicsAssets.GrassBorder, Game1.contentManager.Load<Texture2D>(@"graphics/grass32wB"));
            graphics.Add(graphicsAssets.Brick, Game1.contentManager.Load<Texture2D>(@"graphics/brick32"));
            graphics.Add(graphicsAssets.Castle, Game1.contentManager.Load<Texture2D>(@"graphics/Castle96"));    
            graphics.Add(graphicsAssets.dBrick, Game1.contentManager.Load<Texture2D>(@"graphics/dBrick64"));
            graphics.Add(graphicsAssets.Brick64, Game1.contentManager.Load<Texture2D>(@"graphics/brick64"));
            graphics.Add(graphicsAssets.dBrickHalf, Game1.contentManager.Load<Texture2D>(@"graphics/dbrick64_half"));
            graphics.Add(graphicsAssets.dBrickDestroyed, Game1.contentManager.Load<Texture2D>(@"graphics/dbrick64destroyed"));
            graphics.Add(graphicsAssets.Dirt, Game1.contentManager.Load<Texture2D>(@"graphics/dirt32"));
            
            // UI graphics
            graphics.Add(graphicsAssets.Pixel, Game1.contentManager.Load<Texture2D>(@"graphics/whitePixel"));
            graphics.Add(graphicsAssets.menuBackground, Game1.contentManager.Load<Texture2D>(@"graphics/menuBackground"));
            graphics.Add(graphicsAssets.DarkGrey, Game1.contentManager.Load<Texture2D>(@"graphics/dark_grey"));
            graphics.Add(graphicsAssets.LightGrey, Game1.contentManager.Load<Texture2D>(@"graphics/light"));

            graphics.Add(graphicsAssets.IconSpecialShot, Game1.contentManager.Load<Texture2D>(@"graphics/iconSpecialShot"));

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
