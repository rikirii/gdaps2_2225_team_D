using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell.GameStates.GamePlay
{
    internal class Gameplay :IScreens
    {
        internal GameState currentGameState;
        //background assets
        internal Dictionary<GameState, Texture2D> wallpapers;
        internal Dictionary<FontType, SpriteFont> fonts;
        internal Dictionary<Entity, Texture2D> assets;

        //screen size
        internal int windowWidth;
        internal int windowHeight;

        //graphicsdevicemanager ref from game1
        internal GraphicsDeviceManager _graphics;

        //Entities
        internal Player player;

        /// <summary>
        /// Update current game state in gameplay object
        /// </summary>
        internal GameState CurrentGameState
        {
            set
            {
                currentGameState = value;
            }
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
        public Gameplay(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Dictionary<GameState, Texture2D> wallpapers, Dictionary<FontType, SpriteFont> fonts, Dictionary<Entity, Texture2D> assets)
        {
            this._graphics = graphics;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.wallpapers = wallpapers;
            this.fonts = fonts;
            this.assets = assets;

            player = new Player(assets[Entity.Player], new Rectangle(200, 200, 100, 100), windowWidth, windowHeight);
        }

        /// <summary>
        /// Gameplay manager's update
        /// </summary>
        /// <param name="gameTime">game1 gametime</param>
        public void Update (GameTime gameTime)
        {
            //Switch taht determines which button to update
            switch (currentGameState)
            {

                case GameState.Levels:

                    

                    break;


                case GameState.Infinity:

                    player.Update(gameTime);

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

                    spriteBatch.DrawString(fonts[FontType.Title] , "Levels", new Vector2(100, 100), Color.White);

                    break;


                case GameState.Infinity:

                    player.Draw(spriteBatch);
                    //Draw background
                    //_spriteBatch.Draw(gameBackground, new Rectangle( 180, 0, _graphics.GraphicsDevice.Viewport.Width/2,
                    //_graphics.GraphicsDevice.Viewport.Height), Color.White);
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
