using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTank
{
	public class Obstacle
	{
		Texture2D sprite;				// Texture for Obstacle
		Texture2D hitSprite;			// Texture for damaged Obstacle (if destroyable)
        Texture2D destroyedSprite;		// Texture for destroyed Obstacle (if destroyable)
        Vector2 position;				// Position where the Obstacle is in the level
		CollisionBox collisionBox;      // Collision Box from the Obstacle

        bool destructible;				// true = can be destroyed
		int health;						// health for destroyable Obstacles
		bool destroyed;					// true = destroyed;

		// Constructor for Obstacles in the Level
		public Obstacle(Vector2 position, Texture2D sprite, Texture2D hitSprite, Texture2D destroyedSprite, bool destructible, int health, int width, int height, Vector2 center)
		{
			this.position = position;
			this.sprite = sprite;
			this.hitSprite = hitSprite;
			this.destroyedSprite = destroyedSprite;
			this.destructible = destructible;
			this.health = health;
			this.destroyed = false;
			this.collisionBox = new CollisionBox(center, 0f, width, height);
		}

        // Constructor for Map Border; just adding a obstacle only containing the CollisionBoxes and the bool destroyable for Collision Control
        public Obstacle(CollisionBox border)
		{
			this.collisionBox = border;
			this.destructible = false;
			Level.obstacles.Add(this);
		}

		public CollisionBox GetCollisionBox() { return collisionBox; }		// Getter for Collision Box

		public bool IsDestructible() { return destructible; }		// Getter destructible

		public bool IsDestroyed() { return destroyed; }     // Getter destroyd

		// Draw Obstacles if they have a Texture. Only the Border Obstacles do not have a Texture
        public void Draw(SpriteBatch spriteBatch)
		{
			if(sprite != null) 
			{
                spriteBatch.Draw(sprite, position, Color.White);
            }	
		}

		// If a Projectile hit a target, damage will be applied
		public void OnHit(int damage)
		{
			if (!destroyed)
			{
				health -= damage;
				sprite = hitSprite;		// Change Sprite
				if (health < 0)
				{
					Destroy();
					sprite = destroyedSprite;		// Change Sprite
                }
			}
		}

		// Obstacles dropping to 0 health, will lose its collision Box. Because of the way we check for Collision Boxes we set the Collision Box to a 1*1 Block in the Border 
		public void Destroy()
		{
			this.collisionBox = new CollisionBox(new Vector2(1,1),0f,1,1);

        }
	}


}