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
        public PickUp(Vector2 position)
        {
            scale = 2;
            pickUpType = PickUpEnum.Health;
            layerDepth = 0.4f;


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

        }


        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                if (other.Health < 100)
                {
                    other.Health += 25; //overhealing is possible
                    shouldBeRemoved = true;
                }
                
            }
        }

    }
}
