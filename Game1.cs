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
        int points = 10000;
        TimeSpan timeSum;
        float seconds;
        bool paused = false;


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
            testTank = new PlayerTank(new Vector2(200, 200), tankSprite, turretSprite);
            level = new Level(1, testTank);
            if(level.getDone())
            {
                level = new Level(2, testTank);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                paused = true;
            }
            if(paused) {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
                else if((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))){
                    paused = false;
                }
            }
            if(!paused) {
                
                // TODO: Add your update logic here
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
            if (paused)
            {
                spriteBatch.DrawString(arial24, "Press Enter to Exit, Space to continue", new Vector2(400, 300), Color.Black);
            }
            spriteBatch.End();

            //Points -> Move to right space
            timeSum = gameTime.TotalGameTime;
            seconds = timeSum.Seconds;
            points -= (int)(seconds * 20) + (projectileCount * 5) + (100 - testTank.GetCurrentHP() * 10);
            
            
            base.Draw(gameTime);
        }
    }
}