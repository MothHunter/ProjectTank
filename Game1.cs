using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;

namespace ProjectTank
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static ContentManager contentManager;

        PlayerTank testTank;
        Level level;
        public static int projectileCount = 0;
        public static int points = 0;
        TimeSpan timeSum;
        float seconds;
        int levelcount = 1;
        
      
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
            IsMouseVisible = true;
            Mouse.SetCursor(MouseCursor.Crosshair);

            graphics.PreferredBackBufferWidth = 1216;   //32*38
            graphics.PreferredBackBufferHeight = 800;   //32*25
        }
        

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Menu.arial24 = Content.Load<SpriteFont>(@"fonts/arial24");
            Texture2D tankSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis);
            Texture2D turretSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Turret);
            testTank = new PlayerTank(new Vector2(100, 100), tankSprite, turretSprite);
            if(levelcount <= 3) { 
                level = new Level(levelcount, testTank);
            }
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            InputController.GetInstance().Update();
            Menu.Update(gameTime);
            if (Menu.exit) {Exit();}

            if (!Menu.paused && !Menu.finished && !Menu.end)
            {
                if (Level.aitanks.Count == Level.dead || !Level.tank.isAlive)
                {
                    if (levelcount != 3)
                    {
                        Menu.finished = true;
                    }
                    if (levelcount == 1) { points = 0; }
                    timeSum = gameTime.TotalGameTime;
                    seconds = timeSum.Seconds;
                    int enemyCurrHP = 0;
                    int enemyMaxHP = 0;
                    foreach (Tank aitank in Level.aitanks)
                    {
                        enemyCurrHP += aitank.GetCurrentHP();
                        enemyMaxHP += aitank.GetMaxHP();
                    }
                    points += Math.Max(Math.Max((Level.tank.GetCurrentHP() * 10), 0) - (projectileCount * 2) - (int)(seconds) + (enemyMaxHP - enemyCurrHP * 20), 0);

                    Level.aitanks.Clear();
                    Level.obstacles.Clear();
                    Level.projectiles.Clear();
                    Level.graphicsEffects.Clear();
                    Level.specialShot = null;
                    Level.dead = 0;
                    if (levelcount == 3 && Level.tank.isAlive)
                    {
                        
                        Menu.end = true;
                        Menu.won = true;
                        levelcount = 1;
                    }
                    else if (Level.tank.isAlive)
                    {
                        levelcount += 1;
                        Menu.beaten = true;
                    }
                    else
                    {
                        Menu.finished = true;
                        Menu.beaten = false;
                        levelcount = 1;
                    }
                    Initialize();              
                    
                }
                level.Update(gameTime);
                base.Update(gameTime); 
            }
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            level.Draw(spriteBatch);
            Menu.Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}