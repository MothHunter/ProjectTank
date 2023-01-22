using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    internal static class Menu
    {
        static Texture2D buttonGraphic1;
        static Texture2D buttonGraphic2;
        static CollisionBox Button1 = new CollisionBox(new Vector2(608, 432), 0f, 448, 96);
        static CollisionBox Button2 = new CollisionBox(new Vector2(608, 580), 0f, 448, 96);
        public static SpriteFont arial24;
        public static bool paused = false;
        public static bool finished = false;
        public static bool beaten = false;
        public static bool end = true;
        public static bool won = false;
        public static bool exit = false;



        public static void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) // enter Menu if Escape is pressed
            {
                paused = true;
            }
            if (paused || finished || end)
            {
                if (Button1.Contains(InputController.GetInstance().GetCursorPosition())) //hover over top button
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick()) //click on top button
                    {
                        if (paused) { paused = false; } // continue Game
                        if (finished) { finished = false; } // Next Level
                        if (end) { Game1.points = 0; end = false; } // New Game after winning
                    }
                }
                else if (Button2.Contains(InputController.GetInstance().GetCursorPosition())) // hover over bottom button
                {
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.LightGrey);
                    if (InputController.GetInstance().GetLeftClick()) //click on bottom button
                    {
                        exit= true; //exit Game
                    }
                }
                else //button stays dark
                {
                    buttonGraphic1 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                    buttonGraphic2 = AssetController.GetInstance().getTexture2D(graphicsAssets.DarkGrey);
                }
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (paused || finished || end) //draw Menu with different states
            {
                spriteBatch.Draw(AssetController.GetInstance().getTexture2D(graphicsAssets.menuBackground), new Rectangle(256, 128, 704, 544), Color.White);
                spriteBatch.Draw(buttonGraphic1, new Rectangle(384, 384, 448, 96), Color.White);
                spriteBatch.Draw(buttonGraphic2, new Rectangle(384, 512, 448, 96), Color.White);
                spriteBatch.DrawString(arial24, "Points: " + Game1.points, new Vector2(288, 150), Color.White);
                if (paused)
                {
                    spriteBatch.DrawString(arial24, "Paused", new Vector2(544, 250), Color.White);
                    spriteBatch.DrawString(arial24, "Continue Game", new Vector2(500, 416), Color.Red);
                    spriteBatch.DrawString(arial24, "Exit", new Vector2(576, 544), Color.Red);
                }
                if (finished)
                {
                    if (beaten)
                    {
                        spriteBatch.DrawString(arial24, "You Beat The Level!", new Vector2(464, 250), Color.White);
                        spriteBatch.DrawString(arial24, "Next Level", new Vector2(536, 416), Color.Red);
                    }
                    else
                    {
                        spriteBatch.DrawString(arial24, "You Died!", new Vector2(536, 250), Color.White);
                        spriteBatch.DrawString(arial24, "New Game", new Vector2(528, 416), Color.Red);
                    }

                    spriteBatch.DrawString(arial24, "Exit", new Vector2(576, 544), Color.Red);
                }
                if (end)
                {

                    spriteBatch.DrawString(arial24, "New Game", new Vector2(528, 416), Color.Red);
                    spriteBatch.DrawString(arial24, "Exit", new Vector2(576, 544), Color.Red);
                    if (won)
                    {
                        spriteBatch.DrawString(arial24, "Congratulations!", new Vector2(496, 250), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(arial24, "Project Tank", new Vector2(512, 250), Color.White);
                    }
                }

            }
        }
    }

    
}
