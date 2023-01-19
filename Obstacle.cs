using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static ProjectTank.Level;

namespace ProjectTank
{
	public class Obstacle
	{
		Texture2D sprite;
		Texture2D hitSprite;
        Texture2D destroyedSprite;
        Vector2 position;
		CollisionBox collisionBox;

		bool destructible;
		int health;
		int width;
		int height;
		Vector2 center;
		bool destroyed;

		public Obstacle(Vector2 position, Texture2D sprite, Texture2D hitSprite, Texture2D destroyedSprite, bool destructible, int health, int width, int height, Vector2 center)
		{
			this.position = position;
			this.sprite = sprite;
			this.hitSprite = hitSprite;
			this.destroyedSprite = destroyedSprite;
			this.destructible = destructible;
			this.health = health;
			this.width = width;
			this.height = height;
			this.center = center;
			this.destroyed = false;
			this.collisionBox = new CollisionBox(center, 0f, width, height);
		}

		public Obstacle(CollisionBox border)
		{
			this.collisionBox = border;
			this.destructible = false;
			Level.obstacles.Add(this);
		}


		public CollisionBox GetCollisionBox() { return collisionBox; }

		public bool IsDestructible() { return destructible; }

		public bool IsDestroyed() { return destroyed; }

		public void Draw(SpriteBatch spriteBatch)
		{
			if(sprite != null) 
			{
                spriteBatch.Draw(sprite, position, Color.White);
            }
			
		}

		public void Update(GameTime gameTime)
		{
			
		}
		public void OnHit(int damage)
		{
			if (!destroyed)
			{
				health -= damage;
				sprite = hitSprite;
				if (health < 0)
				{
					Destroy();
					sprite = destroyedSprite;
				}
			}
		}
		public void Destroy()
		{
			this.collisionBox = new CollisionBox(new Vector2(1,1),0f,1,1);

        }
	}


}