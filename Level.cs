using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
    internal class Level
    {
        public static List<Obstacle> obstacles = new List<Obstacle>();                      // Consists of all Obstacles in this level, if destroyable or not
        public static List<Projectile> projectiles = new List<Projectile>();                // All active (flying) Projectils
        public static List<AiTank> aitanks = new List<AiTank>();                            // All AiTanks
        public static List<GraphicsEffect> graphicsEffects = new List<GraphicsEffect>();    // All active Graphicseffects
        public static SpecialShot specialShot;                                              // only one can exist at a time 
        public static Tank tank;                                                            // Player Tank

        bool done;                                                                          // Shows if the level was successfully done
        public static int dead = 0;                                                         // counts dead ai tanks

        public Level(int number, Tank tank)
        {
            // Setting Textures used in more than one level          
            Texture2D dBrick = AssetController.GetInstance().getTexture2D(graphicsAssets.dBrick);
            Texture2D dBrickHalf = AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickHalf);
            Texture2D dBrickDestroyed = AssetController.GetInstance().getTexture2D(graphicsAssets.dBrickDestroyed);
            Texture2D Brick64 = AssetController.GetInstance().getTexture2D(graphicsAssets.Brick64);

            Map.addToList();    // Adding the CollisionBoxes from the Border

            if (number == 1)    // Level 1
            {
                Texture2D castle = AssetController.GetInstance().getTexture2D(graphicsAssets.Castle);       // Setting the texture only used in this level

                dead = 0;       // At the start of the Level, no Tanks are dead

                // Adding Obstacles to the list
                obstacles.Add(new Obstacle(new Vector2(544, 320), castle, null, null, false, 100, 96, 96, new Vector2(592, 368)));
                obstacles.Add(new Obstacle(new Vector2(800, 150), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(832, 182)));
                obstacles.Add(new Obstacle(new Vector2(400, 550), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(432, 582)));

                // Adding the textures for the tank
                Texture2D tankSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis);
                Texture2D turretSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Turret);

                // Adding AiTank
                aitanks.Add(new AiTank1(new Vector2(1000, 700)));
            }

            if (number == 2)     // Level 2
            {
                dead = 0;       // At the start of the Level, no Tanks are dead

                // Adding non destroyable obstacles
                obstacles.Add(new Obstacle(new Vector2(544, 384), Brick64, null, null, false, 100, 64, 64, new Vector2(576, 416)));
                obstacles.Add(new Obstacle(new Vector2(608, 352), Brick64, null, null, false, 100, 64, 64, new Vector2(640, 384)));

                // Adding destroyable obstacles
                obstacles.Add(new Obstacle(new Vector2(480, 416), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(512, 448)));
                obstacles.Add(new Obstacle(new Vector2(416, 448), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(448, 480)));
                obstacles.Add(new Obstacle(new Vector2(352, 480), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(384, 512)));
                obstacles.Add(new Obstacle(new Vector2(288, 512), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(320, 544)));
                obstacles.Add(new Obstacle(new Vector2(224, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(256, 576)));
                obstacles.Add(new Obstacle(new Vector2(672, 320), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(704, 352)));
                obstacles.Add(new Obstacle(new Vector2(736, 288), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(768, 320)));
                obstacles.Add(new Obstacle(new Vector2(800, 256), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(832, 288)));
                obstacles.Add(new Obstacle(new Vector2(864, 224), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(896, 256)));
                obstacles.Add(new Obstacle(new Vector2(928, 192), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(960, 224)));

                // Adding AiTanks
                aitanks.Add(new AiTank1(new Vector2(1000, 650)));
                aitanks.Add(new AiTank2(new Vector2(900, 700)));
                aitanks.Add(new AiTank2(new Vector2(1100, 600)));

            }
            if (number == 3)     // Level 3
            {
                dead = 0;       // At the start of the Level, no Tanks are dead

                // First undestroyable walls get initialized
                for (int i = 32; i < 608; i = i + 64)
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

                // Continuing 
                for (int i = 416; i < 992; i = i + 64)
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

                // Adding destroyable walls
                obstacles.Add(new Obstacle(new Vector2(224, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(256, 576)));
                obstacles.Add(new Obstacle(new Vector2(288, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(320, 576)));
                obstacles.Add(new Obstacle(new Vector2(352, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(384, 576)));
                obstacles.Add(new Obstacle(new Vector2(608, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(640, 576)));
                obstacles.Add(new Obstacle(new Vector2(672, 544), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(704, 576)));
                obstacles.Add(new Obstacle(new Vector2(416, 416), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(448, 448)));
                obstacles.Add(new Obstacle(new Vector2(480, 416), dBrick, dBrickHalf, dBrickDestroyed, true, 60, 64, 64, new Vector2(512, 448)));
                obstacles.Add(new Obstacle(new Vector2(800, 352), dBrick, dBrickHalf, dBrickDestroyed, true, 64, 64, 64, new Vector2(832, 384)));

                // Adding one last non destroyable wall
                obstacles.Add(new Obstacle(new Vector2(800, 288), Brick64, null, null, false, 100, 64, 64, new Vector2(832, 320)));

                // Adding AiTanks
                aitanks.Add(new AiTank1(new Vector2(1120, 64)));
                aitanks.Add(new AiTank2(new Vector2(288, 320)));
                aitanks.Add(new AiTank2(new Vector2(928, 512)));
                aitanks.Add(new AiTank3(new Vector2(576, 320)));
                aitanks.Add(new AiTank3(new Vector2(96, 736)));
                aitanks.Add(new AiTank4(new Vector2(1120, 704)));

            }

            Level.tank = tank;      // Player Tank
            this.done = false;      // At the start the level is not done
        }

        public bool getDone() { return done; }       // Getter for bool done;

        // This function is responsible for drawing the whole level
        public void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch);      // Draw Standard Map


            // Draw all Obstacles in the List
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Draw(spriteBatch);
            }

            tank.Draw(spriteBatch);     // Draw Player Tank

            // Draw all Projectiles in the List
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            // Draw all AiTanks in the List
            foreach (AiTank aitank in aitanks)
            {
                aitank.Draw(spriteBatch);
            }

            // Draw the Special Shot if one exists
            if (specialShot != null)
            {
                specialShot.Draw(spriteBatch);
            }

            // draw graphics effects
            foreach (GraphicsEffect graphicsEffect in graphicsEffects)
            {
                graphicsEffect.Draw(spriteBatch);
            }
        }

        // Clearing Lists
        public static void Clear()
        {
            aitanks.Clear();              // clear lists
            obstacles.Clear();            // clear lists
            projectiles.Clear();          // clear lists
            graphicsEffects.Clear();      // clear lists
            specialShot = null;           // specialShot is empty
            dead = 0;                     // deadcounter reset
        }

        // Updating everything changeable
        public void Update(GameTime gameTime)
        {
            // Updating Tanks
            tank.Update(gameTime);
            foreach (AiTank aiTank in aitanks)
            {
                aiTank.Update(gameTime);
            }

            List<Projectile> removeProjectiles = new List<Projectile>();    // All projectile, hitting a target or expireing are added here

            // Updating Projectiles
            foreach (Projectile projectile in projectiles)
            {
                projectile.update();

                foreach (Obstacle obstacle in obstacles)        // Checking Obstacles
                {
                    if (obstacle.GetCollisionBox().Contains(projectile.getPosition()))      // Hit on a obstacle
                    {
                        if (obstacle.IsDestructible() && !obstacle.IsDestroyed())       // Hit on a destroyable obstacle
                        {
                            obstacle.OnHit(projectile.GetDamage());     // Apply Damage
                            removeProjectiles.Add(projectile);          // Add to remove List
                        }
                        else if (!obstacle.IsDestructible())        // Hit on a non destroyable object 
                        {
                            projectile.Reflect(obstacle.GetCollisionBox());     // Reflect Bullet
                        }
                    }
                }

                foreach (AiTank aiTank in aitanks)      // Checking AiTanks
                {
                    if (aiTank.GetCollisionBox().Contains(projectile.getPosition()))    // Hit
                    {
                        aiTank.getHit(projectile.GetDamage());      // Apply Damage
                        removeProjectiles.Add(projectile);          // Add to remove List
                    }
                }

                if (tank.GetCollisionBox().Contains(projectile.getPosition()))      // Checking Player Tank
                {
                    tank.getHit(projectile.GetDamage());        // Apply Damage
                    removeProjectiles.Add(projectile);          // Add to remove List
                }

                if (projectile.GetRemainingBounces() < 0)       // if already bounced
                {
                    removeProjectiles.Add(projectile);          // Add to remove List
                }
            }
            // Updating Special Shot if exists
            if (specialShot != null)
            {
                specialShot.Update(gameTime);
            }

            // remove expired projectiles
            foreach (Projectile p in removeProjectiles)
            {
                GraphicsEffect graphicsEffect = new GraphicsEffect(AssetController.GetInstance().getTexture2D(graphicsAssets.Explosion),
                                   p.getPosition(), 0.4f, 1.02f, new Vector2(8, 8));
                Level.graphicsEffects.Add(graphicsEffect);
                projectiles.Remove(p);
            }

            // update graphics effects
            for (int i = graphicsEffects.Count() - 1; i >= 0; i--)
            {
                if (graphicsEffects[i].GetRemainingLifetime() <= 0)
                {
                    graphicsEffects.RemoveAt(i);
                }
                else
                {
                    graphicsEffects[i].Update(gameTime);
                }
            }
        }
    }
}