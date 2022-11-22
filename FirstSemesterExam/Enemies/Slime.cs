﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstSemesterExam.Enemies
{
    internal class Slime : Enemy
    {
        public Slime() : base()
        {
            health = 20;
            speed = 10f;
            attackSpeed = 10f;
            attackRange = 10f;
        }
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[2];
            sprites[0] = content.Load<Texture2D>("Enemies\\Slime1");
            sprites[1] = content.Load<Texture2D>("Enemies\\Slime2");
        }
    }
}
