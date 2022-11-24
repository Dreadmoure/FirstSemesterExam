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
        public Slime(Player player) : base(player)
        {
            health = 100f;
            speed = 100f;
            attackSpeed = 10f;
            attackRange = 10f;
            animationSpeed = 3f;
            expValue = 2;
        }
        public Slime(Player player, Vector2 parentPosition) : base(player)
        {
            health = 20f;
            speed = 10f;
            attackSpeed = 10f;
            attackRange = 10f;
            animationSpeed = 3f;
            expValue = 2;

            Vector2 offsetPosition = new Vector2(random.Next(-100, 100), random.Next(-100, 100));
            position = parentPosition + offsetPosition; 
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Slime1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Slime2");
        }
    }
}
