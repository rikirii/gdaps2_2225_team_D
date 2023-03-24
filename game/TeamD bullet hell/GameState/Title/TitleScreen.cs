using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell.GameState.Title
{
    internal class TitleScreen : IScreens
    {
        //some fields
        private Texture2D titleBackGround;
        private int screenWidth;
        private int screenHeight;

        public TitleScreen(Texture2D titleBackGround, int screenWidth, int screenHeight)
        {
            this.titleBackGround = titleBackGround;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        

        
    }
}
