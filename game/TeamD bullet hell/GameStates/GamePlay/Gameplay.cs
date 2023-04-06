using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TeamD_bullet_hell.Bullets;

namespace TeamD_bullet_hell.GameStates.GamePlay
{
    internal class Gameplay : IScreens
    {
        internal GameState currentGameState;

        private bool gameOver;

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


        //BulletList variable for collision testing
        internal List<Bullet> bulletList;


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


        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
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
        public Gameplay(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Dictionary<GameState, Texture2D> wallpapers, Dictionary<FontType, SpriteFont> fonts, Dictionary<Entity, Texture2D> spriteCollection)
        {
            this._graphics = graphics;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.wallpapers = wallpapers;
            this.fonts = fonts;
            this.spriteCollection = spriteCollection;

            this.gameOver = false;

            this.bulletMgr = new BulletManager(_graphics, windowWidth, windowHeight, spriteCollection);
            bulletMgr.LoadBulletFile(spriteCollection[Entity.Bullet]);
            this.bulletList = this.bulletMgr.BulletList;

            //create player object
            player = new Player(spriteCollection[Entity.Player], new Rectangle(windowWidth/2, windowHeight/2, 100, 100), windowWidth, windowHeight);
        }

        /// <summary>
        /// resets the game for next round
        /// </summary>
        public void Reset()
        {
            ////reset the player not sure it is working
            player = new Player(spriteCollection[Entity.Player], new Rectangle(windowWidth / 2, windowHeight / 2, 100, 100), windowWidth, windowHeight);

            //reset the bullet 
            bulletMgr.Reset(spriteCollection[Entity.Bullet]);

            this.gameOver = false;
            this.player.Lives = 3;
        }


        /// <summary>
        /// Gameplay manager's update
        /// </summary>
        /// <param name="gameTime">game1 gametime</param>
        public void Update(GameTime gameTime)
        {
            //updates currentgame state for bullet manager
            bulletMgr.currentGameState = this.currentGameState;

            //Switch taht determines which button to update
            switch (currentGameState)
            {

                case GameState.Levels:

                    

                    break;


                case GameState.Infinity:

                    player.Update(gameTime);
                    
                    bulletMgr.Update(gameTime);

                    //Collision Logic
                    foreach (Bullet bullet in bulletList)
                    {
                        if (player.Intersects(bullet))
                        {
                            player.Lives -= 1;
                        }
                    }

                    if (player.Lives <= 0)
                    {
                        
                        gameOver = !gameOver;
                        currentGameState = GameState.Menu;
                    }

                    break;


                case GameState.LeaderBoard:

                    break;



                case GameState.Pause:

                    break;


                case GameState.Gameplay:
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

                case GameState.Levels:

                    spriteBatch.DrawString(fonts[FontType.Title], "Levels", new Vector2(100, 100), Color.White);

                    break;


                case GameState.Infinity:
                    spriteBatch.Draw(wallpapers[GameState.Gameplay], new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

                    spriteBatch.DrawString(fonts[FontType.Button], string.Format("Lives: {0}", player.Lives), new Vector2(10, 100), Color.White);

                    player.Draw(spriteBatch);

                    bulletMgr.Draw(spriteBatch);

                    break;


                case GameState.LeaderBoard:

                    break;



                case GameState.Pause:

                    break;


                case GameState.Gameplay:
                    break;
            }



        }
    }
}
