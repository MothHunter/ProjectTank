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

        Tank testTank;
        // CollisionBox testBox;
        Map map;
        Obstacle obstacle;
        Obstacle obstacle2;
        Level level;
        public static List<CollisionBox> indestructible = new List<CollisionBox>();
        public static List<CollisionBox> destructible = new List<CollisionBox>();
        public static List<Projectile> projectiles = new List<Projectile>();


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
            testTank = new Tank(new Vector2(200, 200), tankSprite, turretSprite);
            map = new Map(AssetController.GetInstance().getTexture2D(graphicsAssets.GrassBorder), AssetController.GetInstance().getTexture2D(graphicsAssets.Brick));   //TODO: Move to Level for easy implement of different skins
            obstacle = new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.Castle), false, 100, 96, 96, new Vector2(592,368));
            //Destructible obstacle
            //obstacle2 = new Obstacle(new Vector2(544, 320), AssetController.GetInstance().getTexture2D(graphicsAssets.dTest32), true, 1, 32, 32, new Vector2(592,368));
            level = new Level(map, obstacle, new Vector2(0, 0), testTank);


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



            foreach(Projectile projectile in projectiles)
            {
                projectile.update();
                foreach(CollisionBox collisionBox in indestructible)
                {
                    if (collisionBox.Contains(projectile.getPosition()))
                    {
                        projectile.Reflect(collisionBox);
                    }
                }
                foreach (CollisionBox collisionBox in destructible)
                {
                    collisionBox.Contains(projectile.getPosition());
                }

            }

            // if(testBox.Collides(testTank.GetCollisionBox())) Debug.WriteLine("Collision!!");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            level.Draw(spriteBatch);
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}