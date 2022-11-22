using FirstSemesterExam.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace FirstSemesterExam.Menu
{
    public class GameState : State
    {
        #region fields 
        // lists for GameObjects 
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        // fields for enemy spawner
        private float totalGameTime;
        private float timeSinceEnemySpawn;
        private float timeBetweenEnemySpawn = 5f;
        private Random random = new Random();

        private SpriteFont font;
        private Player player;
        #endregion

        public GameState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            player = new Player();
            gameObjects.Add(player);
        }

        public override void LoadContent()
        {
            font = content.Load<SpriteFont>("Fonts\\textFont");

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
            foreach (GameObject gameObject in gameObjects)
            {
                foreach (GameObject other in gameObjects)
                {
                    if (gameObject.IsColliding(other))
                    {
                        gameObject.OnCollision(other);
                        other.OnCollision(gameObject);
                    }
                }
            }

            RemoveGameObjects();

            totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // set spawntime lower each 5 min. (5 * 60 = 300sec) 
            if (totalGameTime % 300 == 0 && timeBetweenEnemySpawn > 0f)
            {
                timeBetweenEnemySpawn -= 0.5f;
            }
            timeSinceEnemySpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceEnemySpawn >= timeBetweenEnemySpawn)
            {
                SpawnEnemy();
                timeSinceEnemySpawn = 0f;
            }

            AddGameObjects();
        }

        private void SpawnEnemy()
        {
            switch (random.Next(0, 100)) // 0 to 99 
            {
                case int r when r >= 0 && r < 45: // 45% chance 
                    InstantiateGameObject(new BlobMonster(player));
                    break;
                case int r when r >= 45 && r < 65: // 20% chance 
                    InstantiateGameObject(new HornedGuy(player));
                    break;
                case int r when r >= 65 && r < 85: // 20% chance 
                    InstantiateGameObject(new Turned(player));
                    break;
                case int r when r >= 85: // 15% chance 
                    InstantiateGameObject(new Robot(player));
                    break;
                default:
                    // throw exception 
                    break;
            }
        }

        private void RemoveGameObjects()
        {
            // temporary list for objects that should be removed from gameObjects list 
            List<GameObject> gameObjectsToBeRemoved = new List<GameObject>();

            // add gameObjects that should be removed to the temporary list 
            foreach (GameObject gameObject in gameObjects)
            {
                bool shouldRemoveGameObject = gameObject.ShouldBeRemoved;
                if (shouldRemoveGameObject)
                {
                    gameObjectsToBeRemoved.Add(gameObject);
                }
            }

            // remove objects in temporary list from the gameObjects list 
            foreach (GameObject gameObject in gameObjectsToBeRemoved)
            {
                gameObjects.Remove(gameObject);
            }
        }

        public static void InstantiateGameObject(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        private void AddGameObjects()
        {
            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObject.LoadContent(content);
                gameObjects.Add(gameObject);
            }

            gameObjectsToAdd.Clear();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.DrawString(font, $"Objects: {gameObjects.Count}\nMouseAngle {player.MouseAngle()}", Vector2.Zero, Color.White);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
