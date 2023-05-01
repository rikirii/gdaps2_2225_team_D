using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeamD_bullet_hell.Bullets;
using TeamD_bullet_hell.ButtonsManager;
using TeamD_bullet_hell.GameStates.GamePlay;

namespace TeamD_bullet_hell.GameStates
{
    /// <summary>
    /// THis manager manages ALL game states. 
    /// </summary>
    internal class StateManager
    {
        //track screen resolution
        internal int windowWidth;
        internal int windowHeight;

        //reference to game1's graphics device manager
        internal GraphicsDeviceManager _graphics;

        //Track current Gamestate
        internal GameState currentGameState;

        //track previous keyboard state
        private KeyboardState prevKBState;

        //pause coolDown
        private bool pauseCD;

        //Managers
        private ButtonManager buttonMgr;
        private Gameplay gameplay;
        private ScreenManager screenMgr;

        //Dictionary for storing assets
        private Dictionary<GameState, Texture2D> wallpapers;
        private Dictionary<FontType, SpriteFont> fontsCollection;
        private Dictionary<Entity, Texture2D> spriteCollection;
        private Dictionary<ButtonAssets, Texture2D> buttonAssets;

        private List<Texture2D> backGroundList;

        /// <summary>
        /// ????
        /// </summary>
        public List<Texture2D> BackGroundList
        {
            get { return backGroundList; }
        }



        /// <summary>
        /// Track and Update Current Game State
        /// </summary>
        public GameState CurrentGameState
        {
            get { return currentGameState; }
            set { currentGameState = value; }
        }


        /// <summary>
        /// Store the previous game state for restarting purposes
        /// </summary>
        public GameState PreviousGameState
        {
            get
            {
                return gameplay.PreviousGameState;
            }
            set
            {
                gameplay.PreviousGameState = value;
            }
            
        }

        /// <summary>
        /// store the next gamestate (mainly because of instructions before gameplay)
        /// </summary>
        public GameState NextGameState
        {
            get;set;
        }

        /// <summary>
        /// Get a reference of Button Manager from stateMgr
        /// </summary>
        public ButtonManager ButtonMgr
        {
            get { return buttonMgr; }
        }

        /// <summary>
        /// this will track and set is god mode
        /// </summary>
        public bool IsGodMode
        {
            get
            {
                return gameplay.IsGodMode;
            }
            set
            {
                gameplay.IsGodMode = value;
            }
        }


        /// <summary>
        /// Property to manage when to resume gameplay
        /// If pause is true, this is false
        /// if this is true, pause is false
        /// </summary>
        public bool ToResume
        {
            get
            {
                return !gameplay.IsPause;
            }
            set
            {
                this.gameplay.IsPause = !value; 
            }
        }

        /// <summary>
        /// pass level # to gameplay's level property for bulletMgr
        /// </summary>
        public int Level
        {
            set
            {
                this.gameplay.Level = value;
            }
        }

        /// <summary>
        /// reference to screen manager for rescaling (temp, maybe)-RY
        /// </summary>
        public ScreenManager ScreenMgr
        { get { return this.screenMgr; } } 

        /// <summary>
        /// directs all states.
        /// </summary>
        /// <param name="graphics">game1 graphics device manager</param>
        /// <param name="screenWidth">screen's width</param>
        /// <param name="screenHeight">screen's height</param>
        /// <param name="wallpapers">Dictionary (collection) of all wallpapers for states/screen</param>
        /// <param name="fonts">Dictionary(collection) of all fonts</param>
        /// <param name="spriteCollection">Dictionary(collection) of all sprite's texture</param>
        /// <param name="buttonAssets">Dictionary(collection) of all button's parts/assets</param>
        public StateManager(GraphicsDeviceManager graphics, int screenWidth, int screenHeight, 
                            Dictionary<GameState, Texture2D> wallpapers, Dictionary<FontType, SpriteFont> fonts, Dictionary<Entity, Texture2D> spriteCollection, Dictionary<ButtonAssets, Texture2D> buttonAssets,
                            List<Texture2D> backGroundList)
        {
            this._graphics = graphics;
            this.windowWidth = screenWidth;
            this.windowHeight = screenHeight;

            this.wallpapers = wallpapers;
            this.fontsCollection = fonts;
            this.spriteCollection= spriteCollection;
            this.buttonAssets = buttonAssets;

            this.backGroundList = backGroundList;

            //set current gamestate to menu
            this.currentGameState = GameState.Menu;

            this.screenMgr = new ScreenManager(this.windowWidth, this.windowHeight);

            this.buttonMgr = new ButtonManager(_graphics, this.windowWidth, this.windowHeight, buttonAssets[ButtonAssets.Outline], buttonAssets[ButtonAssets.BackButton], fonts, this.screenMgr );
            this.gameplay = new Gameplay(_graphics, windowWidth, windowHeight, wallpapers, fontsCollection, spriteCollection, this.screenMgr, this.backGroundList);


            this.gameplay.ScreenMgr = this.screenMgr;
            this.buttonMgr.ScreenMgr = this.screenMgr;

            

        }

        /// <summary>
        /// allow other manager's to update their current game state variable
        /// </summary>
        internal void UpdateCurrentState()
        {
            gameplay.CurrentGameState = this.currentGameState;
        }


        /// <summary>
        /// Statemanger's update.
        /// where all other's manager's logic are managed
        /// </summary>
        /// <param name="gameTime">game1 gameTime</param>
        public void Update(GameTime gameTime)
        {
            UpdateCurrentState();

            //Switch that determines which button to update
            switch (currentGameState)
            {
                case GameState.Menu:

                    
                    gameplay.resetCounter = false;

                    if (gameplay.UserUnderstand)
                    {
                        gameplay.UserUnderstand = false;
                        gameplay.Reset();
                    }

                    buttonMgr.Update(gameTime, currentGameState);


                    break;


                case GameState.Instruction:
                    gameplay.Update(gameTime);

                    buttonMgr.Update(gameTime, currentGameState);

                    if ( gameplay.UserUnderstand)
                    {
                        this.currentGameState = this.NextGameState;
                    }

                    break;

                case GameState.Levels:

                    gameplay.Update(gameTime);
                    buttonMgr.Update(gameTime, currentGameState);
                    


                    break;



                case GameState.Infinity:
                    gameplay.Update(gameTime);

                    this.currentGameState = gameplay.CurrentGameState;

                    break;


                case GameState.Gameplay:
                    gameplay.Update(gameTime);

                    if (gameplay.GameOver)
                    {
                        if (gameplay.CurrentGameState == GameState.Win)
                        {
                            this.currentGameState = GameState.Win;
                        }
                        else
                        {
                            this.currentGameState = GameState.GameOver;
                        }
                        
                    }

                    //buttonMgr.Update(gameTime, currentGameState);

                    if (gameplay.IsPause)
                    {
                        this.currentGameState = GameState.Pause;
                        this.pauseCD = true;
                    }
                    break;


                case GameState.LeaderBoard:

                    buttonMgr.Update(gameTime, currentGameState);

                    break;



                case GameState.Pause:

                    gameplay.Update(gameTime);
                    buttonMgr.Update(gameTime, currentGameState);


                    if (buttonMgr.Restart)
                    {
                        gameplay.Reset();

                        if (this.currentGameState != GameState.Menu)
                        {
                            this.currentGameState = gameplay.PreviousGameState;

                        }
                        this.buttonMgr.Restart = !this.buttonMgr.Restart;  
                    }
                    

                    

                    break;


                case GameState.GameOver:


                    if (!gameplay.ResetCounter)
                    {
                        //gameplay.ResetCounter = true;
                        gameplay.Reset();
                        gameplay.UserUnderstand = false;
                        
                        
                    }
                    
                    buttonMgr.Update(gameTime, currentGameState);

                    break;

                case GameState.Win:

                    if (!gameplay.ResetCounter)
                    {
                        //gameplay.ResetCounter = true;
                        gameplay.Reset();
                        gameplay.UserUnderstand = false;


                    }

                    buttonMgr.Update(gameTime, currentGameState);

                    break;


                case GameState.Setting:

                    buttonMgr.Update(gameTime, currentGameState);
                    break;
            }
        }

        /// <summary>
        /// State manager's draw
        /// Where all other manger's draw are managed
        /// </summary>
        /// <param name="spriteBatch">game1 spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Switch taht determines which button to update
            switch (currentGameState)
            {
                case GameState.Menu:

                    spriteBatch.Draw(wallpapers[GameState.Menu], new Rectangle(0, 0, this.windowWidth,
                                        this.windowHeight), Color.White);


                    buttonMgr.Draw(spriteBatch);

                    break;


                case GameState.Instruction:
                    gameplay.Draw(spriteBatch);
                    buttonMgr.Draw(spriteBatch);
                    break;


                case GameState.Levels:

                    gameplay.Draw(spriteBatch);

                    buttonMgr.Draw(spriteBatch);

                    break;


                case GameState.Infinity:

                    //gameplay.Draw(spriteBatch);

                    //buttonMgr.Draw(spriteBatch);


                    break;

                case GameState.Gameplay:

                    gameplay.Draw(spriteBatch);

                    //buttonMgr.Draw(spriteBatch);


                    break;


                case GameState.LeaderBoard:

                    buttonMgr.Draw(spriteBatch);

                    break;



                case GameState.Pause:
                    gameplay.Draw(spriteBatch);                   

                    spriteBatch.DrawString(fontsCollection[FontType.Title], "Pause Menu", new Vector2(850, 214), Color.Black);

                    buttonMgr.Draw(spriteBatch);
                    break;


                case GameState.GameOver:

                    gameplay.Draw(spriteBatch);

                    buttonMgr.Draw(spriteBatch);

                    break;



                case GameState.Win:

                    gameplay.Draw(spriteBatch);
                    buttonMgr.Draw(spriteBatch);


                    break;




                case GameState.Setting:

                    spriteBatch.Draw(wallpapers[GameState.Menu], new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

                    buttonMgr.Draw(spriteBatch);

                    break;
            }
        }

    }
}
