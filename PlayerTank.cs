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
        public int shotSpecialShots = 0; // counts the special shots fired; will be reset every level
        int selectedSpecial = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"> position n the map where the tank is placed</param>
        /// <param name="tankSprite">graphics used for the body of the tank</param>
        /// <param name="turretSprite">graphics used for the turret</param>
        public PlayerTank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite) : base(position, tankSprite, turretSprite)
        {
            maxHP = 100; // sets the max HP of the tank to 100
            currentHP = maxHP; // sets the current hp as max hp
        }

        // extends the draw methode of tank to also add a healthbar on the side of it
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); // draws the tank with the draw methode in tank class
            
            // loads the pixle graphic
            Texture2D pixel = AssetController.GetInstance().getTexture2D(graphicsAssets.Pixel);
            // creates a frame to surround the healthbar
            Rectangle frame = new Rectangle((int)(position.X - (sprite.Width / 2) - 16), (int)(position.Y - sprite.Width / 2),
                                            8, sprite.Height);
            // fills the entire frame with green pixles
            Rectangle life = new Rectangle((int)(position.X - (sprite.Width / 2) - 15), (int)((position.Y - sprite.Width / 2) + 1),
                                            6, sprite.Height - 2);
            // makes sure the current hp are not below 0 to later draw the health bar correctly
            if (currentHP < 0) { currentHP= 0; }
            // now we create a red bar that will be drawn over your green bar the more hp were lost the bigger the red bar gets
            Rectangle spentLife = new Rectangle((int)(position.X - (sprite.Width / 2) - 15), (int)((position.Y - sprite.Width / 2) + 1),
                                            6, (sprite.Height - 2) * Math.Min(maxHP - currentHP, maxHP) / maxHP);
            spriteBatch.Draw(pixel, frame, Color.Black); // draws the frame
            spriteBatch.Draw(pixel, life, Color.Green); // draws the green livebar
            spriteBatch.Draw(pixel, spentLife, Color.Red); // draws the red liveLostBar
            if(shotSpecialShots < Level.dead) // checks if there are special shots available and if so draws a special shot icon on screen
            {
                // not selected special shot is grayed out
                Texture2D icon = AssetController.GetInstance().getTexture2D(graphicsAssets.IconSpecialShot);
                spriteBatch.Draw(icon, new Rectangle(1158, 8, 48, 48), selectedSpecial==1?Color.White: Color.Gray);
                Texture2D icon0 = AssetController.GetInstance().getTexture2D(graphicsAssets.IconSpecialShot0);
                spriteBatch.Draw(icon0, new Rectangle(1108, 8, 48, 48), selectedSpecial == 0 ? Color.White : Color.Gray);
            }
        }


        public override void getInput(GameTime gameTime)
        {

            // handle inputs from keyboard
            InputController input = InputController.GetInstance();
            // if the w key is pressed the tank will speed up till its speedlimit is reached
            if (input.GetKeyDown(Keys.W))
            {
                //Speed up
                SpeedUp();
            }
            // if the s key is pressed the tank will slow down; if the speed hits 0 the tank will go backwards
            else if (input.GetKeyDown(Keys.S))
            {
                Reverse();
            }
            else
            {
                // no speed input => tank rolls to a halt
                Roll();
            }
            // if the a key is pressed the tank will rotate to the right
            if (input.GetKeyDown(Keys.A))
            {
                RotateLeft();
            }
            // if the d key is pressed the tank will rotate to the left
            if (input.GetKeyDown(Keys.D))
            {
                RotationRight();
            }

            // uses the current mouse position to rotate the turrent in its direction
            RotateTurret(input.GetCursorPosition());

            // if the left mousebutton is clicked the tank tries to fire a standard shot
            if (input.GetLeftClick())
            {
                ShootStandard();
            }
            // if the right mousebutton is clicked the tank tries to fire a special shot
            if (input.GetRightClick())
            {
                ShootSpecial();
            }

            if (input.GetKeyDown(Keys.Q))
            {
                selectedSpecial = 0;
            }
            if (input.GetKeyDown(Keys.E))
            {
                selectedSpecial = 1;
            }
        }

        // tanks tries to fire a special shot
        protected void ShootSpecial()
        {
            // checks if a special shot is availabe by comparing the special shots already shot and the enemies dead
            if (shotSpecialShots >= Level.dead) { return; }
            // checks if the firecooldown is ready to fire
            if (fireCooldownCountdown > 0) { return; }

            if (selectedSpecial == 0)
            {
                // get graphic for shot
                Texture2D projectileSprite = AssetController.GetInstance()
                    .getTexture2D(graphicsAssets.StandardProjectile);

                // calculate offset for where to create the shot relative to tank position
                // so that the shot is not created on top of the tank
                Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;

                // add shot to list of projectiles managed by the Level class
                Level.projectiles.Add(new Projectile(position + offset, projectileSprite,
                    turret.GetRotation(), 10, 0, 35));
                Level.projectiles.Add(new Projectile(position + offset, projectileSprite,
                    turret.GetRotation(), 9, 0, 35));
                Level.projectiles.Add(new Projectile(position + offset, projectileSprite,
                    turret.GetRotation(), 11, 0, 35));
            }
            else
            {              
                // the offset is so the shot does not generate inside the tank but at the front of the turret
                Vector2 offset = Utility.radToV2(turret.GetRotation()) * 16;
                // gets the position of the mouse
                Vector2 target = InputController.GetInstance().GetCursorPosition();
                // creats a new specialshot
                SpecialShot shot = new SpecialShot(turret.GetRotation(), 10f, 50, position + offset, target);
                // attatches the shot to the level
                Level.specialShot = shot;
            }
            // special shot will be shot so the number of shots shot is updated
            shotSpecialShots += 1;
            // the firecooldown is set because the shot will get shot
            fireCooldownCountdown = 2f;
        }
        // if a projectile hits a tank the damage of the tank is given to this methode to update the hitpoints of the tank and let it die if the hp are below or at 0
        public override void getHit(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0) // checks if the hp are at or below 0 and if so let the turret and the tank die
            {
                isAlive = false;
                turret.Die();
            }
        }
    }
}
