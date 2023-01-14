using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
    public enum graphicsAssets { Tank1Chassis, Tank1Turret, StandardProjectile}
    internal class AssetController
    {
        private Dictionary<graphicsAssets, Texture2D> graphics;
        private static AssetController instance;
        private AssetController()
        {
            graphics = new Dictionary<graphicsAssets, Texture2D>();
            graphics.Add(graphicsAssets.Tank1Chassis, Game1.contentManager.Load<Texture2D>(@"graphics/tank1"));
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
