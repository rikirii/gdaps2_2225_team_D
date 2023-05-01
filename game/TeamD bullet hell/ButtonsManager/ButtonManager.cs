using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using TeamD_bullet_hell.GameStates;
using Microsoft.Xna.Framework.Input;

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
        private List<Button> instructionButton;
        private List<Button> leaderBoardButtons;
        private List<Button> pauseButtons;
        private List<Button> gameOverButtons;
        private List<Button> winButtons;
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

        /// <summary>
        /// reference to screen manager for scaling (temp. maybe )-RY
        /// </summary>
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
        /// mainly used for pause menu restart
        /// </summary>
        public bool Restart
        {
            get;set;
        }

        public GameState CurrentGameState
        {
            set
            {
                this.currentGameState = value;
            }
        }

        /// <summary>
        /// track what was the previous state from instruction
        /// </summary>
        public GameState ToPreviousState
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
            this.Restart = false;

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
            CreateInstructionButton(graphics);
            CreateLeaderBoardButton(graphics, buttonOutline);
            CreateGameOverButton(graphics, buttonOutline);
            CreateWinButton(graphics, buttonOutline);
            CreateSettingButtons(graphics, buttonOutline);
            CreatePauseMenuButton(graphics, buttonOutline);


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

            for (int i = 0; i < 5; i++)
            {
                levelsButtons.Add(new Button(graphics.GraphicsDevice,
                                        new Rectangle( 155 + (255 * i + 60* i) , 361, 255, 235),
                                        "Level " + (i + 1),
                                        fonts[FontType.Button],
                                        Color.DimGray,
                                        buttonOutline,
                                        false) );
            }

            

            //add left click event to all buttons in level list
            foreach (Button b in levelsButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        /// <summary>
        /// creates the necessary buttons for create instruction state button
        /// 
        /// ***this method might be deleted temp hold -RY
        /// </summary>
        /// <param name="graphics"></param>
        internal void CreateInstructionButton(GraphicsDeviceManager graphics)
        {
            instructionButton = new List<Button>();

            instructionButton.Add(backButton);

            //add left click event to all buttons in infinity list
            foreach (Button b in instructionButton)
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
            pauseButtons = new List<Button>();

            Button mainMenu = new Button(graphics.GraphicsDevice,
                                new Rectangle((windowWidth / 2) - buttonOutline.Width / 2, 342, buttonOutline.Width, buttonOutline.Height / 3),
                                "Main Menu",
                                fonts[FontType.Button],
                                Color.DarkGray,
                                buttonOutline,
                                false);

            Button restart = new Button(graphics.GraphicsDevice,
                                    new Rectangle(mainMenu.Position.X, mainMenu.Position.Y + (buttonOutline.Height + 10), mainMenu.Position.Width, mainMenu.Position.Height),
                                    "Restart",
                                    fonts[FontType.Button],
                                    Color.DarkGray,
                                    buttonOutline,
                                    false);

            Button resume = new Button(graphics.GraphicsDevice,
                                    new Rectangle(restart.Position.X, restart.Position.Y + (buttonOutline.Height + 10), mainMenu.Position.Width, mainMenu.Position.Height),
                                    "Resume",
                                    fonts[FontType.Button],
                                    Color.DarkCyan,
                                    buttonOutline,
                                    false);

            pauseButtons.Add(mainMenu);
            pauseButtons.Add(restart) ;
            pauseButtons.Add(resume) ;

            foreach (Button b in pauseButtons)
            {
                b.OnLeftButtonClick += this.ButtonLeftClicked;
            }
        }

        /// <summary>
        /// create the buttons necessary for the game over screen
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="buttonOutline">the button outline when mouse hover</param>
        internal void CreateGameOverButton(GraphicsDeviceManager graphics, Texture2D buttonOutline)
        {
            gameOverButtons = new List<Button>();

            Button backButton = new Button(graphics.GraphicsDevice,
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
        /// create the buttons necessary for the win screen
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="buttonOutline">the button outline when mouse hover</param>
        internal void CreateWinButton(GraphicsDeviceManager graphics, Texture2D buttonOutline)
        {
            winButtons = new List<Button>();

            Button backButton = new Button(graphics.GraphicsDevice,
                                new Rectangle((windowWidth / 2) - buttonOutline.Width / 2, (windowHeight - 10) - buttonOutline.Height, buttonOutline.Width, buttonOutline.Height / 3),
                                "Return to Main Menu",
                                fonts[FontType.Button],
                                Color.Black,
                                buttonOutline,
                                false);

            Button reTry = new Button(graphics.GraphicsDevice,
                                    new Rectangle(backButton.Position.X, backButton.Position.Y - (buttonOutline.Height + 10), backButton.Position.Width, backButton.Position.Height),
                                    "Retry",
                                    fonts[FontType.Button],
                                    Color.Black,
                                    buttonOutline,
                                    false);


            winButtons.Add(backButton);
            winButtons.Add(reTry);

            foreach (Button b in winButtons)
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


                case GameState.Instruction:
                    foreach (Button buttons in instructionButton)
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

                    //foreach (Button buttons in instructionButton)
                    //{
                    //    buttons.Update(gameTime);
                    //}

                    break;


                case GameState.LeaderBoard:
                    foreach (Button buttons in leaderBoardButtons)
                    {
                        buttons.Update(gameTime);
                    }
                    break;


               
                case GameState.Pause:

                    foreach (Button buttons in pauseButtons)
                    {
                        buttons.Update(gameTime);
                    }
                    break;


                case GameState.GameOver:

                    foreach (Button buttons in gameOverButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;


                case GameState.Win:

                    foreach (Button buttons in winButtons)
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


                case GameState.Instruction:

                    foreach (Button buttons in instructionButton)
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


                    //foreach (Button buttons in instructionButton)
                    //{
                    //    buttons.Draw(spriteBatch);
                    //}

                    break;


                case GameState.LeaderBoard:

                    foreach (Button buttons in leaderBoardButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }
                    break;


                case GameState.Pause:

                    foreach (Button buttons in pauseButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }
                    break;


                case GameState.GameOver:

                    foreach (Button buttons in gameOverButtons)
                    {
                        buttons.Draw(spriteBatch);
                    }

                    break;


                case GameState.Win:

                    foreach (Button buttons in winButtons)
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
        //levelButtons = {backbutton, lvl1, lvl2, lvl3, lvl4, lvl5]
        //infinityButtons = [backbutton]
        //leaderBoardButtons = [backbutton]
        //gameOverButtons = [return to main menu, retry]
        //winButtons = [return to main menu, retry]
        //settingButtons = [godmode, back]
        //pauseButtons = [return to menu, restart, resume]

        /// <summary>
        /// switch current game state when button are clicked.
        /// </summary>
        public void ButtonLeftClicked()
        {
            switch (currentGameState)
            {
                case GameState.Menu:
                    //select lvl click
                    if (menuButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Levels;
                        this.currentGameState = GameState.Levels;
                        stateMgr.PreviousGameState = GameState.Levels;
                        this.ToPreviousState = GameState.Levels;
                    }
                    //infinity click
                    if (menuButtons[1].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
                        stateMgr.NextGameState = GameState.Infinity;
                        stateMgr.PreviousGameState = GameState.Menu;
                        this.ToPreviousState = GameState.Menu;
                    }
                    //leaderboard clicked
                    if (menuButtons[2].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.LeaderBoard;
                        this.currentGameState = GameState.LeaderBoard;
                    }
                    //setting clicked
                    if (menuButtons[3].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Setting;
                        this.currentGameState = GameState.Setting;
                    }
                    break;


                case GameState.Levels:
                    //backbutton clicked
                    if (levelsButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }

                    //level 1
                    if (levelsButtons[1].IsClicked)
                    {
                        
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
                        stateMgr.NextGameState = GameState.Gameplay;
                        stateMgr.Level = 1;
                    }

                    //level 2
                    if (levelsButtons[2].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
                        stateMgr.NextGameState = GameState.Gameplay;
                        stateMgr.Level = 2;
                    }

                    //level 3
                    if (levelsButtons[3].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
                        stateMgr.NextGameState = GameState.Gameplay;
                        stateMgr.Level = 3;
                    }

                    //level 4
                    if (levelsButtons[4].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
                        stateMgr.NextGameState = GameState.Gameplay;
                        stateMgr.Level = 4;
                    }

                    //level 5
                    if (levelsButtons[5].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
                        stateMgr.NextGameState = GameState.Gameplay;
                        stateMgr.Level = 5;
                    }

                    break;

                    ///
                    ///Instruction
                case GameState.Instruction:

                    ///=======this is back button=====
                    //This checks if to previous state (for back button) is menu and clicked.
                    //Thus, this back button will go to main menu. INFINITY HAS NO SELECT SCREEN LIKE LEVELS
                    if (instructionButton[0].IsClicked && ToPreviousState == GameState.Menu )
                    {
                        //infinty mode has no select screen. Thus, menu
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }

                    //to preivous state is gamestate.levels as it has a select level screen. Thus, back button
                    //in instruction will bring user back to level select for this case.
                    else if (instructionButton[0].IsClicked && ToPreviousState == GameState.Levels)
                    {
                        stateMgr.CurrentGameState = GameState.Levels;
                        this.currentGameState = GameState.Levels;
                    }
                    break;


                case GameState.LeaderBoard:
                    //backbutton clicked
                    if (leaderBoardButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }
                    break;


                case GameState.Pause:

                    //main menu clicked
                    if (pauseButtons[0].IsClicked)
                    {
                        
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState= GameState.Menu;
                        this.Restart = !this.Restart;
                    }

                    //if restart
                    if (pauseButtons[1].IsClicked)
                    {
                        this.Restart = !this.Restart;
                        stateMgr.CurrentGameState = stateMgr.PreviousGameState;
                        this.currentGameState = stateMgr.PreviousGameState;
                        this.stateMgr.ToResume = true;
                    }

                    //if resume back to gameplay
                    if (pauseButtons[2].IsClicked)
                    {
                        stateMgr.currentGameState = stateMgr.PreviousGameState;
                        this.currentGameState = stateMgr.PreviousGameState;
                        stateMgr.ToResume = true;
                    }
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
                        this.Restart = !this.Restart;
                        stateMgr.NextGameState = stateMgr.PreviousGameState;
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction ;
                    }
                    break;



                case GameState.Win:

                    //go back to main menu
                    if (winButtons[0].IsClicked)
                    {
                        stateMgr.CurrentGameState = GameState.Menu;
                        this.currentGameState = GameState.Menu;
                    }

                    //retry the last played level
                    if (winButtons[1].IsClicked)
                    {
                        this.Restart = !this.Restart;
                        stateMgr.NextGameState = stateMgr.PreviousGameState;
                        stateMgr.CurrentGameState = GameState.Instruction;
                        this.currentGameState = GameState.Instruction;
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
