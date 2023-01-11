using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Tank testTank;
        Map map;
        Texture2D obstacle;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Mouse.SetCursor(MouseCursor.Crosshair);

            graphics.PreferredBackBufferWidth = 1216;   //32*38
            graphics.PreferredBackBufferHeight = 800;   //32*25
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            arial24 = Content.Load<SpriteFont>(@"fonts/arial24");
            testTank = new Tank(new Vector2(200, 200), Content.Load<Texture2D>(@"graphics/tank1"));
            map = new Map(Content.Load<Texture2D>(@"graphics/grass32"), Content.Load<Texture2D>(@"graphics/brick32"));   //TODO: Move to Level for easy implement of different skins
            obstacle = Content.Load<Texture2D>(@"graphics/castle96");
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
            map.Draw(spriteBatch);
            spriteBatch.Draw(obstacle, new Rectangle(416, 416, 96, 96), Color.White);
            testTank.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}