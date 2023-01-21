using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
    internal class PlayerTank : Tank
    {
        public int shotSpecialShots = 0;
        public PlayerTank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite) : base(position, tankSprite, turretSprite)
        {
            maxHP = 100;
            currentHP = maxHP;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            // draw health bar
            Texture2D pixel = AssetController.GetInstance().getTexture2D(graphicsAssets.Pixel);
            Rectangle frame = new Rectangle((int)(position.X - (sprite.Width / 2) - 16), (int)(position.Y - sprite.Width / 2),
                                            8, sprite.Height);
            Rectangle life = new Rectangle((int)(position.X - (sprite.Width / 2) - 15), (int)((position.Y - sprite.Width / 2) + 1),
                                            6, sprite.Height - 2);
            if(currentHP < 0) { currentHP= 0; }
            Rectangle spentLife = new Rectangle((int)(position.X - (sprite.Width / 2) - 15), (int)((position.Y - sprite.Width / 2) + 1),
                                            6, (sprite.Height - 2) * Math.Min(maxHP - currentHP, maxHP) / maxHP);
            spriteBatch.Draw(pixel, frame, Color.Black);
            spriteBatch.Draw(pixel, life, Color.Green);
            spriteBatch.Draw(pixel, spentLife, Color.Red);
            if(shotSpecialShots < Level.dead)
            {
                Texture2D icon = AssetController.GetInstance().getTexture2D(graphicsAssets.IconSpecialShot);
                spriteBatch.Draw(icon, new Rectangle(1134, 34, 48, 48), Color.White);
            }
        }


        public override void getInput(GameTime gameTime)
        {

            // handle inputs from keyboard
            InputController input = InputController.GetInstance();
            if (input.GetKeyDown(Keys.W))
            {
                //Speed up
                SpeedUp();
            }
            else if (input.GetKeyDown(Keys.S))
            {
                //slow down / go backwards
                Reverse();
            }
            else
            {
                // no speed input => tank rolls to a halt
                Roll();
            }

            if (input.GetKeyDown(Keys.A))
            {
                RotateLeft();
            }
            if (input.GetKeyDown(Keys.D))
            {
                RotationRight();
            }


            // handle inputs from mouse
            RotateTurret(input.GetCursorPosition());

            if (input.GetLeftClick())
            {
                ShootStandard();
            }
            if (input.GetRightClick())
            {
                ShootSpecial();
            }
        }

        protected void ShootSpecial()
        {
            // no special shots available
            if (shotSpecialShots >= Level.dead) { return; }
            // fire cooldown not ready
            if (fireCooldownCountdown > 0)  { return; }
            shotSpecialShots += 1;

            fireCooldownCountdown = 2f;
            Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;
            Vector2 target = InputController.GetInstance().GetCursorPosition();
            SpecialShot shot = new SpecialShot(turret.GetRotation(), 10f, 50, position + offset, target);
            Level.specialShot = shot;
        }
        public override void getHit(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                isAlive = false;
                turret.Die();
                // TODO: zu endscreen weiterleiten
            }
        }
    }
}
