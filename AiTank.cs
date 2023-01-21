using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTank
{
    internal abstract class AiTank : Tank
    {
        Random rng;
        float decisionCooldown = 0;
        String currentAction;
        public AiTank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite) : base(position, tankSprite, turretSprite)
        {
            rng= new Random();
        }

        public override void getInput(GameTime gameTime)
        {
            RotateTurret(Level.tank.GetPosition());
            ShootStandard();

            if (maxSpeed > 0) 
            { 
                if (decisionCooldown <= 0)
                {
                    int decision = rng.Next(3);
                    decisionCooldown = (float)rng.NextDouble();
                    switch (decision)
                    {
                        case 0:
                            currentAction = "turnLeft";
                            break;
                        case 1:
                            currentAction = "forward";
                            decisionCooldown *= 3;
                            break;
                        case 2:
                            currentAction = "turnRight";
                            break;
                    }

                }
                decisionCooldown -= gameTime.ElapsedGameTime.Milliseconds / 1000f;

                if (currentAction.Equals("forward"))
                {
                    //Speed up
                    SpeedUp();
                }
                //else if (input.GetKeyDown(Keys.S))
                //{
                //    //slow down / go backwards
                //    Reverse();
                //}
                else
                {
                    // no speed input => tank rolls to a halt
                    Roll();
                }
                if (currentAction.Equals("turnLeft"))
                {
                    RotateLeft();
                }
                if (currentAction.Equals("turnRight"))
                {
                    RotationRight();
                }
            }
            
        }
        public override void getHit(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                if (isAlive)
                {
                    Level.dead += 1;
                }
                isAlive = false;
                turret.Die();
            }
        }
    }
}
