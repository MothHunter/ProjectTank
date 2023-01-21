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
        public SpriteFont arial24;
        public static ContentManager contentManager;

        PlayerTank testTank;
        Level level;
        public static int projectileCount = 0;
        int points = 0;
        TimeSpan timeSum;
        float seconds;
        bool paused = false;
        bool finished = false;
        bool end = false;
        int levelcount = 1;
        
        CollisionBox Button1 = new CollisionBox(new Vector2(608, 432), 0f, 448, 96);
        CollisionBox Button2 = new CollisionBox(new Vector2(608, 580), 0f, 448, 96);
       
        Texture2D buttonGraphic1;
        Texture2D buttonGraphic2;



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
            arial24 = Content.Load<SpriteFont>(@"fonts/arial24");
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

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            InputController.GetInstance().Update();
            buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
            buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                paused = true;
            }
            if (paused)
            {
                if (Button1.Contains(InputController.GetInstance().GetCursorPosition()))
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick())
                    {
                        paused = false;
                    }

                }
                else if (Button2.Contains(InputController.GetInstance().GetCursorPosition()))
                {
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick())
                    {
                        Exit();
                    }
                }
                else
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                }

            }
            if (finished)
            {
                if (Button1.Contains(InputController.GetInstance().GetCursorPosition()))
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick())
                    {
                        finished = false;
                    }

                }
                else if (Button2.Contains(InputController.GetInstance().GetCursorPosition()))
                {
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick())
                    {
                        Exit();
                    }
                }
                else
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                }

            }
        
            if (end)
            {
                if (Button1.Contains(InputController.GetInstance().GetCursorPosition()))
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick())
                    {
                        end = false;
                        levelcount = 1;
                    }

                }
                else if (Button2.Contains(InputController.GetInstance().GetCursorPosition()))
                {
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick())
                    {
                        Exit();
                    }
                }
                else
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                }
            }

            if (!paused && !finished && !end)
            {

                    // TODO: Add your update logic here


                    if (Level.aitanks.Count == Level.dead || !Level.tank.isAlive)
                    {
                        finished = true;
                        if (levelcount == 1) { points = 0; }
                        timeSum = gameTime.TotalGameTime;
                        seconds = timeSum.Seconds;
                        // points -= (int)(seconds * 10) - (projectileCount * 25) - ((100 - testTank.GetCurrentHP()) * 50) + ( Level.aitanks.Count * 100);
                        int enemyCurrHP = 0;
                        int enemyMaxHP = 0;
                        foreach (Tank aitank in Level.aitanks)
                        {
                            enemyCurrHP += aitank.GetCurrentHP();
                            enemyMaxHP += aitank.GetMaxHP();
                        }
                        points += Math.Max(Math.Max((Level.tank.GetCurrentHP() * 10), 0) - (projectileCount * 2) - (int)(seconds) + (enemyMaxHP - enemyCurrHP * 20), 0);
                        // TODO: point speichern wenn lvl abgeschlossen

                    Level.aitanks.Clear();
                    Level.obstacles.Clear();
                    Level.projectiles.Clear();
                    Level.dead = 0;
                    if (levelcount == 3 && Level.tank.isAlive)
                    {
                        levelcount = 1;
                    }
                    else if (Level.tank.isAlive)
                    {
                        levelcount += 1;
                    }
                    else
                    {
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            if (paused || finished || end)
            {
                spriteBatch.Draw(AssetController.GetInstance().getTexture2D(graphicsAssets.menuBackground), new Rectangle(256, 128, 704, 544), Color.White);
                spriteBatch.Draw(buttonGraphic1, new Rectangle(384, 384, 448, 96), Color.White);
                spriteBatch.Draw(buttonGraphic2, new Rectangle(384, 512, 448, 96), Color.White);
                spriteBatch.DrawString(arial24, "Points: " + points, new Vector2(512, 256), Color.White);
                if (paused)
                {
                    spriteBatch.DrawString(arial24, "Continue Game", new Vector2(500, 416), Color.Red);
                    spriteBatch.DrawString(arial24, "Exit", new Vector2(576, 544), Color.Red);
                }
                if (finished)
                {
                    spriteBatch.DrawString(arial24, "Next Level", new Vector2(544, 416), Color.Red);
                    spriteBatch.DrawString(arial24, "Exit", new Vector2(576, 544), Color.Red);
                }

            }
                spriteBatch.End();
                

            //Points -> Move to right space
            
            
            
            base.Draw(gameTime);
        }
    }
}