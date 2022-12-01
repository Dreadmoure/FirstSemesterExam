using FirstSemesterExam.Enemies;
using FirstSemesterExam.HighScore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace FirstSemesterExam.Menu
{
    public class GameState : State
    {
        #region fields 
        // lists for GameObjects 
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        public static List<GameObject> enemies = new List<GameObject>();
        //Background
        private Texture2D background;
        // Global timer
        private static float globalGameTimer1;
        private static int globalGameTimer2;
        private string globalGameTimerText;
        private Color timeTextColor;
        private string waveTimerText;
        // fields for enemy spawner
        private float totalGameTime;
        private float timeSinceEnemySpawn;
        private float timeBetweenEnemySpawn = 1f;
        private float timeSinceEnemyWave;
        private float timeBetweenEnemyWave = 60f;
        private int increaseWaveSize; 
        private Random random = new Random();
        private Texture2D pixel;
        private SpriteFont font;
        public static Player player;
        private static int score = 0;
        private static int kills; 
        // pause menu 
        private static bool paused = false;
        private List<Button> pausedButtons;
        private Button resumeGameButton;
        private Button backToMenuButton;
        private Button quitGameButton;
        private Texture2D pausedTexture; 
        // game over menu 
        private Texture2D gameOverTexture; 
        private static bool gameOver = true;
        private List<Component> gameOverComponents;
        private Button saveScoreButton;
        private TextBox enterNameTextbox; 
        // levelup 
        private LevelUpCard[] cardArray;
        private LevelUpCard card1;
        private LevelUpCard card2;
        private LevelUpCard card3;

        private List<GameObject> currentCollisions = new List<GameObject>();
        private List<GameObject> previousCollisions = new List<GameObject>();
        #endregion

        public static bool HandlePause
        {
            set { paused = value; }
        }
        public static bool GetGameOver
        {
            get { return gameOver; }
        }


        public List<GameObject> GetEnemies
        { 
            get { return enemies; } 
        }


        public GameState(ContentManager content, GraphicsDevice graphicsDevice, GameWorld game) : base(content, graphicsDevice, game)
        {
            player = new Player();
            gameObjects.Add(player);
            kills = 0;

            //sets the timer and color of the timer
            globalGameTimer1 = 0f;
            globalGameTimer2 = 0;
            globalGameTimerText = "Time";

            //waveTimeColor
            waveTimerText = "Next Wave In";
            timeTextColor.R = 58;
            timeTextColor.G = 77;
            timeTextColor.B = 82;


            Color buttonColor = Color.Blue; 

            // buttons for pause screen 
            resumeGameButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 - GameWorld.GetScreenSize.Y / 6), "Resume Game", buttonColor);
            backToMenuButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), "Main Menu", buttonColor);
            quitGameButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 + GameWorld.GetScreenSize.Y / 6), "Quit Game", buttonColor);
            pausedButtons = new List<Button>() { resumeGameButton, backToMenuButton, quitGameButton };

            // buttons for game over screen 
            gameOver = false; 
            saveScoreButton = new Button(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2 + GameWorld.GetScreenSize.Y / 6), "Save score", Color.Red);
            enterNameTextbox = new TextBox(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), "");
            gameOverComponents = new List<Component> { saveScoreButton, enterNameTextbox }; 

            card1 = new LevelUpCard(new Vector2(GameWorld.GetScreenSize.X / 3, GameWorld.GetScreenSize.Y / 2));
            card2 = new LevelUpCard(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2));
            card3 = new LevelUpCard(new Vector2(GameWorld.GetScreenSize.X / 1.5f, GameWorld.GetScreenSize.Y / 2));
            cardArray = new LevelUpCard[3] { card1, card2, card3 };

            LoadContent(); 
        }

        public override void LoadContent()
        {

            background = content.Load<Texture2D>("lvl");
            gameOverTexture = content.Load<Texture2D>("Menus\\GameOverScreen");
            pausedTexture = content.Load<Texture2D>("Menus\\paused"); 

            font = content.Load<SpriteFont>("Fonts\\textFont");
            pixel = content.Load<Texture2D>("pixel");
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(content);
            }

            //loads the content for all the buttons
            foreach (Button button in pausedButtons)
            {
                button.LoadContent(content);
            }

            foreach (Component component in gameOverComponents)
            {
                component.LoadContent(content);
             }

            foreach (LevelUpCard card in cardArray)
            {
                card.LoadContent(content);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!paused && !Player.LeveledUp && !gameOver)
            {
                CalculateScore(); 
                
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    paused = true;
                }

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
                            currentCollisions.Add(other);

                            if (!previousCollisions.Contains(other))
                            {
                                gameObject.OnCollisionEnter(other);
                            }
                        }
                    }
                }
                previousCollisions = currentCollisions;
                currentCollisions = new List<GameObject>();

                RemoveGameObjects();

                // the global timer used to tell the user how long they have survived
                globalGameTimer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                // purely used to convert the timer into an int so it can be printed out to the screen later
                globalGameTimer2 = (int)globalGameTimer1;


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
                // waves 
                timeSinceEnemyWave += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(timeSinceEnemyWave >= timeBetweenEnemyWave)
                {
                    SpawnWave(); 
                    timeSinceEnemyWave = 0f;
                    if (timeBetweenEnemyWave > 10f)
                    {
                        // set wave spawntime lower (by 5sec) 
                        timeBetweenEnemyWave -= 5f;
                    }
                    // increase wave size 
                    increaseWaveSize += 5; 
                }

                AddGameObjects();

                if(player.Health < 0)
                {
                    gameOver = true; 
                }

                if(player.Exp >= player.MaxExp)
                {
                    
                    foreach(LevelUpCard card in cardArray)
                    {
                        card.RandomCard(content);
                    }

                    while (card1.GetCardIndex == card2.GetCardIndex)
                    {
                        card2.RandomCard(content);
                    }

                    while (card1.GetCardIndex == card3.GetCardIndex || card2.GetCardIndex == card3.GetCardIndex)
                    {
                        card3.RandomCard(content);
                    }
                }
            }
            else if(paused)
            {
                foreach (Button button in pausedButtons)
                {
                    button.Update(gameTime);
                }

                if (resumeGameButton.isClicked)
                {
                    resumeGameButton.isClicked = false;
                    paused = false;
                }
                if (backToMenuButton.isClicked)
                {
                    backToMenuButton.isClicked = false;
                    game.ChangeState(GameWorld.GetMenuState);
                }
                if (quitGameButton.isClicked)
                {
                    quitGameButton.isClicked = false;
                    game.Exit();
                }
            }
            else if (gameOver)
            {
                foreach (Component component in gameOverComponents)
                {
                    component.Update(gameTime);
                }

                string name = ""; 

                if (enterNameTextbox.isActive)
                {
                    name = enterNameTextbox.TextEntered; 
                }
                if (saveScoreButton.isClicked)
                {
                    //string name = "kage";
                    File.AppendAllText("./scores.txt", name + " " + score + "\n");

                    saveScoreButton.isClicked = false;
                    GameWorld.HandleHighscoreState = new HighscoreState(content, graphicsDevice, game);
                    game.ChangeState(GameWorld.HandleHighscoreState);
                }
            }
            else if (Player.LeveledUp)
            {
                foreach (LevelUpCard card in cardArray)
                {
                    card.Update(gameTime);
                }


                if (card1.isClicked)
                {
                    card1.isClicked = false;

                    Player.LeveledUp = false;
                    player.InitializeUpgrade(card1.GetCardIndex);
                }
                if (card2.isClicked)
                {
                    card2.isClicked = false;

                    Player.LeveledUp = false;
                    player.InitializeUpgrade(card2.GetCardIndex);
                }
                if (card3.isClicked)
                {
                    card3.isClicked = false;

                    Player.LeveledUp = false;
                    player.InitializeUpgrade(card3.GetCardIndex);
                }
            }
        }


        private void SpawnEnemy()
        {
            switch (random.Next(0, 100)) // 0 to 99 
            {
                case int r when r >= 0 && r < 30: // 30% chance 
                    InstantiateGameObject(new BlobMonster(player));
                    break;
                case int r when r >= 30 && r < 45: // 15% chance 
                    InstantiateGameObject(new Slime(player));
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

        private void SpawnWave()
        {
            // random number of enemies in wave 
            int numberOfEnemies = random.Next(5, 20) + increaseWaveSize;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy(); 
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
                if (gameObject is Enemy)
                {
                    enemies.Remove(gameObject);
                }
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
                if (gameObject is Enemy)
                {
                    enemies.Add(gameObject);
                }
            }

            gameObjectsToAdd.Clear();
        }
        public static void CountEnemyKill()
        {
            kills += 1; 
        }
        public static void CalculateScore()
        {
            score = kills * globalGameTimer2; 
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(background, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(background.Width/2, background.Height/2), 3f, SpriteEffects.None, 0.01f);

            float gGTToriginX = font.MeasureString(globalGameTimerText).X / 2;
            float gGTToriginY = font.MeasureString(globalGameTimerText).Y / 2;

            string globalGameTimer3 = globalGameTimer2.ToString();

            float gGToriginX = font.MeasureString(globalGameTimer3).X / 2;
            float gGToriginY = font.MeasureString(globalGameTimer3).Y / 2;

            //draw globel timer
            spriteBatch.DrawString(font, globalGameTimerText, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 15f), timeTextColor, 0f, new Vector2(gGTToriginX, gGTToriginY), 3.5f, SpriteEffects.None, 0.3f);
            spriteBatch.DrawString(font, globalGameTimer3, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 15f + 60), timeTextColor, 0f, new Vector2(gGToriginX, gGToriginY), 3.5f, SpriteEffects.None, 0.3f);

            float wTToriginX = font.MeasureString(waveTimerText).X / 2;
            float wTToriginY = font.MeasureString(waveTimerText).Y / 2;
            string waveTimer = ((int)(timeBetweenEnemyWave - timeSinceEnemyWave)).ToString();
            float wToriginX = font.MeasureString(waveTimer).X / 2;
            float wToriginY = font.MeasureString(waveTimer).Y / 2;
            
            spriteBatch.DrawString(font, waveTimerText, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 1.12f), timeTextColor, 0f, new Vector2(wTToriginX, wTToriginY), 3.5f, SpriteEffects.None, 0.3f);
            spriteBatch.DrawString(font, waveTimer, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 1.12f + 60), timeTextColor, 0f, new Vector2(wToriginX, wToriginY), 3.5f, SpriteEffects.None, 0.3f);

            //spriteBatch.DrawString(font, $"Objects: {gameObjects.Count}\nMouseAngle: {player.MouseAngle()}\nPlayer HP: {player.Health}\nPlayer MaxHP: {player.MaxHealth}\nPlayer EXP: {player.Exp}\nPlayer LVL: { player.LevelIndicator}\nLightsaberLvl: {player.LightSaberLvl}\nTKLvl: {player.ThrowingKnifeLvl}\nMagicMissile: {player.MagicMissileLvl}\nKills: {kills}\nNext wave in: {(int)(timeBetweenEnemyWave - timeSinceEnemyWave)}", Vector2.Zero, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            if (paused)
            {
                spriteBatch.Draw(pausedTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.95f); 
                
                foreach (Button button in pausedButtons)
                {
                    button.Draw(gameTime, spriteBatch);
                }
            }
            if (gameOver)
            {
                spriteBatch.Draw(gameOverTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

                string text = $"SCORE: {score}";
                float x = GameWorld.GetScreenSize.X / 2 - font.MeasureString(text).X / 2;
                float y = GameWorld.GetScreenSize.Y / 3 - font.MeasureString(text).Y / 2;
                spriteBatch.DrawString(font, text, new Vector2(x,y), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.95f); 

                foreach (Component component in gameOverComponents)
                {
                    component.Draw(gameTime, spriteBatch);
                }
            }

            if (Player.LeveledUp)
            {
                foreach (LevelUpCard card in cardArray)
                {
                    card.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }
    }
}
