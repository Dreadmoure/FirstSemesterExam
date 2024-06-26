﻿using FirstSemesterExam.Enemies;
using FirstSemesterExam.HighScore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
    /// <summary>
    /// GameState class, this is where we put all in, it is the game "scene"
    /// </summary>
    public class GameState : State
    {
        #region Fields 
        // lists for GameObjects 
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        public static List<GameObject> enemies = new List<GameObject>();
        // Background
        private Texture2D background;
        private static Song backgroundMusic;
        private static TimeSpan playPosition; 
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
        // random, font and the player
        private Random random = new Random();
        private SpriteFont font;
        public static Player player;
        // score related fields
        private static int score = 0;
        private static int kills;
        private Button saveScoreButton;
        private TextBox enterNameTextbox;
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
        // levelup related
        private LevelUpCard[] cardArray;
        private LevelUpCard card1;
        private LevelUpCard card2;
        private LevelUpCard card3;

        #endregion

        #region Properties
        /// <summary>
        /// Used to set the paused bool for the pause menu/levelup screen
        /// </summary>
        public static bool HandlePause
        {
            set { paused = value; }
        }
        /// <summary>
        /// Used to set the gameOver bool for the gameover screen
        /// </summary>
        public static bool GetGameOver
        {
            get { return gameOver; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the GameState
        /// </summary>
        /// <param name="content">the content manager</param>
        /// <param name="graphicsDevice">the graphics device</param>
        /// <param name="game">the game world</param>
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

            // level up cards for the level up screen
            card1 = new LevelUpCard(new Vector2(GameWorld.GetScreenSize.X / 3, GameWorld.GetScreenSize.Y / 2));
            card2 = new LevelUpCard(new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2));
            card3 = new LevelUpCard(new Vector2(GameWorld.GetScreenSize.X / 1.5f, GameWorld.GetScreenSize.Y / 2));
            cardArray = new LevelUpCard[3] { card1, card2, card3 };

            LoadContent(); 
        }
        #endregion

        #region Methods
        public override void LoadContent()
        {
            // backgrounds and overlays
            background = content.Load<Texture2D>("lvl");
            gameOverTexture = content.Load<Texture2D>("Menus\\GameOverScreen");
            pausedTexture = content.Load<Texture2D>("Menus\\paused");

            // load background music 
            backgroundMusic = content.Load<Song>("Music\\Time For Action_demo");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;

            //loads a font
            font = content.Load<SpriteFont>("Fonts\\textFont");

            //loads the content for all gameObject
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(content);
            }

            //loads the content for all the buttons
            foreach (Button button in pausedButtons)
            {
                button.LoadContent(content);
            }

            //loads the content for all components
            foreach (Component component in gameOverComponents)
            {
                component.LoadContent(content);
            }

            //loads the content for all LevelUpCards
            foreach (LevelUpCard card in cardArray)
            {
                card.LoadContent(content);
            }
        }
        /// <summary>
        /// updates the gamestate each frame
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //checks if the game is not paused, if the player did not just level up and it isnt gameover
            if (!paused && !Player.LeveledUp && !gameOver)
            {
                CalculateScore(); 
                
                //access the pause menu and pause the music
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    paused = true;
                    MediaPlayer.Pause(); 
                }

                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(gameTime);
                }
                //checks if there is a collision of gameobjects each frame
                foreach (GameObject gameObject in gameObjects)
                { 
                    foreach (GameObject other in gameObjects)
                    {
                        if (gameObject.IsColliding(other))
                        {
                            //if there is a collision it runs the collision method
                            gameObject.OnCollision(other);
                        }
                    }
                }

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

                //if the player is dead, gameover
                if(player.Health < 0)
                {
                    gameOver = true;
                    GameWorld.soundEffects[16].CreateInstance().Play();
                }

                //if the players current exp exceeds the max value, the player levels up
                if(player.Exp >= player.MaxExp)
                {

                    //a random card is chosen
                    GameWorld.soundEffects[7].CreateInstance().Play();
                    foreach (LevelUpCard card in cardArray)

                    {
                        card.RandomCard(content);
                    }
                    //card 2 cannot be the same as card 1
                    while (card1.GetCardIndex == card2.GetCardIndex)
                    {
                        card2.RandomCard(content);
                    }
                    //card 3 cannot be the same as either 1 or 2
                    while (card1.GetCardIndex == card3.GetCardIndex || card2.GetCardIndex == card3.GetCardIndex)
                    {
                        card3.RandomCard(content);
                    }
                }
            }
            //if the game is paused show the pause
            else if(paused)
            {
                //update the buttons so they can be interacted with, even when paused
                foreach (Button button in pausedButtons)
                {
                    button.Update(gameTime);
                }
                //resumes game and music
                if (resumeGameButton.isClicked)
                {
                    GameWorld.soundEffects[13].CreateInstance().Play();
                    resumeGameButton.isClicked = false;
                    MediaPlayer.Resume();
                    paused = false;
                }
                //back to the main menu, restarts the menu music
                if (backToMenuButton.isClicked)
                {
                    GameWorld.soundEffects[14].CreateInstance().Play();
                    backToMenuButton.isClicked = false;
                    game.ChangeState(GameWorld.GetMenuState);
                    MediaPlayer.Pause();
                    playPosition = MediaPlayer.PlayPosition; 
                    MenuState.RestartMenuMusic(); 
                }
                //stops music, closes application
                if (quitGameButton.isClicked)
                {
                    GameWorld.soundEffects[14].CreateInstance().Play();
                    MediaPlayer.Stop(); 
                    quitGameButton.isClicked = false;
                    game.Exit();
                }
            }
            else if (gameOver)
            {
                //updates each component
                foreach (Component component in gameOverComponents)
                {
                    component.Update(gameTime);
                }

                //sets the string to empty so the player can enter their name
                string name = ""; 

                //gets the input of the textbox
                if (enterNameTextbox.isActive)
                {
                    name = enterNameTextbox.GetTextEntered; 
                }
                //saves the value of the input field to a file, with the score
                if (saveScoreButton.isClicked)
                {

                    GameWorld.soundEffects[13].CreateInstance().Play();


                    File.AppendAllText("./scores.txt", name + " " + score + "\n");

                    saveScoreButton.isClicked = false;
                    GameWorld.HandleHighscoreState = new HighscoreState(content, graphicsDevice, game);
                    game.ChangeState(GameWorld.HandleHighscoreState);

                    MediaPlayer.Stop(); 
                    MenuState.RestartMenuMusic();
                }
            }
            //if the player leveled up
            else if (Player.LeveledUp)
            {
                //update cards even if the game is paused
                foreach (LevelUpCard card in cardArray)
                {
                    card.Update(gameTime);
                }

                //initialises the upgrade of the card that is chosen below, either 1, 2 or 3
                if (card1.isClicked)
                {
                    GameWorld.soundEffects[13].CreateInstance().Play();
                    card1.isClicked = false;

                    Player.LeveledUp = false;
                    player.InitializeUpgrade(card1.GetCardIndex);
                }
                if (card2.isClicked)
                {
                    GameWorld.soundEffects[13].CreateInstance().Play();
                    card2.isClicked = false;

                    Player.LeveledUp = false;
                    player.InitializeUpgrade(card2.GetCardIndex);
                }
                if (card3.isClicked)
                {
                    GameWorld.soundEffects[13].CreateInstance().Play();
                    card3.isClicked = false;

                    Player.LeveledUp = false;
                    player.InitializeUpgrade(card3.GetCardIndex);
                }
            }
        }

        /// <summary>
        /// Method for starting the music from the begining
        /// </summary>
        public static void RestartGameMusic()
        {
            MediaPlayer.Play(backgroundMusic); 
        }
        /// <summary>
        /// method for getting the timestamp where the music was paused, so it can resume at the same time
        /// </summary>
        public static void UnpauseGameMusic()
        {
            MediaPlayer.Play(backgroundMusic, playPosition); 
        }
        /// <summary>
        /// method for spawning an enemy, based on chance
        /// </summary>
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
                    throw new ArgumentOutOfRangeException("Unknown value");
            }
        }
        /// <summary>
        /// Method for spawning a wave of enemies of different sizes + the size it increases every time
        /// </summary>
        private void SpawnWave()
        {
            // random number of enemies in wave 
            int numberOfEnemies = random.Next(5, 20) + increaseWaveSize;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy(); 
            }
        }
        /// <summary>
        /// method for adding gameobjects to a tempoary list, slated for removal
        /// </summary>
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
        /// <summary>
        /// method for adding gameObjects to a tempoary list
        /// </summary>
        /// <param name="gameObject"></param>
        public static void InstantiateGameObject(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        /// <summary>
        /// method which handles adding the gameobjects from the tempoary list  to the GameState
        /// </summary>
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
            //clears the list
            gameObjectsToAdd.Clear();
        }
        /// <summary>
        /// method for adding 1 to the players kill counter
        /// </summary>
        public static void CountEnemyKill()
        {
            kills += 1; 
        }
        /// <summary>
        /// method for calculating the score based on both kills and time
        /// </summary>
        public static void CalculateScore()
        {
            score = kills * globalGameTimer2; 
        }

        /// <summary>
        /// Method for drawing sprites and text to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //defines the start when we begin to draw and chooses the front to back layer sorting
            spriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            //background
            spriteBatch.Draw(background, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 2), null, Color.White, 0f, new Vector2(background.Width/2, background.Height/2), 3f, SpriteEffects.None, 0.01f);

            //origin of a string
            float gGTToriginX = font.MeasureString(globalGameTimerText).X / 2;
            float gGTToriginY = font.MeasureString(globalGameTimerText).Y / 2;

            //converts timer to a string so we can draw it
            string globalGameTimer3 = globalGameTimer2.ToString();

            float gGToriginX = font.MeasureString(globalGameTimer3).X / 2;
            float gGToriginY = font.MeasureString(globalGameTimer3).Y / 2;

            //draw globel timer
            spriteBatch.DrawString(font, globalGameTimerText, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 15f), timeTextColor, 0f, new Vector2(gGTToriginX, gGTToriginY), 3.5f, SpriteEffects.None, 0.3f);
            spriteBatch.DrawString(font, globalGameTimer3, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 15f + 60), timeTextColor, 0f, new Vector2(gGToriginX, gGToriginY), 3.5f, SpriteEffects.None, 0.3f);

            //origins and a timer
            float wTToriginX = font.MeasureString(waveTimerText).X / 2;
            float wTToriginY = font.MeasureString(waveTimerText).Y / 2;
            string waveTimer = ((int)(timeBetweenEnemyWave - timeSinceEnemyWave)).ToString();
            float wToriginX = font.MeasureString(waveTimer).X / 2;
            float wToriginY = font.MeasureString(waveTimer).Y / 2;
            
            //draw the wave timer and text
            spriteBatch.DrawString(font, waveTimerText, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 1.12f), timeTextColor, 0f, new Vector2(wTToriginX, wTToriginY), 3.5f, SpriteEffects.None, 0.3f);
            spriteBatch.DrawString(font, waveTimer, new Vector2(GameWorld.GetScreenSize.X / 2, GameWorld.GetScreenSize.Y / 1.12f + 60), timeTextColor, 0f, new Vector2(wToriginX, wToriginY), 3.5f, SpriteEffects.None, 0.3f);

            //draw each gameobject
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
            //draw pause menu
            if (paused)
            {
                spriteBatch.Draw(pausedTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.95f); 
                
                foreach (Button button in pausedButtons)
                {
                    button.Draw(gameTime, spriteBatch);
                }
            }
            //draw gameover
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
            //draw levelup cards
            if (Player.LeveledUp)
            {
                foreach (LevelUpCard card in cardArray)
                {
                    card.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }
        #endregion
    }
}
