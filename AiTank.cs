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

        private bool CheckLineOfSight(Vector2 target, bool includeAll)
        {
            Vector2 directionNormalized = Vector2.Normalize(target - position);
            int steps = (int)((target - position).Length() / 5);
            for(int i = 3; i < steps; i++)
            {
                Vector2 point = position + (directionNormalized * i * 5);

                // debug lines
                //Level.graphicsEffects.Add(new GraphicsEffect(AssetController.GetInstance().getTexture2D(graphicsAssets.Explosion),
                //               point, 0.4f, 1.02f, new Vector2(8, 8)));
                foreach (AiTank aiTank in Level.aitanks)
                {
                    if(aiTank != this && aiTank.GetCollisionBox().Contains(point))
                    {
                        return false;
                    }
                }
                foreach (Obstacle obstacle in Level.obstacles)
                {
                    if (obstacle.GetCollisionBox().Contains(point) && (includeAll || !obstacle.IsDestructible()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void getInput(GameTime gameTime)
        {
            RotateTurret(Level.tank.GetPosition());
            if(CheckLineOfSight(Level.tank.GetPosition(), false))
            {
                ShootStandard();
            }

            if (maxSpeed > 0) 
            {
                if (!CheckLineOfSight(position + (Utility.radToV2(rotation) * 80f), true) || speed <= 0.1f)
                {
                    // we either have hit a wall or are about to
                    // check if the sides are free
                    bool left = CheckLineOfSight(position + (Utility.radToV2(rotation - 0.7f) * 40), true);
                    bool right = CheckLineOfSight(position + (Utility.radToV2(rotation + 0.7f) * 40), true);
                    if (left && !right)  // left is free, right is not
                    {                        
                        currentAction = "turnLeft";
                    }
                    else if (right && !left) // right is free, left is not
                    {
                        currentAction = "turnRight";
                    }
                    else if (rng.Next(2) == 0) // both or neither are free => let rng decide
                    {
                        currentAction = "turnLeft";
                    }
                    else
                    {
                        currentAction = "turnRight";

                    }
                    decisionCooldown = 0.3f;
                }
                if (decisionCooldown <= 0)
                {
                    int decision = rng.Next(3);
                    decisionCooldown = Math.Max ((float)rng.NextDouble(), 0.4f);
                    switch (decision)
                    {
                        case 0:
                            currentAction = "turnLeft";
                            decisionCooldown *= 0.5f;
                            break;
                        case 1:
                            currentAction = "turnRight";
                            decisionCooldown *= 0.5f;
                            break;
                        case 2:
                            currentAction = "forward";
                            decisionCooldown *= 3f;
                            break;
                    }

                }
                decisionCooldown -= gameTime.ElapsedGameTime.Milliseconds / 1000f;

                //if (currentAction.Equals("forward"))
                //{                  
                //    //nothing, because we always go forward
                //}
                //else if (input.GetKeyDown(Keys.S))
                //{
                //    //slow down / go backwards
                //    Reverse();
                //}
                //else
                //{
                //    // no speed input => tank rolls to a halt
                //    Roll();
                //}


                if (currentAction.Equals("turnLeft"))
                {
                    RotateLeft();
                }
                if (currentAction.Equals("turnRight"))
                {
                    RotationRight();
                }
                SpeedUp(); // we always speed up
            }
            
        }
        public override void getHit(int damage)
        {
            currentHP -= damage;
            if (currentHP <= 0 && isAlive)
            {
                Level.dead += 1;                
                isAlive = false;
                turret.Die();
            }
        }
    }
}
