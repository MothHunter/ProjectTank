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
        public static List<Obstacle> obstacles = new List<Obstacle>();
        public static List<Projectile> projectiles = new List<Projectile>();
        public static List<AiTank> aitanks = new List<AiTank>();
        public static SpecialShot specialShot; // only one can exist at a time

        Vector2 startPosition;
        public static Tank tank;
        bool done;
        public static int dead = 0; // counts dead ai tanks

        public Level(int number, Tank tank)
        {
            Texture2D castle = AssetController.GetInstance().getTexture2D(graphicsAssets.Castle);
            Texture2D dBrick = AssetController.GetInstance().getTexture2D(graphicsAssets.dBrick);
            Texture2D dBrickHalf = AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickHalf);
            Texture2D dBrickDestroyed = AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickDestroyed);
            Texture2D Brick64 = AssetController.GetInstance().getTexture2D(graphicsAssets.Brick64);

            if (number == 1)
            {
                dead = 0;
                obstacles.Add(new Obstacle(new Vector2(544, 320), castle, null, null, false, 100, 96, 96, new Vector2(592, 368)));
                obstacles.Add(new Obstacle(new Vector2(800, 150), dBrick, dBrickHalf, dBrickDestroyed, true, 20, 64, 64, new Vector2(832, 182)));
                obstacles.Add(new Obstacle(new Vector2(400, 550), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(432, 582)));

                this.map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.Grass/*Border*/), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));   //TODO: Move to Level for easy implement of different skins
                this.startPosition = new Vector2(0,0);

                Texture2D tankSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis);
                Texture2D turretSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Turret);

                aitanks.Add(new AiTank1(new Vector2(1000, 700)));
                
                //Destructible //this.obstacle = new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.dTest32), true, 1, 32, 32, new Vector2(560,336));
            }
            if(number == 2)
            {
                dead = 0;
                //obstacles.Add(new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrick), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickDestroyed),null , true, 20, 96, 96, new Vector2(592, 368)));
                obstacles.Add(new Obstacle(new Vector2(544, 384), Brick64, null, null, false, 100, 64, 64, new Vector2(576, 416)));
                obstacles.Add(new Obstacle(new Vector2(608, 352), Brick64, null, null, false, 100, 64, 64, new Vector2(640, 384)));

                obstacles.Add(new Obstacle(new Vector2(480, 416), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(512, 448)));
                obstacles.Add(new Obstacle(new Vector2(416, 448), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(448, 480)));
                obstacles.Add(new Obstacle(new Vector2(352, 480), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(684, 512)));
                obstacles.Add(new Obstacle(new Vector2(288, 512), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(320, 544)));
                obstacles.Add(new Obstacle(new Vector2(224, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(256, 576)));

                obstacles.Add(new Obstacle(new Vector2(672, 320), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(704, 352)));
                obstacles.Add(new Obstacle(new Vector2(736, 288), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(768, 320)));
                obstacles.Add(new Obstacle(new Vector2(800, 256), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(832, 288)));
                obstacles.Add(new Obstacle(new Vector2(864, 224), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(896, 256)));
                obstacles.Add(new Obstacle(new Vector2(928, 192), dBrick, dBrickHalf, dBrickDestroyed, true, 100, 64, 64, new Vector2(960, 224)));

                this.map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.GrassBorder), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));
                this.startPosition = new Vector2(0, 0);
                
                aitanks.Add(new AiTank1(new Vector2(1000, 700)));
                aitanks.Add(new AiTank2(new Vector2(800, 700)));
                aitanks.Add(new AiTank2(new Vector2(900, 700)));

            }
            if(number == 3)
            {
                dead = 0;
                for (int i = 32; i <608; i= i+64)
                {
                    obstacles.Add(new Obstacle(new Vector2(160, i), Brick64, null, null, false, 100, 64, 64, new Vector2(192, i + 32)));
                    if (i >= 160 && i <= 416)
                    {
                        obstacles.Add(new Obstacle(new Vector2(352, i), Brick64, null, null, false, 100, 64, 64, new Vector2(384, i + 32)));
                    }
                    if (i >= 160)
                    {
                        obstacles.Add(new Obstacle(new Vector2(992, i), Brick64, null, null, false, 100, 64, 64, new Vector2(1024, i + 32)));
                    }
                }
                for (int i = 416; i < 992; i= i+ 64)
                {
                    
                    obstacles.Add(new Obstacle(new Vector2(i, 160), Brick64, null, null, false, 100, 64, 64, new Vector2(i + 32, 192)));
                    if (i < 608 || i >= 736)
                    {
                        obstacles.Add(new Obstacle(new Vector2(i, 544), Brick64, null, null, false, 100, 64, 64, new Vector2(i + 32, 576)));
                    }
                    if (i >= 544 && i < 864)
                    {
                        obstacles.Add(new Obstacle(new Vector2(i, 416), Brick64, null, null, false, 100, 64, 64, new Vector2(i + 32, 448)));
                    }
                }

                this.map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.GrassBorder), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));
                this.startPosition = new Vector2(0, 0);

                aitanks.Add(new AiTank1(new Vector2(1000, 700)));
                aitanks.Add(new AiTank2(new Vector2(800, 700)));
                aitanks.Add(new AiTank3(new Vector2(1000, 200)));
                aitanks.Add(new AiTank4(new Vector2(200, 700)));
            }
            Level.tank = tank;
            this.done = false;
        }

        public bool getDone(){ return done; }
        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
            for(int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Draw(spriteBatch);
            }
            
            tank.Draw(spriteBatch);

            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            foreach(AiTank aitank in aitanks)
            {
                aitank.Draw(spriteBatch);
            }
            if (specialShot != null)
            {
                specialShot.Draw(spriteBatch);
            }

        }

        public void Update(GameTime gameTime)
        {
            tank.Update(gameTime);
            foreach(AiTank aiTank in aitanks)
            {
                aiTank.Update(gameTime);
            }
            List<Projectile> removeProjectiles = new List<Projectile>();
            foreach (Projectile projectile in projectiles)
            {
                projectile.update();
                foreach (Obstacle obstacle in obstacles)
                {
                    if (obstacle.GetCollisionBox().Contains(projectile.getPosition()))
                    {
                        if (obstacle.IsDestructible() && !obstacle.IsDestroyed())
                        {
                            obstacle.OnHit(projectile.GetDamage());
                            removeProjectiles.Add(projectile);
                        }
                        else if (!obstacle.IsDestructible())
                        {
                            projectile.Reflect(obstacle.GetCollisionBox());
                        }
                    }
                }
                foreach (AiTank aiTank in aitanks)
                {
                    if (aiTank.GetCollisionBox().Contains(projectile.getPosition()))
                    {
                        aiTank.getHit(projectile.GetDamage());
                        removeProjectiles.Add(projectile);
                    }
                }
                if (tank.GetCollisionBox().Contains(projectile.getPosition()))
                {
                    tank.getHit(projectile.GetDamage());
                    removeProjectiles.Add(projectile);
                }
                if (projectile.GetRemainingBounces() < 0)
                {
                    removeProjectiles.Add(projectile);
                }
            }
            if (specialShot != null)
            {
                specialShot.Update(gameTime);
            }

            // remove expired projectiles
            foreach(Projectile p in removeProjectiles)
            {
                projectiles.Remove(p);
            }

        }
    }
}