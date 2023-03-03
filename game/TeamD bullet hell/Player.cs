using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell
{
    internal class Player
    {
        protected Texture2D asset;
        protected Rectangle position;

        //window size
        protected int windowWidth;
        protected int windowHeight;

        public Player(Texture2D asset, Rectangle position, int windowWidth, int windowHeight)
        {
            this.asset = asset;
            this.position = position;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKBState = Keyboard.GetState();

            //movement if statements
            if (currentKBState.IsKeyDown(Keys.Up))
            {
                this.position.Y -= 3;
            }
            if (currentKBState.IsKeyDown(Keys.Down))
            {
                this.position.Y += 3;
            }

            if (currentKBState.IsKeyDown(Keys.Left))
            {
                this.position.X -= 3;
            }
            if (currentKBState.IsKeyDown(Keys.Right))
            {
                this.position.X += 3;
            }

            //if and else ifs for edge warping
            if (this.position.X > this.windowWidth)
            {
                this.position.X = 0;
            }
            else if (this.position.X < 0)
            {
                this.position.X = this.windowWidth;
            }
            if (this.position.Y > this.windowHeight)
            {
                this.position.Y = 0;
            }
            else if (this.position.Y < 0)
            {
                this.position.Y = this.windowHeight;
            }
        }

        public void Draw (SpriteBatch sb)
        {
            sb.Draw(this.asset, this.position, Color.White);
        }


    }
}
