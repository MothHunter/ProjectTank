using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using static ProjectTank.Game1;

namespace ProjectTank
{
	internal class Obstacle
	{
		Texture2D sprite;
		Vector2 position;
		CollisionBox collisionBox;

		bool destructible;
		int health;
		int width;
		int height;
		Vector2 center;
		bool destroyed;

		public Obstacle(Vector2 position, Texture2D sprite, bool destructible, int health, int width, int height, Vector2 center)
		{
			this.position = position;
			this.sprite = sprite;
			this.destructible = destructible;
			this.health = health;
			this.width = width;
			this.height = height;
			this.center = center;
			this.collisionBox = new CollisionBox(center, 0f, width, height);
			if (destructible)
			{
				Game1.destructible.Add(collisionBox);
			}
			else 
			{
				Game1.indestructible.Add(collisionBox); 
			}
        }

		public CollisionBox GetCollisionBox() { return collisionBox; }



		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(sprite, position, Color.White);
		}

		public void Update(GameTime gameTime)
		{
			
		}
		public void OnHit(int damage)
		{
			if (!destroyed)
			{
				health -= damage;
				if (health < 0)
				{
					Destroy();
				}
			}
		}
		public void Destroy()
		{
			Game1.destructible.Remove(this.collisionBox);
			//sprite = AssetController.GetInstance().getTexture2D(graphicsAssets.adTest32);

        }
	}


}