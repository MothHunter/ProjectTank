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

        // Textures for Player Tank
        Texture2D turretSprite;
        Texture2D tankSprite;

        public static int points = 0;   // current Points
        PlayerTank Tank;                // Playable Tank
        Level level;                    // Level Instance
        int levelcount = 1;             // changing the level if won or lost
        float seconds = 0;              // used Time for calculation points
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
            Menu.arial24 = Content.Load<SpriteFont>(@"fonts/arial24");                                          // initializing the font
            tankSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis);               // initializng graphics for Tank
            turretSprite = AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Turret);
            Tank = new PlayerTank(new Vector2(100, 100), tankSprite, turretSprite);                             // creating Tank
            level = new Level(levelcount, Tank);                                                                // creating Level                   
           
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
                seconds += gameTime.ElapsedGameTime.Milliseconds / 1000f;
                if (Level.aitanks.Count == Level.dead || !Level.tank.isAlive)           // if all enemy tanks are dead or the player tank is dead
                {
                    level.endLevelCountdown -= gameTime.ElapsedGameTime.Milliseconds / 1000f;

                    if (level.endLevelCountdown <= 0)
                    {
                        if (levelcount != 3)                                                // if you beat level 1 or 2
                        {
                            Menu.finished = true;                                           // set finished for pausing and displaying menu
                        }

                        if (levelcount == 1) { points = 0; }                                // if you start playing - points are set to 0


                        foreach (Tank aitank in Level.aitanks)                              // Stats used for calculating points
                        {
                            enemyCurrHP += aitank.GetCurrentHP();
                            enemyMaxHP += aitank.GetMaxHP();
                        }

                        points += Math.Max(Math.Max((Level.tank.GetCurrentHP() * 10), 0) - (int)seconds + ((enemyMaxHP - enemyCurrHP) * 20), 0); // points formula
                        seconds = 0;
                        enemyMaxHP = 0;
                        enemyCurrHP = 0;
                        Level.Clear();      // Clearing the Lists for next Level

                        if (levelcount == 3 && Level.tank.isAlive) // if last level is beaten
                        {

                            Menu.end = true;        //show end menu
                            Menu.won = true;        //show winscreen
                            levelcount = 1;         //set level to 1 in case the player wants to play again
                        }

                        else if (Level.tank.isAlive)    //if other two levels are beaten
                        {
                            levelcount += 1;            //increase level
                            Menu.beaten = true;         //show level win menu
                        }

                        else                            //if player dies
                        {
                            Menu.finished = true;       //show losescreen
                            Menu.beaten = false;
                            levelcount = 1;             //set level to 1
                        }

                        Tank = new PlayerTank(new Vector2(100, 100), tankSprite, turretSprite);

                        if (levelcount <= 3)        // new level based on counter
                        {
                            level = new Level(levelcount, Tank);
                        }
                    }
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