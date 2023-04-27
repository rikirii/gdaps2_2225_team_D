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
        private Texture2D gameOverImage;
        private Texture2D playerShip;
        private Texture2D backButtonPNG;
        private Texture2D buttonOutline;


        //test for the animation on background 
        private List<Texture2D> backGroundList;
        private Texture2D placeHolderTexture;


        //field that tracks the gameState
        GameState currentGameState;

        //Getting a mouse state and keyboard state
        MouseState mState;
        MouseState prevMstate;
        KeyboardState KBstate;
        KeyboardState prevKBstate;

       

        //Declare state manager (important where all states/screen are managed)
        private StateManager stateMgr;
        

        //Temp dictionary variables to pass reference to state manager
        private Dictionary<GameState, Texture2D> wallpapers;
        private Dictionary<FontType, SpriteFont> fontsCollection;
        private Dictionary<Entity, Texture2D> spriteCollection;
        private Dictionary<ButtonAssets, Texture2D> buttonAssets;

       
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            //use this resolution to test. only switch back to 1080p after everything is working OKAY
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            //_graphics.PreferredBackBufferWidth = 1920;
            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.IsFullScreen = true; //fullscreen (not recommended -RY)

            //this will make window size auto scale to current monitor's size
            //_graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            //_graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;

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

            //size and position rescaler




            //makes bullet list to track # of bullet (temp location -RY)



           backGroundList = new List<Texture2D>();

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
            gameOverImage = Content.Load<Texture2D>("../Content/Background/gameOver");
            Texture2D winScreen = Content.Load<Texture2D>("../Content/Background/win");
            Texture2D instruction = Content.Load<Texture2D>("../Content/Background/instruction");

            //loading button textures
            buttonOutline = Content.Load<Texture2D>("../Content/ButtonAssets/buttonOutline");
            backButtonPNG = Content.Load<Texture2D>("../Content/ButtonAssets/backButton");

            //load Bullet Texture 
            greenCircleBullet = Content.Load<Texture2D>("../Content/BulletAsset/GreenBullet");

            //loading player
            playerShip = Content.Load<Texture2D>("../Content/playerAsset/ship");



            //testing ********************

            //Adding wallpaper to dictionary. Use key(GameState) to get reference of the wallpaper to use
            wallpapers.Add(GameState.Menu, mainWallpaper);
            wallpapers.Add(GameState.LeaderBoard, leaderBoardImage);
            wallpapers.Add(GameState.Gameplay, gameBackground);
            wallpapers.Add(GameState.GameOver, gameOverImage);
            wallpapers.Add(GameState.Win, winScreen);
            wallpapers.Add(GameState.Instruction, instruction);

            //Adding font types to dictionary. Use key(FontType) to get reference of the spritefont
            fontsCollection.Add(FontType.Title, titleFont);
            fontsCollection.Add(FontType.Button, buttonFont);

            //Adding button assets to dictionary. Use ButtonAssets to get reference of the buttonassets
            buttonAssets.Add(ButtonAssets.Outline, buttonOutline);
            buttonAssets.Add(ButtonAssets.BackButton, backButtonPNG);

            //Adding player, enemy, bullet, etc to dictionary. Use Entity enum to get reference of their texture2D
            spriteCollection.Add(Entity.Player, playerShip);
            spriteCollection.Add(Entity.Bullet, greenCircleBullet);


            //test reading in all the image in the backGround 
            for (int i = 0; i < 400; i++)
            {
                placeHolderTexture = backButtonPNG = Content.Load<Texture2D>($"../Content/Background/BackGroundAnimation/no  ({i+1})");
                backGroundList.Add(placeHolderTexture);
            }

            //load Game State Manager
            stateMgr = new StateManager(_graphics, this.windowWidth, this.windowHeight, wallpapers, fontsCollection, spriteCollection, buttonAssets,backGroundList);

            stateMgr.ButtonMgr.StateMgr = this.stateMgr;

            
            

        }

        protected override void Update(GameTime gameTime)
        {

            this.currentGameState = stateMgr.CurrentGameState;

            //Switch that interracts with the game
            switch (currentGameState)
            {
                case GameState.Menu:

                    stateMgr.Update(gameTime);

                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();


                    break;

                case GameState.Instruction:
                    stateMgr.Update(gameTime);

                    break;

                case GameState.Levels:

                    stateMgr.Update(gameTime);

                    break;


                case GameState.Infinity:
                case GameState.Gameplay:

                    stateMgr.Update(gameTime);
                    

                    break;

                

                case GameState.LeaderBoard:

                    stateMgr.Update(gameTime);

                    break;


                case GameState.Pause:
                    stateMgr.Update(gameTime);

                    
                    break;


                case GameState.GameOver:

                    stateMgr.Update(gameTime);
                    break;


                case GameState.Win:
                    stateMgr.Update(gameTime);
                    break;


                case GameState.Setting:
                    stateMgr.Update(gameTime);

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

                   
                    //this is for testing, looking for coords
                    //spriteBatch.DrawString(fonts[FontType.Button], string.Format("windowX: {0}," +
                    //    " windowY: {1} || mouse x: {2} mouse y: {3}", windowWidth, windowHeight, mState.X, mState.Y), new Vector2(10, 10), Color.White);

                    

                    //Making dividers for each level
                    //_spriteBatch.Draw(rectangle, new Vector2(375, 185), Color.Red);
                    //_spriteBatch.Draw(rectangle, new Vector2(375, 260), Color.Red);
                    break;



                case GameState.Instruction:
                    GraphicsDevice.Clear(Color.MidnightBlue);
                    stateMgr.Draw(_spriteBatch);

                    break;


                //draws levels screen
                case GameState.Levels:

                    GraphicsDevice.Clear(Color.MidnightBlue );

                    stateMgr.Draw(_spriteBatch);

                    //MouseState mState = Mouse.GetState();

                    //_spriteBatch.DrawString(fontsCollection[FontType.Button], string.Format("windowX: {0}," +
                    //    " windowY: {1} || mouse x: {2} mouse y: {3} | godMode: {4}", windowWidth, windowHeight, mState.X, mState.Y, stateMgr.IsGodMode), new Vector2(10, 10), Color.White);

                    break;


                //draws infinity screen
                case GameState.Gameplay:
                    GraphicsDevice.Clear(Color.Black);

                    //Background Commented Out Temporarily for playtesting (screen is just black)

                    stateMgr.Draw(_spriteBatch);


                    break;



                //draws leaderboard screen
                case GameState.LeaderBoard:
                    //GraphicsDevice.Clear(Color.Black);
                    _spriteBatch.Draw(leaderBoardImage, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width,
                    _graphics.GraphicsDevice.Viewport.Height), Color.White);

                    stateMgr.Draw(_spriteBatch);


                    break;


                case GameState.Pause:
                    GraphicsDevice.Clear(Color.Gray);

                    stateMgr.Draw(_spriteBatch);
                    break;


                case GameState.GameOver:
                    stateMgr.Draw(_spriteBatch);
                    break;

                case GameState.Win:
                    GraphicsDevice.Clear(Color.Black);
                    stateMgr.Draw(_spriteBatch);
                    break;

                case GameState.Setting:
                    stateMgr.Draw(_spriteBatch);
                    break;
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }

        
    }
}