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
        Random rng;                 // random number generator for AI decisions
        float decisionCooldown = 0; // how long the AI sticks to a movement decision it has made; random
        String currentAction;       // the current movement decision of the AI
        bool stuck = false;         // whether the tank has hit an obstacle; used for AI controll

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">position where the tank is placed (centered)</param>
        /// <param name="tankSprite">graphics used for the body of the tank</param>
        /// <param name="turretSprite">graphics used for the turret of the tank</param>
        public AiTank(Vector2 position, Texture2D tankSprite, Texture2D turretSprite) : base(position, tankSprite, turretSprite)
        {
            rng= new Random();      // initialize random number generator
        }

        /// <summary>
        /// Checks if the line between tank position and "target" intersects with an obstacle
        /// or other ai tank. Used for line of fire check and for movement to reduce collisions
        /// </summary>
        /// <param name="target">target coordinates</param>
        /// <param name="includeAll">hether to include destructible objects</param>
        /// <returns>false if path is obstructed, true if it is not</returns>
        private bool CheckLineOfSight(Vector2 target, bool includeAll)
        {
            // calculate the normalized direction vector from position to the target
            Vector2 directionNormalized = Vector2.Normalize(target - position);

            // calculate how many steps of length 5 are needed to "walk" the entire distance to target
            int steps = (int)((target - position).Length() / 5);

            for(int i = 3; i < steps; i++)
            {
                // calculate position of next step
                Vector2 point = position + (directionNormalized * i * 5);

                // debug lines
                // uncomment these to visualize the checked line-of-sight paths
                //Texture2D pixel = AssetController.GetInstance().getTexture2D(graphicsAssets.Pixel);
                //Level.graphicsEffects.Add(new GraphicsEffect(pixel,
                //              point, 0.05f, 1.02f, new Vector2(2, 2)));

                // check if line intersects with ai tanks
                foreach (AiTank aiTank in Level.aitanks)
                {
                    if(aiTank != this && aiTank.GetCollisionBox().Contains(point))
                    {
                        return false;
                    }
                }

                // check if line intersects with any (includeAll == true) or
                // only indistructible (includeAll == false) obstacles
                foreach (Obstacle obstacle in Level.obstacles)
                {
                    if (obstacle.GetCollisionBox().Contains(point) && 
                        (includeAll || !obstacle.IsDestructible()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Overrides the abstract method of the Tank class.
        /// Contains the AI that controlls the tank
        /// </summary>
        /// <param name="gameTime">game clock</param>
        public override void getInput(GameTime gameTime)
        {
            // rotate turret towards the player tank
            RotateTurret(Level.tank.GetPosition());

            // shoot if there is no other ai tank or indestructible obstacle in the way
            if(CheckLineOfSight(Level.tank.GetPosition(), false))
            {
                ShootStandard();
            }

            // movement code for non-stationary tanks
            if (maxSpeed > 0)
            {
                if (speed > 0.15f) { stuck = false; }

                // check if we are about to hit something or already have (speed ~ 0)
                // if stuck is true this has been executed at least once in the current situation
                // and will be skipped until we can move again in order to make the AI stick to 
                // one turning decision untill it has a free path
                // otherwise there could be a lot of back-and-forth wiggling when in a dead end
                if (!stuck && (!CheckLineOfSight(position + (Utility.radToV2(rotation) * 100f), true) || speed <= 0.05f))
                {
                    // we either have hit a wall or are about to
                    // if speed is ~ 0 we have allready collided and the stuck condition is enabled
                    if (speed <= 0.05f) { stuck = true; }

                    // check left and right to decide which direction to turn in
                    bool left = CheckLineOfSight(position + (Utility.radToV2(rotation - 0.7f) * 60), true);
                    bool right = CheckLineOfSight(position + (Utility.radToV2(rotation + 0.7f) * 60), true);
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
                    decisionCooldown = 0.3f; // stick to this decision for at least 0.3 seconds
                }

                // the current movement decision has expried, make a new one
                if (!stuck && decisionCooldown <= 0)
                {
                    int decision = rng.Next(3);  // decision
                    //expiry time of decision; base is between 0.4 an 1 seconds
                    decisionCooldown = (float)(rng.NextDouble() * 0.6) + 0.4f;
                    switch (decision)
                    {
                        case 0:
                            currentAction = "turnLeft";
                            decisionCooldown *= 0.5f;   // turn decsions expire faster to avoid u-turns
                            break;
                        case 1:
                            currentAction = "turnRight";
                            decisionCooldown *= 0.5f;
                            break;
                        case 2:
                            currentAction = "forward";
                            decisionCooldown *= 3f;     // going in a streight line expires slower
                            break;
                    }

                }

                decisionCooldown -= gameTime.ElapsedGameTime.Milliseconds / 1000f;

                // execute movement decision
                SpeedUp(); // we always speed up
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

        /// <summary>
        /// The tank takes damage from a hit
        /// </summary>
        /// <param name="damage">points of damage</param>
        public override void getHit(int damage)
        {
            currentHP -= damage;

            // check if tank is still alive after the hit
            if (currentHP <= 0 && isAlive)
            {
                Level.dead += 1;    // add 1 to the targets killed for this level
                isAlive = false;
                turret.Die();       // also disable the turret
            }
        }
    }
}
