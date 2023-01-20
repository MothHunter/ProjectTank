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

        Vector2 startPosition;
        Tank tank;
        bool done;

        public Level(int number, Tank tank)
        {
            if(number == 1)
            {
                obstacles.Add(new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.Castle), null, null, false, 100, 96, 96, new Vector2(592, 368)));
                obstacles.Add(new Obstacle(new Vector2(800, 150), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickHalf), null, AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickDestroyed), true, 20, 64, 64, new Vector2(832, 182)));
                obstacles.Add(new Obstacle(new Vector2(400, 550), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrick), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickHalf), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickDestroyed), true, 60, 64, 64, new Vector2(432, 582)));

                this.map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.Grass/*Border*/), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));   //TODO: Move to Level for easy implement of different skins
                this.startPosition = new Vector2(0,0);

                Texture2D tankSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis);
                Texture2D turretSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Turret);

                aitanks.Add(new AiTank1(new Vector2(1000, 700), tankSprite, turretSprite));
                //Destructible //this.obstacle = new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.dTest32), true, 1, 32, 32, new Vector2(560,336));
            }
            if(number == 2)
            {
                obstacles.Add(new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrick), AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickDestroyed),null , true, 20, 96, 96, new Vector2(592, 368)));
                this.map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.Dirt), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));   //TODO: Move to Level for easy implement of different skins
                this.startPosition = new Vector2(0, 0);
                aitanks.Add(new AiTank1(new Vector2(1000, 700)));
                aitanks.Add(new AiTank2(new Vector2(800, 700)));

            }
            this.tank = tank;
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
                        aiTank.getHit(projectile);
                        removeProjectiles.Add(projectile);
                    }
                }
                if (tank.GetCollisionBox().Contains(projectile.getPosition()))
                {
                    tank.getHit(projectile);
                    removeProjectiles.Add(projectile);
                }
                if (projectile.GetRemainingBounces() < 0)
                {
                    removeProjectiles.Add(projectile);
                }
            }

            // remove expired projectiles
            foreach(Projectile p in removeProjectiles)
            {
                projectiles.Remove(p);
            }

        }
    }
}