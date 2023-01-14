using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectTank
{
	internal class Obstacle
	{
		Texture2D sprite;
		Vector2 position;
		CollisionBox collisionBox;

		bool destructible;
		int health;
		bool destroyed;

		public Obstacle(Vector2 position, Texture2D sprite, bool destructible, int health)
		{
			this.position = position;
			this.sprite = sprite;
			this.destructible = destructible;
			this.health = health;
		}


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
			//Remove = true;
		}
	}


}