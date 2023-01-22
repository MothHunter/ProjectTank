using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProjectTank
{
    public class Game1 : Game
    {
        // Framework
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static ContentManager contentManager;

        public static int points = 0;   // current Points
        PlayerTank Tank;                // Playable Tank
        Level level;                    // Level Instance
        TimeSpan timeSum;               // TimeSum for measuring the time
        float seconds;                  // Timsum converted in float used for calculating the Points
        int levelcount = 1;             // changing the level if won or lost
        int enemyCurrHP = 0;            // for calculating points
        int enemyMaxHP = 0;             // for calculating points


        public Game1()
        {
            // Framework
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;

            IsMouseVisible = true;                      // Mousecourser shall be visiable
            Mouse.SetCursor(MouseCursor.Crosshair);     // Setting the Mousecourser to a Crosshair

            graphics.PreferredBackBufferWidth = 1216;   // 32*38 for Resolution
            graphics.PreferredBackBufferHeight = 800;   // 32*25 for Resolution
        }
        

        protected override void Initialize()
        {
            Menu.arial24 = Content.Load<SpriteFont>(@"fonts/arial24");                                              // initializing the font
            Texture2D tankSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis);         // initializng graphics for Tank
            Texture2D turretSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Turret);
            Tank = new PlayerTank(new Vector2(100, 100), tankSprite, turretSprite);                                 // creating Tank
            if(levelcount <= 3) { 
                level = new Level(levelcount, Tank);                                                                // creating Level based on counter                     
            }
           
            base.Initialize();
        }

        // Creating a SpriteBatch for Drawing
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        // Function for Updating everything
        protected override void Update(GameTime gameTime)
        {
            InputController.GetInstance().Update();
            Menu.Update(gameTime);                                                      // calling Menu Update
            
            if (Menu.exit) {Exit();}                                                    // Exit Button

            if (!Menu.paused && !Menu.finished && !Menu.end)                            // if game is not paused
            {
                if (Level.aitanks.Count == Level.dead || !Level.tank.isAlive)           // if all enemy tanks are dead or the player tank is dead
                {
                    if (levelcount != 3)                                                // if you beat level 1 or 2
                    {
                        Menu.finished = true;                                           // set finished for pausing and displaying menu
                    }

                    if (levelcount == 1) { points = 0; }                                // if you start playing - points are set to 0
                    
                    timeSum = gameTime.TotalGameTime;                                   
                    seconds = timeSum.Seconds;
                    
                    
                    foreach (Tank aitank in Level.aitanks)                              
                    {
                        enemyCurrHP += aitank.GetCurrentHP();
                        enemyMaxHP += aitank.GetMaxHP();
                    }
                    
                    points += Math.Max(Math.Max((Level.tank.GetCurrentHP() * 10), 0) - (int)(seconds) + (enemyMaxHP - enemyCurrHP * 20), 0); // points formula

                    Level.Clear();
                    
                    if (levelcount == 3 && Level.tank.isAlive) // if last level is won
                    {
                        
                        Menu.end = true;
                        Menu.beaten = true;
                        levelcount = 1;
                    }
                    
                    else if (Level.tank.isAlive)
                    {
                        levelcount += 1;
                        Menu.won = true;
                    }
                    
                    else
                    {
                        Menu.finished = true;
                        Menu.won = false;
                        levelcount = 1;
                    }
                    
                    Initialize();              
                    
                }
                
                level.Update(gameTime);     // update Level
                base.Update(gameTime);      // update Game
            }
            
        }

        // Draw Game
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);     
       
            spriteBatch.Begin();                            
            level.Draw(spriteBatch);                        // Draw Level
            Menu.Draw(spriteBatch);                         // Draw Menu
            spriteBatch.End();                              
            
            base.Draw(gameTime);                            
        }
    }
}