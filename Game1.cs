using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace ProjectTank
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public SpriteFont arial24;
        Tank testTank;
        public static ContentManager contentManager;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = Content;
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            arial24 = Content.Load<SpriteFont>(@"fonts/arial24");
            testTank = new Tank(new Vector2(200, 200), AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            InputController.GetInstance().Update();
            // TODO: Add your update logic here

            testTank.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(arial24, "Hello, Tanks!", new Vector2(300, 300), Color.Red);
            testTank.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}