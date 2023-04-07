using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using TeamD_bullet_hell.GameStates;

namespace TeamD_bullet_hell.ButtonsManager
{
    internal class ButtonManager
    {
        //window size
        internal int windowWidth;
        internal int windowHeight;

        //reference to the state Manager
        internal StateManager stateMgr;

        //current game state
        internal GameState currentGameState;

        //universal buttons
        internal Button backButton;

        //list of buttons to organize buttons per game state
        private List<Button> menuButtons;
        private List<Button> levelsButtons;
        private List<Button> infinityButtons;
        private List<Button> leaderBoardButtons;
        private List<Button> pauseButtons;
        private List<Button> gameOverButtons;
        private List<Button> settingButtons;

        //collection (dictionary) of fonts to use
        Dictionary<FontType, SpriteFont> fonts;

        private ScreenManager screenMgr;

        /// <summary>
        /// Able to use a reference of State Manager from Game1
        /// </summary>
        public StateManager StateMgr
        {

            set { stateMgr = value; }
        }

        public ScreenManager ScreenMgr
        {
            set
            {
                this.screenMgr = value;
            }
        }

        /// <summary>
        /// track god mode status
        /// </summary>
        public bool IsGodMode
        {
            get;set;
        }



        /// <summary>
        /// Button Manager Constructor
        /// Creates the necessary buttons for the game.
        /// Uses methods to organize the code per game state screen
        /// </summary>
        /// <param name="graphics">game1 _graphics</param>
        /// <param name="buttonOutline">Texture2D button Outline for button when hover</param>
        public ButtonManager(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Texture2D buttonOutline, Texture2D backButtonPNG, Dictionary<FontType, SpriteFont> fonts,  ScreenManager screenMgr)
        {
            //get window height
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            this.screenMgr = screenMgr;

            //create buttons through methods
            CreateMenuButton(graphics, buttonOutline, fonts);
            backButton = new Button(graphics.GraphicsDevice,
                                new Rectangle(10, 10, buttonOutline.Width / 3, buttonOutline.Height / 3),
                                "Back",
                                fonts[FontType.Button],
                                Color.Black,
                                buttonOutline,
                                false);
            CreateSelectLvlButton(graphics, buttonOutline);
            CreateInfinityButton(graphics);
            CreateLeaderBoardButton(graphics, buttonOutline);
            CreateGameOverButton(graphics, buttonOutline);
            CreateSettingButtons(graphics, buttonOutline);

        }

        /// <summary>
        /// This creates the necessary buttons for the main menu screen
        /// </summary>
        /// <param name="graphics">graphicsdevice manager from game1 via contrustor</param>
        /// <param name="buttonOutline">Texture2D outline via constructor</param>
        internal void CreateMenuButton(GraphicsDeviceManager graphics, Texture2D buttonOutline, Dictionary<FontType, SpriteFont> fonts)
        {
            menuButtons = new List<Button>();
            this.fonts = fonts;

            //380
            Button selectLevel = new Button(graphics.GraphicsDevice,
                                            new Rectangle((1038), (409), ( buttonOutline.Width) , ( buttonOutline.Height  - 15) ),
                                            "Select Level",
                                            fonts[FontType.Button],
                                            Color.Black,
                                            buttonOutline,
                                            false);
            Button infinity = new Button(graphics.GraphicsDevice,
                                        new Rectangle(selectLevel.X, selectLevel.Position.Height + selectLevel.Position.Y + 5, buttonOutline.Width, (buttonOutline.Height ) - 25),
                                        "Infinity Mode", 
                                        fonts[FontType.Button],
                                        Color.Black, 
                                        buttonOutline,
                                        false);
            Button leaderBoard = new Button(graphics.GraphicsDevice, 
                                        new Rectangle(selectLevel.X, infinity.Position.Y + infinity.Position.Height + 5, buttonOutline.Width, (buttonOutline.Height ) - 30),
                                        "Leaderboard",
                                        fonts[FontType.Button],
                                        Color.Black,
                                        buttonOutline,
                                        false);

            Button setting = new Button(graphics.GraphicsDevice,
                                        new Rectangle(selectLevel.X, leaderBoard.Position.Y + leaderBoard.Position.Height + 5, buttonOutline.Width, (buttonOutline.Height) - 30),
                                        "Settings",
                                        fonts[FontType.Button],
                                        Color.Black,
                                        buttonOutline,
                                        false);


            menuButtons.Add(selectLevel);
            menuButtons.Add(infinity);
            menuButtons.Add(leaderBoard);
            menuButtons.Add(setting);

            //add left click event to all buttons in menu list
            foreach (Button b in menuButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        /// <summary>
        /// Creates the buttons needed for select lvl scnreen
        /// </summary>
        /// <param name="graphics">grpahics device manager</param>
        /// <param name="buttonOutline">The outline for button when mouse is hover</param>
        internal void CreateSelectLvlButton(GraphicsDeviceManager graphics, Texture2D buttonOutline)
        {
            levelsButtons = new List<Button>();

            levelsButtons.Add(backButton);

            //add left click event to all buttons in level list
            foreach (Button b in levelsButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        /// <summary>
        /// creates the necessary buttons for create infinity button
        /// </summary>
        /// <param name="graphics"></param>
        internal void CreateInfinityButton(GraphicsDeviceManager graphics)
        {
            infinityButtons = new List<Button>();

            infinityButtons.Add(backButton);

            //add left click event to all buttons in infinity list
            foreach (Button b in infinityButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        /// <summary>
        /// Creates buttons need for leaderboard screen
        /// </summary>
        /// <param name="graphics">graphics device manager</param>
        /// <param name="buttonOutline">The outline for button when mouse is hover</param>
        internal void CreateLeaderBoardButton(GraphicsDeviceManager graphics, Texture2D buttonOutline)
        {
            leaderBoardButtons = new List<Button>();

            leaderBoardButtons.Add(backButton);

            //add left click event to all buttons in leaderboard list
            foreach (Button b in leaderBoardButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        /// <summary>
        /// create the buttons necessary for pause menu
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="buttonOutline">the button outline when mouse hover</param>
        internal void CreatePauseMenuButton(GraphicsDeviceManager graphics, Texture2D buttonOutline)
        {

        }

        /// <summary>
        /// create the buttons necessary for the game over screen
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="buttonOutline">the button outline when mouse hover</param>
        internal void CreateGameOverButton(GraphicsDeviceManager graphics, Texture2D buttonOutline)
        {
            gameOverButtons = new List<Button>();

            backButton = new Button(graphics.GraphicsDevice,
                                new Rectangle( (windowWidth/2) - buttonOutline.Width/2, (windowHeight - 10) - buttonOutline.Height, buttonOutline.Width, buttonOutline.Height / 3),
                                "Return to Main Menu",
                                fonts[FontType.Button],
                                Color.Black,
                                buttonOutline,
                                false);

            Button reTry = new Button(graphics.GraphicsDevice,
                                    new Rectangle(backButton.Position.X, backButton.Position.Y - (buttonOutline.Height + 10), backButton.Position.Width, backButton.Position.Height),
                                    "Retry",
                                    fonts[FontType.Button],
                                    Color.Gray,
                                    buttonOutline,
                                    false);


            gameOverButtons.Add(backButton);
            gameOverButtons.Add(reTry);

            foreach ( Button b in  gameOverButtons )
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="buttonOutline">the button outline when mouse hover</param>
        public void CreateSettingButtons(GraphicsDeviceManager graphics, Texture2D buttonOutline )
        {
            settingButtons = new List<Button>();

            Button godMode = new Button(graphics.GraphicsDevice,
                                    new Rectangle(this.menuButtons[0].X, this.menuButtons[0].Y, buttonOutline.Width, (buttonOutline.Height) - 15),
                                    "God Mode",
                                    fonts[FontType.Button],
                                    Color.Black,
                                    buttonOutline,
                                    true);

            //a different button for setting screen
            Button back = new Button(graphics.GraphicsDevice,
                                    new Rectangle(godMode.X, godMode.Position.Height + godMode.Position.Y + 5, buttonOutline.Width, (buttonOutline.Height) - 25),
                                    "Return",
                                    fonts[FontType.Button],
                                    Color.Black,
                                    buttonOutline,
                                    false);

            settingButtons.Add(godMode);
            settingButtons.Add(back);

            foreach (Button b in settingButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        


        /// <summary>
        /// ButtonManager update.
        /// </summary>
        /// <param name="gameTime">game1 gameTime</param>
        /// <param name="currentGameState">The current game state for FSMS</param>
        public void Update(GameTime gameTime, GameState currentGameState)
        {
            this.currentGameState = currentGameState;

            //Switch taht determines which button to update
            switch (currentGameState)
            {
                case GameState.Menu:

                    foreach (Button buttons in menuButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;


                case GameState.Levels:
                    foreach (Button buttons in levelsButtons)
                    {
                        buttons.Update(gameTime);
                    }
                    break;


                case GameState.Infinity:

                    foreach (Button buttons in infinityButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;


                case GameState.LeaderBoard:
                    foreach (Button buttons in leaderBoardButtons)
                    {
                        buttons.Update(gameTime);
                    }
                    break;


               
                case GameState.Pause:

                    break;


                case GameState.GameOver:

                    foreach (Button buttons in gameOverButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;

                case GameState.Setting:

                    foreach (Button buttons in settingButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;
            }

        }

        /// <summary>
        /// Button manager's draw
        /// </summary>
        /// <param name="spriteBatch">game1  _spriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Determines what button to draw
            switch (currentGameState)
            {
                case GameState.Menu:

                    foreach (Button buttons in menuButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }

                    break;


                case GameState.Levels:


                    foreach (Button buttons in levelsButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }
                    break;


                case GameState.Infinity:


                    foreach (Button buttons in infinityButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }

                    break;


                case GameState.LeaderBoard:

                    foreach (Button buttons in leaderBoardButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }
                    break;


                case GameState.Pause:

                    break;


                case GameState.GameOver:

                    foreach (Button buttons in gameOverButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }

                    break;

                case GameState.Setting:

                    foreach (Button buttons in settingButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }

                    break;
            }

        }



        //==========================================
        //============ Click handlers=========
        //==========================================

        //***update list when new buttons are added***
        //menuButtons = [selectLvl, infinity, leaderboard, setting]
        //levelButtons = {backbutton]
        //infinityButtons = [backbutton]
        //leaderBoardButtons = [backbutton]
        //gameOverButtons = [return to main menu, retry]
        //settingButtons = [godmode, back]

        /// <summary>
        /// switch current game state when button are clicked.
        /// </summary>
        public void ButtonLeftClicked()
        {
            switch (currentGameState)
            {
                case GameState.Menu:
                    if (menuButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Levels;
                        this.currentGameState = GameState.Levels;
                    }
                    if (menuButtons[1].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Infinity;
                        this.currentGameState = GameState.Infinity;
                    }
                    if (menuButtons[2].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.LeaderBoard;
                        this.currentGameState = GameState.LeaderBoard;
                    }
                    if (menuButtons[3].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Setting;
                        this.currentGameState = GameState.Setting;
                    }
                    break;

                case GameState.Levels:
                    if (levelsButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }
                    break;

                case GameState.Infinity:

                    if (infinityButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }
                    break;


                case GameState.LeaderBoard:
                    if (leaderBoardButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }
                    break;

                case GameState.Pause:
                    break;


                case GameState.GameOver:

                    //go back to main menu
                    if (gameOverButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState  = GameState.Menu;
                    }

                    //retry the last played level
                    if (gameOverButtons[1].IsClicked)
                    {
                        stateMgr.CurrentGameState = stateMgr.PreviousGameState;
                        this.currentGameState = stateMgr.PreviousGameState;
                    }
                    break;


                case GameState.Setting:

                    //godmode button
                    if (settingButtons[0].IsClicked)
                    {
                        stateMgr.IsGodMode = !stateMgr.IsGodMode;
                        if (stateMgr.IsGodMode)
                        {
                            settingButtons[0].IsGodMode = true;
                        }
                        else
                        {
                            settingButtons[0].IsGodMode = false;
                        }
                    }

                    //back button
                    if (settingButtons[1].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }

                    break;

            }
        }
    }
}
