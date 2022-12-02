using FirstSemesterExam.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace FirstSemesterExam.Enemies
{
    internal class Slime : Enemy
    {
        #region Fields
        private Player player;
        #endregion

        #region Constructors
        public Slime(Player player) : base(player)
        {
            health = 20;
            speed = 100f;
            attackSpeed = 15f;
            attackRange = 25f;
            animationSpeed = 3f;
            expValue = 2;
            this.player = player;
        }

        public Slime(Player player, Vector2 parentPosition) : base(player)
        {
            health = 20f;
            speed = 100f;
            attackSpeed = 10f;
            attackRange = 10f;
            animationSpeed = 3f;
            expValue = 2;
            this.player = player;

            Vector2 offsetPosition = new Vector2(random.Next(-100, 100), random.Next(-100, 100));
            position = parentPosition + offsetPosition; 
        }
        #endregion

        #region Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Slime1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Slime2");
        }

        public override void OnCollision(GameObject other)
        {
            // 0.1% chance of slimes merging to BlobMonster 
            if (random.Next(1000) < 1)
            {
                if (other is Slime)
                {
                    // merge together to 1 BlobMonster 
                    GameState.InstantiateGameObject(new BlobMonster(player, position, other.GetPosition));

                    // remove both slimes 
                    ShouldBeRemoved = true;
                    other.ShouldBeRemoved = true;
                }
            } 

            base.OnCollision(other); 
        }
        #endregion
    }
}
