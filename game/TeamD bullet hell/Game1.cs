using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public enum GameMode
{
    Menu,
    Levels,
    Gameplay,
    Infinity,
    Pause,
    LeaderBoard

    
}

namespace TeamD_bullet_hell
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player player;
        private int windowHeight;
        private int windowWidth;

        //sprite and texture fields for the menu
        private SpriteFont arialBold30;
        private Texture2D tempWallpaper;
        private Texture2D leaderBoardImage;
        private Texture2D recOutline;
        private Texture2D backButtonPNG;
        private Texture2D gameBackground;
        private Texture2D playerShip;

        //Rectangles to select the options on the Menu
        //
        //buttons
        Button selectLevel;
        Button infinity;
        Button leaderBoard;
        Button backButton;

        //field that tracks the gameState
        GameMode currentGameState;

        //Getting a mouse state and keyboard state
        MouseState mState;
        MouseState prevMstate;
        KeyboardState KBstate;
        KeyboardState prevKBstate;

        //Button list (I suspect there will be a lot of buttons to draw) -RY
        private List<Button> menuButtons;
        private List<Button> levelsButtons;
        private List<Button> pauseButtons;
        private List<Button> leaderBoardButtons;
        private List<Button> infinityButtons;
        


        //trying out stacks -RY
        //Stack<GameMode> stackGameModes = new Stack<GameMode>();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //store window's width and height
            this.windowWidth = _graphics.GraphicsDevice.Viewport.Width;
            this.windowHeight = _graphics.GraphicsDevice.Viewport.Height;

            //set initial currentGamestate to Menu
            currentGameState = GameMode.Menu;
            //stackGameModes.Push(GameMode.Menu);

            //initialize list of buttons to store many buttons
            menuButtons= new List<Button>();
            levelsButtons= new List<Button>();  
            pauseButtons= new List<Button>();
            leaderBoardButtons = new List<Button>();
            infinityButtons= new List<Button>();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            arialBold30 = Content.Load <SpriteFont>("arial");
            tempWallpaper = Content.Load<Texture2D>("temp");
            gameBackground = Content.Load<Texture2D>("gameBackground");
            leaderBoardImage = Content.Load<Texture2D>("leaderboard");
            recOutline = Content.Load<Texture2D>("Redtangle"); //bad name for this. -RY
            backButtonPNG = Content.Load<Texture2D>("backButton");

            //loading player
            playerShip = Content.Load<Texture2D>("ship");

            player = new Player(playerShip, new Rectangle(0, 0, 100, 100), windowWidth, windowHeight);


            //===========================================================
            //These codes are for menu screen only!!! -RY
            selectLevel = new Button(_graphics.GraphicsDevice, new Rectangle(380, 200, recOutline.Width, (recOutline.Height / 2) - 15), recOutline );
            infinity = new Button(_graphics.GraphicsDevice, new Rectangle(380, 210 + selectLevel.Position.Height, recOutline.Width, (recOutline.Height / 2) - 25), recOutline);
            leaderBoard = new Button(_graphics.GraphicsDevice, new Rectangle(380, infinity.Position.Y + infinity.Position.Height + 5, recOutline.Width, (recOutline.Height / 2) - 30), recOutline);

            //adds menu buttons to menu list
            menuButtons.Add(selectLevel);
            menuButtons.Add(infinity);
            menuButtons.Add(leaderBoard);

            //add left click event to all buttons in menu list
            foreach(Button b in menuButtons)
            {
                b.OnLeftButtonClick += this.LeftButtonClicked;
            }

            //===========================================================

            //universal back button?
            backButton = new Button(_graphics.GraphicsDevice, new Rectangle(10, 10, backButtonPNG.Width, backButtonPNG.Height), recOutline);

            //========LeaderBoard Screen Only!!!!======
            leaderBoardButtons.Add(backButton);

            //add left click event to all buttons in leaderboard list
            foreach(Button b in leaderBoardButtons)
            {
                b.OnLeftButtonClick += this.LeftButtonClicked;
            }


            //========Infinity screen only!========
            infinityButtons.Add(backButton);

            //add left click event to all buttons in infinity list
            foreach (Button b in infinityButtons)
            {
                b.OnLeftButtonClick += this.LeftButtonClicked;
            }


            //========Levels screen only=========
            levelsButtons.Add(backButton);

            //add left click event to all buttons in level list
            foreach (Button b in levelsButtons)
            {
                b.OnLeftButtonClick += this.LeftButtonClicked;
            }




        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Getting  the state of the keyboard and the Mouse
            //mState = Mouse.GetState();
            //KBstate = Keyboard.GetState();


            //currentGameState = stackGameModes.Peek();

            //Switch that interracts with the game
            switch (currentGameState)
            {
                case GameMode.Menu:

                    ////if someone pressed within the rectangle of the level select, they will be taken to the levels
                    //if (levelSelect.Contains(mState.Position) &&
                    //    mState.LeftButton == ButtonState.Released &&
                    //    prevMstate.LeftButton == ButtonState.Pressed)
                    //{
                    //    gameState = GameMode.Levels;
                    //}

                    ////if infinity mode is clicked, they will be taken to infinity mode
                    //if (infinity.Contains(mState.Position) &&
                    //    mState.LeftButton == ButtonState.Released &&
                    //    prevMstate.LeftButton == ButtonState.Pressed)
                    //{
                    //    gameState = GameMode.Infinity;
                    //}

                    ////If LeaderBoards is clicked, they will be taken to the leaderBoards
                    //if (leaderBoard.Contains(mState.Position) &&
                    //    mState.LeftButton == ButtonState.Released &&
                    //    prevMstate.LeftButton == ButtonState.Pressed)
                    //{
                    //    gameState = GameMode.LeaderBoard;
                    //}

                    foreach (Button buttons in menuButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;

                case GameMode.Infinity:

                    player.Update(gameTime);

                    foreach (Button buttons in infinityButtons)
                    {
                        buttons.Update(gameTime);
                    }

                    break;


                case GameMode.LeaderBoard:
                    foreach (Button buttons in leaderBoardButtons)
                    {
                        buttons.Update(gameTime);
                    }
                    break;


                case GameMode.Levels:
                    foreach (Button buttons in levelsButtons)
                    {
                        buttons.Update(gameTime);
                    }
                    break;


                case GameMode.Pause:
                    
                    break;


                case GameMode.Gameplay:
                    break;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            

            //Switch that draws things based on the state of the ganme
            switch (currentGameState)
            {
                //While in menu draw screen and background 
                case GameMode.Menu:
                    GraphicsDevice.Clear(Color.DarkBlue);
                    _spriteBatch.Draw(tempWallpaper, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width,
                    _graphics.GraphicsDevice.Viewport.Height), Color.White);

                    //temp title. do not take our title seriously pls -Ricky Y
                    _spriteBatch.DrawString(arialBold30, "<Galatic Inferno: BulletStorm>", new Vector2(20, 20), Color.White);

                    foreach (Button b in menuButtons)
                    {
                        b.Draw(_spriteBatch);
                    }

                    //Making dividers for each level
                    //_spriteBatch.Draw(rectangle, new Vector2(375, 185), Color.Red);
                    //_spriteBatch.Draw(rectangle, new Vector2(375, 260), Color.Red);
                    break;

                //draws infinity screen
                case GameMode.Infinity:
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Draw(gameBackground, new Rectangle( 100, 0, _graphics.GraphicsDevice.Viewport.Width/2,
                    _graphics.GraphicsDevice.Viewport.Height), Color.White);

                    BackButtonDisplayer();

                    foreach (Button b in infinityButtons)
                    {
                        b.Draw(_spriteBatch);
                    }

                    player.Draw(_spriteBatch);

                    break;

                //draws leaderboard screen
                case GameMode.LeaderBoard:
                    GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Draw(leaderBoardImage, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width,
                    _graphics.GraphicsDevice.Viewport.Height), Color.White);

                    BackButtonDisplayer();

                    foreach (Button b in leaderBoardButtons)
                    {
                        b.Draw(_spriteBatch);
                    }

                    break;

                //draws levels screen
                case GameMode.Levels:
                    GraphicsDevice.Clear(Color.DarkSlateBlue);
                    _spriteBatch.DrawString(arialBold30, "Levels", new Vector2(100,100), Color.White);
                    BackButtonDisplayer();
                    foreach (Button b in leaderBoardButtons)
                    {
                        b.Draw(_spriteBatch);
                    }
                    break;


                case GameMode.Pause:
                    break;


                case GameMode.Gameplay:
                    break;
            }


            

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //=====helper method?=======
        public void BackButtonDisplayer()
        {
            _spriteBatch.Draw(backButtonPNG,
                        new Rectangle(backButton.Position.X, backButton.Position.Y,
                                      backButton.Position.Width, backButton.Position.Height),
                                      Color.White);
        }


        //==========================================
        //============ Click handlers=========
        //==========================================

        //switch current game state when button are clicked.
        public void LeftButtonClicked()
        {
            switch (currentGameState)
            {
                case GameMode.Menu:
                    if (selectLevel.IsClicked)
                    {
                        //stackGameModes.Push(GameMode.Levels);
                        currentGameState = GameMode.Levels;
                    }
                    if (infinity.IsClicked)
                    {
                        //stackGameModes.Push(GameMode.Infinity);
                        currentGameState = GameMode.Infinity;
                    }
                    if (leaderBoard.IsClicked)
                    {
                        //stackGameModes.Push(GameMode.LeaderBoard);
                        currentGameState = GameMode.LeaderBoard;
                    }
                    break;


                case GameMode.Infinity:

                    if (backButton.IsClicked)
                    {
                        //stackGameModes.Pop();
                        currentGameState = GameMode.Menu;
                    }
                    break;


                case GameMode.LeaderBoard:
                    if (backButton.IsClicked)
                    {
                        //stackGameModes.Pop();
                        currentGameState = GameMode.Menu;
                    }
                    break;


                case GameMode.Levels:
                    if (backButton.IsClicked)
                    {
                        //stackGameModes.Pop();
                        currentGameState = GameMode.Menu;
                    }
                    break;


                case GameMode.Pause:
                    break;


                case GameMode.Gameplay:
                    break;


            }
        }
    }
}