using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam
{
    public enum PickUpEnum {Health, KillAll }
    internal class PickUp : GameObject
    {

        protected static Random random = new Random();
        public PickUpEnum pickUpType;
        private int pickUpTimer;
        private float elapsedTime;
        public PickUp(Vector2 position)
        {
            scale = 2;
            pickUpType = PickUpEnum.Health;
            layerDepth = 0.4f;
            pickUpTimer = 5;


            this.position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            if (pickUpType == PickUpEnum.Health)
            {
                sprites = new Texture2D[1];
                sprites[0] = content.Load<Texture2D>("PickUps\\Heart");
                
            }
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(pickUpTimer < elapsedTime)
            {
                shouldBeRemoved = true;
            }
        }


        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                Player player = (Player)other;
                if(player.Health < player.MaxHealth)
                {
                    other.Health += 25;
                    shouldBeRemoved = true;
                }
                    
            }
        }

    }
}
