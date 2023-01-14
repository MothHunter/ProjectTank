using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;
using System.Windows.Forms.VisualStyles;

namespace ProjectTank
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public SpriteFont arial24;
        public static ContentManager contentManager;

        Tank testTank;
        // CollisionBox testBox;
        Map map;
        Texture2D obstacle;


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
            testTank = new Tank(new Vector2(200, 200), AssetController.GetInstance().getTexture2D(graphicsAssets.Tank1Chassis));
            map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.GrassBorder), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));   //TODO: Move to Level for easy implement of different skins
            obstacle = AssetController.GetInstance().getTexture2D(graphicsAssets.Castle);
            base.Initialize();

            // testBox = new CollisionBox(new Vector2(0, 0), 0f, 100f, 100f);
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

            // if(testBox.Collides(testTank.GetCollisionBox())) Debug.WriteLine("Collision!!");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            map.Draw(spriteBatch);
            spriteBatch.Draw(obstacle, new Rectangle(416, 416, 96, 96), Color.White);
            testTank.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}