using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FirstSemesterExam
{
    #region Enums
    /// <summary>
    /// used to indicate which pickup type is used, then passed on to the constructor (there is only 1 at the momment)
    /// </summary>
    public enum PickUpEnum {Health}
    #endregion
    /// <summary>
    /// Pickup class, which is used to make the type of pickup based on the enum value
    /// </summary>
    internal class PickUp : GameObject
    {
        #region Fields
        protected static Random random = new Random();
        public PickUpEnum pickUpType;
        private int pickUpTimer;
        private float elapsedTime;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for PickUp which takes a position as a parameter
        /// </summary>
        /// <param name="position"></param>
        public PickUp(Vector2 position)
        {
            scale = 2;
            pickUpType = PickUpEnum.Health;
            layerDepth = 0.4f;
            pickUpTimer = 5;


            this.position = position;
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            //if the pickup type is of Health, it loads the corresponding sprite
            if (pickUpType == PickUpEnum.Health)
            {
                sprites = new Texture2D[1];
                sprites[0] = content.Load<Texture2D>("PickUps\\Heart");
                
            }
            //more can be added if needed
        }

        public override void Update(GameTime gameTime)
        {
            //creates a timer after which the object is removed from the game
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(pickUpTimer < elapsedTime)
            {
                shouldBeRemoved = true;
            }
        }


        public override void OnCollision(GameObject other)
        {
            //if the player collides with the pickup it adds to the players health, if the player has less than maxhealth
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
        #endregion

    }
}
