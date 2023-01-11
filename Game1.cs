using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Level testBackground;
        Level levelBorder;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            

            graphics.PreferredBackBufferWidth = 1216;   //32*38
            graphics.PreferredBackBufferHeight = 800;   //32*25
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            arial24 = Content.Load<SpriteFont>(@"fonts/arial24");
            testTank = new Tank(new Vector2(200, 200), Content.Load<Texture2D>(@"graphics/tank1"));
            testBackground = new Level(new Vector2(0, 0), Content.Load<Texture2D>(@"graphics/grass"));
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
            for (int i = 32; i < 1184; i += 32)
            {
                for(int j = 32;j<768; j+=32)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/grass32"), new Rectangle(i, j, 32, 32), Color.White);
                }
            }
            for (int i = 0; i < 2016; i = i + 32)
            {
                if(i >= 1200)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/brick32"), new Rectangle(0, i - 1216, 32, 32), Color.White);
                    spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/brick32"), new Rectangle(1184, i - 1216, 32, 32), Color.White);
                }

                spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/brick32"), new Rectangle(i, 768, 32, 32), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>(@"graphics/brick32"), new Rectangle(i, 0, 32, 32), Color.White);

            }
            spriteBatch.DrawString(arial24, "Level Border", new Vector2(500, 5), Color.Black);
            testTank.Draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}