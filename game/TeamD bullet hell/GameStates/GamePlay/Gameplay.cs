using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TeamD_bullet_hell.Bullets;

namespace TeamD_bullet_hell.GameStates.GamePlay
{
    internal class Gameplay : IScreens
    {
        internal GameState currentGameState;
        private GameState previousGameState;

        private bool gameOver;

        //input fields
        private KeyboardState prevKBState;

        //background assets
        internal Dictionary<GameState, Texture2D> wallpapers;
        internal Dictionary<FontType, SpriteFont> fonts;
        internal Dictionary<Entity, Texture2D> spriteCollection;

        //screen size
        internal int windowWidth;
        internal int windowHeight;

        //graphicsdevicemanager ref from game1
        internal GraphicsDeviceManager _graphics;

        //Entities
        internal Player player;

        //bullet manager. 
        private BulletManager bulletMgr;

        //Track the score based on time survived
        private int scoreCounter ;
        private int printForScore;

        // Declare a boolean variable to keep track of whether god mode is enabled or disabled
        private bool godModeEnabled;

        //Added a counter to prevent constant reset call in game over state
        internal bool resetCounter;

        //pause counter or variable
        private bool isPause;


        //instruction screen
        private bool userUnderstand;
        private bool newStart;

        private List<Texture2D> backGroundList;
        private float currentGameTime = 0;
        private int frameNumber = 0;

        /// <summary>
        /// to track whether to pull up the instruction screen or not
        /// </summary>
        public bool UserUnderstand
        {
            get { return userUnderstand; }
            set 
            { 
                userUnderstand = value;
            }
        }

        /// <summary>
        /// Update current game state in gameplay object
        /// </summary>
        internal GameState CurrentGameState
        {
            get
            {
                return currentGameState;
            }
            set
            {
                currentGameState = value;
            }
        }

        /// <summary>
        /// track the previous gaemstate for the retry button
        /// </summary>
        public GameState PreviousGameState
        {
            get { return previousGameState; }
            
        }

        /// <summary>
        /// idk if this is needed - RY
        /// track previous keyboardstate
        /// </summary>
        public KeyboardState PreviousKB
        {
            get
            {
                return prevKBState;
            }
        }

        /// <summary>
        /// track if gameover for other managers
        /// </summary>
        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }

        /// <summary>
        /// track pause status
        /// </summary>
        public bool IsPause
        {
            get
            {
                return this.isPause;
            }
            set
            {
                this.isPause = value;
            }
        }

        /// <summary>
        /// Track isgodmode status
        /// 
        /// </summary>
        public bool IsGodMode
        {
            get { return godModeEnabled; }
            set
            {
                godModeEnabled = value;
            }
        }

        /// <summary>
        /// IDK IF THIS WILL BE USED - RY
        /// get a screenmanager reference
        /// </summary>
        public ScreenManager ScreenMgr
        {
            get;set;
            
        }

        /// <summary>
        /// track when to reset and when not to
        /// </summary>
        public bool ResetCounter
        {
            get { return resetCounter; }
            set { resetCounter = value; }
        }

        /// <summary>
        /// Which level file to use for level mode
        /// </summary>
        public int Level
        {
            get;set;
        }

        /// <summary>
        /// constructor for setting up any gameplays related
        /// </summary>
        /// <param name="graphics">graphics device manager from game1</param>
        /// <param name="windowWidth">Screen width</param>
        /// <param name="windowHeight">screen height</param>
        /// <param name="wallpapers">Dictionary (collection) of wallpapers for states</param>
        /// <param name="fonts">dictionary (collection) of all fonts</param>
        /// <param name="assets">dictionary (collection) of all assets</param>
        public Gameplay(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Dictionary<GameState, Texture2D> wallpapers, Dictionary<FontType, SpriteFont> fonts, Dictionary<Entity, Texture2D> spriteCollection, 
                            ScreenManager screenMgr, List<Texture2D> backGroundList)
        {
            this._graphics = graphics;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.wallpapers = wallpapers;
            this.fonts = fonts;
            this.spriteCollection = spriteCollection;

            this.userUnderstand = false;


            this.ScreenMgr = screenMgr;

            this.godModeEnabled = false;
            this.gameOver = false;
            this.resetCounter = false;
            this.isPause = false;

            this.bulletMgr = new BulletManager(_graphics, windowWidth, windowHeight, spriteCollection);

            this.bulletMgr.ScreenMgr = this.ScreenMgr;


            //create player object
            player = new Player(spriteCollection[Entity.Player], new Rectangle(windowWidth / 2, windowHeight / 2, (100), (100)), windowWidth, windowHeight);

            this.backGroundList = backGroundList;

            //first time startup, reset() will prepare everything needed.
            this.Reset();

            
        }
        /// <summary>
        /// this method update the backgorud series 
        /// </summary>
        /// <returns></returns>
        public void UpdateBackGround(GameTime gameTime)
        {
            currentGameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds);
            //loop through the background 
            if(currentGameTime >=0.03)
            {
                currentGameTime = 0;
                frameNumber++;
                //when it reach the end 
                if(frameNumber>=200)
                {
                    frameNumber = 0;
                }
            }
        }

        /// <summary>
        /// resets the game for next round
        /// </summary>
        public void Reset()
        {
            //reset player position

            player.X = windowWidth /2;
            player.Y = windowHeight - player.Position.Height;

            //reset the bullet 
            //bulletMgr.currentGameState = GameState.Menu;
            bulletMgr.Reset(spriteCollection[Entity.Bullet]);
            

            this.gameOver = false;
            this.player.Lives = 3;
            this.isPause = false;

            
            scoreCounter = 0;

            
        }


        /// <summary>
        /// Gameplay manager's update
        /// </summary>
        /// <param name="gameTime">game1 gametime</param>
        public void Update(GameTime gameTime)
        {
            //updates currentgame state for bullet manager
            //bulletMgr.currentGameState = this.currentGameState;


            //Switch taht determines which button to update
            switch (currentGameState)
            {
                case GameState.Instruction:

                    KeyboardState currentKbState = Keyboard.GetState();

                    if (currentKbState.IsKeyDown(Keys.Enter) && prevKBState.IsKeyUp(Keys.Enter) )
                    {
                        userUnderstand = true;
                    }

                    this.prevKBState = currentKbState;

                    break;


                case GameState.Levels:

                    
                    bulletMgr.currentGameState = GameState.Levels;
                    


                    break;


                case GameState.Infinity:

                    
                    bulletMgr.currentGameState = GameState.Infinity;
                    this.currentGameState = GameState.Gameplay;


                    

                    break;


                case GameState.Gameplay:
                    scoreCounter += (int)((gameTime.ElapsedGameTime.TotalSeconds) * 100);
                    if (userUnderstand)
                    {
                        KeyboardState kbState = Keyboard.GetState();

                        if (kbState.IsKeyDown(Keys.Escape) && prevKBState.IsKeyUp(Keys.Escape))
                        {
                            previousGameState = this.currentGameState;
                            this.isPause = true;
                            this.currentGameState = GameState.Pause;

                        }
                        this.prevKBState = kbState;

                       

                        if (!isPause)
                        {
                            player.Update(gameTime);

                            UpdateBackGround(gameTime);
                            //test here only open bullet level 1

                            bulletMgr.Level = this.Level;

                            bulletMgr.Update(gameTime);

                            //Collision Logic

                            if (!this.godModeEnabled)
                            {
                                //only do the level 1 here
                                foreach (Bullet bullet in bulletMgr.CurrentBulletList)
                                {
                                    if (player.Intersects(bullet))
                                    {
                                        player.Lives -= 1;

                                    }
                                }
                            }

                            if (player.Lives <= 0)
                            {
                                printForScore = scoreCounter;
                                previousGameState = this.currentGameState;
                                gameOver = !gameOver;
                                currentGameState = GameState.GameOver;

                            }

                            if (bulletMgr.ChangeState)
                            {
                                /////////////////////////////////////////////////////////////////////////////////////////////////////
                                previousGameState = this.currentGameState;
                                //this.isPause = true;
                                gameOver = !gameOver;
                                this.currentGameState = GameState.Win;
                                bulletMgr.ChangeState = false;

                            }


                        }
                    }


                    break;


                case GameState.Pause:

                    
                    break;


                case GameState.GameOver:


                    break;


                case GameState.Win:
                    break;
            }



        }

        /// <summary>
        /// gameplay manager's draw.
        /// Directs all gameplay related drawings
        /// </summary>
        /// <param name="spriteBatch">gaem1 spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Switch taht determines which button to update
            switch (currentGameState)
            {
                case GameState.Instruction:

                    spriteBatch.Draw(wallpapers[GameState.Instruction], new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
                    break;

                case GameState.Levels:

                    spriteBatch.DrawString(fonts[FontType.Title], "Levels", new Vector2(100, 100), Color.White);

                    break;


                case GameState.Gameplay:
                    //test the animateing backgroiund

                    spriteBatch.Draw(backGroundList[frameNumber], new Rectangle(0, 0, windowWidth, windowHeight), Color.White);


                    //spriteBatch.Draw(wallpapers[GameState.Gameplay], new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

                    spriteBatch.DrawString(fonts[FontType.Button], string.Format("Lives: {0}", player.Lives), new Vector2(10, 100), Color.White);

                    player.Draw(spriteBatch);

                    //test here only open bullet level 1
                    bulletMgr.Draw(spriteBatch);


                    break;


                case GameState.Pause:
                    


                    break;


                case GameState.GameOver:
                    spriteBatch.Draw(wallpapers[GameState.GameOver], new Rectangle(0, 0, windowWidth, windowHeight), Color.White);


                    //important info with this. the drawstring for score is harded coded position.
                    spriteBatch.DrawString(fonts[FontType.Title], "" + printForScore, new Vector2(435, 546), Color.White);

                    


                    break;

                case GameState.Win:
                    spriteBatch.Draw(wallpapers[GameState.Win], new Rectangle(0,0, windowWidth, windowHeight), Color.White);
                    break;
            }



        }
    }
}
