using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeamD_bullet_hell.Bullets;
using TeamD_bullet_hell.ButtonsManager;
using TeamD_bullet_hell.GameStates.GamePlay;
using TeamD_bullet_hell.GameStates.Title;

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

        //Managers
        private TitleScreen mainMenu;
        private ButtonManager buttonMgr;
        private Gameplay gameplay;
        private ScreenManager screenMgr;

        //Dictionary for storing assets
        private Dictionary<GameState, Texture2D> wallpapers;
        private Dictionary<FontType, SpriteFont> fontsCollection;
        private Dictionary<Entity, Texture2D> spriteCollection;
        private Dictionary<ButtonAssets, Texture2D> buttonAssets;


        /// <summary>
        /// Track and Update Current Game State
        /// </summary>
        public GameState CurrentGameState
        {
            get { return currentGameState; }
            set { currentGameState = value; }
        }

        public GameState PreviousGameState
        {
            get
            {
                return gameplay.PreviousGameState;
            }
        }

        /// <summary>
        /// Get a reference of Button Manager
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
                            Dictionary<GameState, Texture2D> wallpapers, Dictionary<FontType, SpriteFont> fonts, Dictionary<Entity, Texture2D> spriteCollection, Dictionary<ButtonAssets, Texture2D> buttonAssets)
        {
            this._graphics = graphics;
            this.windowWidth = screenWidth;
            this.windowHeight = screenHeight;

            this.wallpapers = wallpapers;
            this.fontsCollection = fonts;
            this.spriteCollection= spriteCollection;
            this.buttonAssets = buttonAssets;

            //set current gamestate to menu
            this.currentGameState = GameState.Menu;

            this.screenMgr = new ScreenManager(this.windowWidth, this.windowHeight);

            this.mainMenu = new TitleScreen(wallpapers[GameState.Menu], windowWidth, windowHeight, _graphics);
            this.buttonMgr = new ButtonManager(_graphics, this.windowWidth, this.windowHeight, buttonAssets[ButtonAssets.Outline], buttonAssets[ButtonAssets.BackButton], fonts, this.screenMgr );
            this.gameplay = new Gameplay(_graphics, windowWidth, windowHeight, wallpapers, fontsCollection, spriteCollection, this.screenMgr);


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
                    
                    buttonMgr.Update(gameTime, currentGameState);

                    break;


                case GameState.Levels:

                    buttonMgr.Update(gameTime, currentGameState);

                    break;


                case GameState.Infinity:

                    gameplay.Update(gameTime);

                    if (gameplay.GameOver)
                    {
                        this.currentGameState = GameState.GameOver;
                    }

                    buttonMgr.Update(gameTime, currentGameState);

                    break;


                case GameState.LeaderBoard:

                    buttonMgr.Update(gameTime, currentGameState);

                    break;



                case GameState.Pause:

                    break;


                case GameState.GameOver:


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

                    mainMenu.Draw(spriteBatch);

                    buttonMgr.Draw(spriteBatch);

                    break;


                case GameState.Levels:

                    gameplay.Draw(spriteBatch);

                    buttonMgr.Draw(spriteBatch);

                    break;


                case GameState.Infinity:

                    gameplay.Draw(spriteBatch);

                    buttonMgr.Draw(spriteBatch);


                    break;


                case GameState.LeaderBoard:

                    buttonMgr.Draw(spriteBatch);

                    break;



                case GameState.Pause:

                    break;


                case GameState.GameOver:

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
