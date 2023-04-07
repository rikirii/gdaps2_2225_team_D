using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using TeamD_bullet_hell.Bullets;
using TeamD_bullet_hell.ButtonsManager;
using TeamD_bullet_hell.GameStates;
using TeamD_bullet_hell.GameStates.GamePlay;
using TeamD_bullet_hell.GameStates.Title;

namespace TeamD_bullet_hell
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //track window's size
        private int windowHeight;
        private int windowWidth;

        //bullet
        private Texture2D greenCircleBullet;

        private List<Bullet> bulletList;



        //SpriteFont
        private SpriteFont titleFont;
        private SpriteFont buttonFont;
        private SpriteFont normalFont;

        ////sprite and texture fields for the menu        
        private Texture2D mainWallpaper;
        private Texture2D leaderBoardImage;
        private Texture2D gameBackground;
        private Texture2D playerShip;
        private Texture2D backButtonPNG;
        private Texture2D buttonOutline;


        //field that tracks the gameState
        GameState currentGameState;

        //Getting a mouse state and keyboard state
        MouseState mState;
        MouseState prevMstate;
        KeyboardState KBstate;
        KeyboardState prevKBstate;

        //timer for the bullet
        float deltaTime = 0f;
        float currentGameTime = 0f;

        //Declare state manager (important where all states/screen are managed)
        private StateManager stateMgr;

        //Temp dictionary variables to pass reference to state manager
        private Dictionary<GameState, Texture2D> wallpapers;
        private Dictionary<FontType, SpriteFont> fontsCollection;
        private Dictionary<Entity, Texture2D> spriteCollection;
        private Dictionary<ButtonAssets, Texture2D> buttonAssets;

        private Random r;
        private Bullet[,] bulletArray;
       

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = true; //fullscreen (not recommended -RY)

            //this will make window size auto scale to current monitor's size
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            //makes screen borderless
            Window.IsBorderless = true;
            _graphics.ApplyChanges();

            //store window's width and height
            this.windowWidth = _graphics.GraphicsDevice.Viewport.Width;
            this.windowHeight = _graphics.GraphicsDevice.Viewport.Height;

            //initialize temp dictionary
            wallpapers = new Dictionary<GameState, Texture2D>();
            fontsCollection = new Dictionary<FontType, SpriteFont>();
            spriteCollection = new Dictionary<Entity, Texture2D>();
            buttonAssets = new Dictionary<ButtonAssets, Texture2D>();

            //makes bullet list to track # of bullet (temp location -RY)
            bulletList = new List<Bullet>();
            bulletArray = new Bullet[11, 11];
            r = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //******IMPORTANT INFO******* -RY
            //when loading in assets from a folder in Content, You must do "../Content/<Insert folder name>/<Insert file name>"

            //loading fonts
            titleFont = Content.Load<SpriteFont>("../Content/Fonts/Arial30Bold");
            buttonFont = Content.Load<SpriteFont>("../Content/Fonts/Arial20Normal");

            //loading background
            mainWallpaper = Content.Load<Texture2D>("../Content/Background/gameMenu");
            leaderBoardImage = Content.Load<Texture2D>("../Content/Background/leaderboard");
            gameBackground = Content.Load<Texture2D>("../Content/Background/gameBackground");

            //loading button textures
            buttonOutline = Content.Load<Texture2D>("Redtangle"); //bad name for this. -RY
            backButtonPNG = Content.Load<Texture2D>("../Content/ButtonAssets/backButton");

            //load Bullet Texture 
            greenCircleBullet = Content.Load<Texture2D>("GreenBullet");

            //loading player
            playerShip = Content.Load<Texture2D>("ship");

            //File IO Method
            BulletMap("Bullet Pattern");


            //testing ********************

            //Adding wallpaper to dictionary. Use key(GameState) to get reference of the wallpaper to use
            wallpapers.Add(GameState.Menu, mainWallpaper);
            wallpapers.Add(GameState.LeaderBoard, leaderBoardImage);
            wallpapers.Add(GameState.Gameplay, gameBackground);

            //Adding font types to dictionary. Use key(FontType) to get reference of the spritefont
            fontsCollection.Add(FontType.Title, titleFont);
            fontsCollection.Add(FontType.Button, buttonFont);

            //Adding button assets to dictionary. Use ButtonAssets to get reference of the buttonassets
            buttonAssets.Add(ButtonAssets.Outline, buttonOutline);
            buttonAssets.Add(ButtonAssets.BackButton, backButtonPNG);

            //Adding player, enemy, bullet, etc to dictionary. Use Entity enum to get reference of their texture2D
            spriteCollection.Add(Entity.Player, playerShip);
            spriteCollection.Add(Entity.Bullet, greenCircleBullet);


            //load Game State Manager
            stateMgr = new StateManager(_graphics, this.windowWidth, this.windowHeight, wallpapers, fontsCollection, spriteCollection, buttonAssets);

            stateMgr.ButtonMgr.StateMgr = this.stateMgr;


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.currentGameState = stateMgr.CurrentGameState;

            //Switch that interracts with the game
            switch (currentGameState)
            {
                case GameState.Menu:


                    stateMgr.Update(gameTime);

                    break;


                case GameState.Levels:

                    stateMgr.Update(gameTime);

                    break;


                case GameState.Infinity:

                    stateMgr.Update(gameTime);
                    for (int i = 0; i < bulletArray.GetLength(0); i++)
                    {
                        //Random velocity for the bullets to move in
                        int xMove = r.Next(2, 7);
                        int yMove = r.Next(2, 7);

                        for (int j = 0; j < bulletArray.GetLength(1); j++)
                        {
                            if (bulletArray[i, j] != null)
                            {
                                //if it is on the left half of the screen, move in the + direction
                                //if on the right, move in the - direction
                                if (bulletArray[i, j].Position.X > (windowWidth / 2))
                                {
                                    bulletArray[i, j].PositionX += xMove;

                                }
                                else
                                {
                                    bulletArray[i, j].PositionX -= xMove;
                                }

                                //if it is on the top half of the screen, move down direction
                                //if on the bottom, move up
                                if (bulletArray[i, j].Position.Y > (windowHeight / 2))
                                {
                                    bulletArray[i, j].PositionY += yMove;

                                }
                                else
                                {
                                    bulletArray[i, j].PositionY -= yMove;

                                }

                                
                            }
                        }
                    }

                    break;

                case GameState.LeaderBoard:

                    stateMgr.Update(gameTime);

                    break;


                case GameState.Pause:

                    break;


                case GameState.GameOver:
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
                case GameState.Menu:

                    //GraphicsDevice.Clear(Color.Black);

                    stateMgr.Draw(_spriteBatch);

                    MouseState mState = Mouse.GetState();
                    //this is for testing, looking for coords
                    _spriteBatch.DrawString(fontsCollection[FontType.Button], string.Format("windowX: {0}," +
                        " windowY: {1} || mouse x: {2} mouse y: {3}", windowWidth, windowHeight, mState.X, mState.Y), new Vector2(10, 10), Color.White);
                    //Making dividers for each level
                    //_spriteBatch.Draw(rectangle, new Vector2(375, 185), Color.Red);
                    //_spriteBatch.Draw(rectangle, new Vector2(375, 260), Color.Red);
                    break;


                //draws levels screen
                case GameState.Levels:

                    GraphicsDevice.Clear(Color.DarkSlateBlue);

                    stateMgr.Draw(_spriteBatch);
                    break;


                //draws infinity screen
                case GameState.Infinity:
                    GraphicsDevice.Clear(Color.Black);

                    //Background Commented Out Temporarily for playtesting (screen is just black)

                    stateMgr.Draw(_spriteBatch);

                    //Draws the bullets based on the 2D array from the file
                    for (int i = 0; i < bulletArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < bulletArray.GetLength(1); j++)
                        {
                            if (bulletArray[i, j] != null)
                            {
                                bulletArray[i, j].Draw(_spriteBatch);

                            }


                        }

                    }

                    //draw the bullet

                    //foreach (Bullet bullet in bulletList)
                    //{
                    //    bullet.Draw(_spriteBatch);
                    //} 

                    //COLLISION CODE COMMENTED OUT FOR NOW
                    /*Collision Logic + Bullet Drawing
                    foreach (Bullet bullet in bulletList)// print all squares
                    {
                        bullet.Draw(_spriteBatch);
                        if (myPlayer.Intersects(bullet) == true)
                        {
                            currentGameState = GameState.Menu;
                        }
                    } */




                    break;


                //draws leaderboard screen
                case GameState.LeaderBoard:
                    //GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Draw(leaderBoardImage, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width,
                    _graphics.GraphicsDevice.Viewport.Height), Color.White);

                    stateMgr.Draw(_spriteBatch);


                    break;


                case GameState.Pause:
                    break;


                case GameState.GameOver:
                    break;
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Method that mapes a map of the bullets loaded in from a file
        /// </summary>
        /// <param name="filename">the name of the file that wants to be read in</param>
        public void BulletMap(string filename)
        {
            float spawnTime = 1;

            StreamReader input = null;
            try
            {
                //and declaring it in the try block
                input = new StreamReader("../../../" + filename + ".csv");

                //create a string to bring the data in and loop while the line has data 
                string line = null;

                for (int i = 0; i < 11; i++)
                {
                    //read a line every single row
                    line = input.ReadLine();

                    //make a string array for each row
                    string[] row = line.Split(',');

                    for (int j = 0; j < 11; j++)
                    {
                        //if the element in the string array is an X, then make a new bullet at that spot in the array
                        if (char.Parse(row[j]) == 'X')
                        {
                            bulletArray[i, j] = new Bullet(r.Next(0, 180), new Rectangle(r.Next(450, 900), r.Next(200, 900),
                                75, 75), greenCircleBullet, 20, spawnTime += 0.5f, 1920, 1080);
                        }
                        else if (char.Parse(row[j]) == '-')
                        {
                            //if not that spot is null
                            bulletArray[i, j] = null;
                        }


                    }

                }


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Uh oh: " + e.Message);
            }

            input.Close();


        }
    }
}