using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        //sprite and texture fields for the menu
        private SpriteFont arialBold30;
        private Texture2D tempWallpaper;
        private Texture2D rectangle;

        //Rectangles to select the options on the Menu
        Rectangle levelSelect;
        Rectangle infinity;
        Rectangle leaderBoard;

        //field that tracks the gameState
        GameMode gameState;

        //Getting a mouse state and keyboard state
        MouseState mState;
        MouseState prevMstate;
        KeyboardState KBstate;
        KeyboardState prevKBstate;

        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = GameMode.Menu;
           


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            arialBold30 = Content.Load <SpriteFont>("arial");
            tempWallpaper = Content.Load<Texture2D>("temp");
            rectangle = Content.Load<Texture2D>("Redtangle");
            levelSelect = new Rectangle(375, 185, rectangle.Width / 2, rectangle.Height / 2);
            infinity = new Rectangle(375, 250 ,rectangle.Width / 2, rectangle.Height / 2);
            leaderBoard = new Rectangle(375, 260, rectangle.Width / 2, rectangle.Height / 2);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //Getting  the state of the keyboard and the Mouse
            mState = Mouse.GetState();
            prevMstate = Mouse.GetState();
            KBstate = Keyboard.GetState();

            //Switch that interracts with the game
            switch (gameState)
            {
                case GameMode.Menu:

                    //if someone pressed within the rectangle of the level select, they will be taken to the levels
                    if (levelSelect.Contains(mState.Position) &&
                        mState.LeftButton == ButtonState.Released &&
                        prevMstate.LeftButton == ButtonState.Pressed)
                    {
                        gameState = GameMode.Levels;
                    }

                    //if infinity mode is clicked, they will be taken to infinity mode
                    if (infinity.Contains(mState.Position) &&
                        mState.LeftButton == ButtonState.Released &&
                        prevMstate.LeftButton == ButtonState.Pressed)
                    {
                        gameState = GameMode.Infinity;
                    }

                    //If LeaderBoards is clicked, they will be taken to the leaderBoards
                    if (leaderBoard.Contains(mState.Position) &&
                        mState.LeftButton == ButtonState.Released &&
                        prevMstate.LeftButton == ButtonState.Pressed)
                    {
                        gameState = GameMode.LeaderBoard;
                    }
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
            switch (gameState)
            {
                //While in menu draw screen and background 
                case GameMode.Menu:
                _spriteBatch.Draw(tempWallpaper, new Rectangle(0,0, _graphics.GraphicsDevice.Viewport.Width,
                    _graphics.GraphicsDevice.Viewport.Height), Color.White);

                //temp title. do not take our title seriously pls -Ricky Y
                _spriteBatch.DrawString(arialBold30, "<Galatic Inferno: BulletStorm>", new Vector2(20, 20), Color.White);

                //Making dividers for each level
                _spriteBatch.Draw(rectangle, new Vector2(375,185), Color.Red);
                _spriteBatch.Draw(rectangle, new Vector2(375, 260), Color.Red);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}