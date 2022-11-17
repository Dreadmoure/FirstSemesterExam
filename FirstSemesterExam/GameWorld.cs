using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FirstSemesterExam
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize; 
        // lists for GameObjects 
        private List<GameObject> gameObjects = new List<GameObject>(); 
        private List<GameObject> gameObjectsToAdd = new List<GameObject>();
        // fields for enemy spawner
        private float totalGameTime; 
        private float timeSinceEnemySpawn;
        private float timeBetweenEnemySpawn = 5f;
        private Random random = new Random();

        public static Vector2 GetScreenSize
        {
            get { return screenSize; }
        }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // set screensize 
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Survive Us";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content); 
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            base.Update(gameTime);
        }

        private void SpawnEnemy()
        {
            switch (random.Next(0, 100)) // 0 to 99 
            {
                case int r when r >= 0: // 45% chance 
                    InstantiateGameObject(new BlobMonster());
                    break;
                case int r when r >= 45 && r < 65: // 20% chance 
                    InstantiateGameObject(new HornedGuy());
                    break;
                case int r when r >= 65 && r < 85: // 20% chance 
                    InstantiateGameObject(new Turned());
                    break;
                case int r when r >= 85: // 15% chance 
                    InstantiateGameObject(new Robot());
                    break;
                default:
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

        public void InstantiateGameObject(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        private void AddGameObjects()
        {
            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObject.LoadContent(Content);
                gameObjects.Add(gameObject);
            }

            gameObjectsToAdd.Clear();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp); 

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch); 
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}