using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell.GameStates.Title
{
    internal class TitleScreen : IScreens
    {
        //some fields
        private Texture2D titleBackGround;

        private int screenWidth;
        private int screenHeight;

        private GraphicsDeviceManager _graphics;

        public TitleScreen(Texture2D titleBackGround, int screenWidth, int screenHeight, GraphicsDeviceManager graphics)
        {
            this.titleBackGround = titleBackGround;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this._graphics = graphics;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(titleBackGround, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width,
            _graphics.GraphicsDevice.Viewport.Height), Color.White);
        }

        

        

        
    }
}
