using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms.VisualStyles;

namespace ProjectTank
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public SpriteFont arial24;
        Tank testTank;
        Level testBackground;
        Level levelBorder;
        Map background;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            

            graphics.PreferredBackBufferWidth = 1216;
            graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            arial24 = Content.Load<SpriteFont>(@"fonts/arial24");
            testTank = new Tank(new Vector2(200, 200), Content.Load<Texture2D>(@"graphics/tank1"));
            background = new Map(Content.Load<Texture2D>(@"graphics/grass"));
            //levelBorder = new Level(new Vector2(0, 0), Content.Load<Texture2D>(@"graphics/brick"));
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
            //testBackground.Draw(spriteBatch);
            //spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/grass"), new Rectangle(50, 50, 1100, 700), Color.White);
            background.Draw(spriteBatch);
            for (int i = 0; i < 2000; i = i + 50)
            {
                if(i >= 1200)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/sBrick"), new Rectangle(0, i - 1200, 50, 50), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/sBrick"), new Rectangle(1150, i -1200, 50, 50), Color.White);
                }
                spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/sBrick"), new Rectangle(i, 750, 50, 50), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/sBrick"), new Rectangle(i, 0, 50, 50), Color.White);

            }
            spriteBatch.DrawString(arial24, "Level Border", new Vector2(500, 10), Color.Black);
            testTank.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}