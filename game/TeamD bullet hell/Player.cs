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

        //player stats
        private int lives;

        public Rectangle Position
        {
            get { return position; }
        }

        
        public int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
            }
        }

        public Player(Texture2D asset, Rectangle position, int windowWidth, int windowHeight)
        {
            this.asset = asset;
            this.position = position;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            this.lives = 3;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKBState = Keyboard.GetState();

            //movement if statements
            if (currentKBState.IsKeyDown(Keys.Up))
            {
                this.position.Y -= 10;
            }
            if (currentKBState.IsKeyDown(Keys.Down))
            {
                this.position.Y += 10;
            }

            if (currentKBState.IsKeyDown(Keys.Left))
            {
                this.position.X -= 10;
            }
            if (currentKBState.IsKeyDown(Keys.Right))
            {
                this.position.X += 10;
            }

            //if and else ifs for screen lock
            if ( (this.position.X + this.position.Width ) >= (this.windowWidth) )
            {
                this.position.X = (this.windowWidth - this.position.Width);
            }
            else if (this.position.X <= 0)
            {
                this.position.X = 0;
            }
            if ( (this.position.Y + this.position.Height) >= this.windowHeight )
            {
                this.position.Y = (this.windowHeight - this.position.Height);
            }
            else if (this.position.Y <= 0)
            {
                this.position.Y = 0;
            }
        }

        public void Draw (SpriteBatch sb)
        {
            sb.Draw(this.asset, this.position, Color.White);
        }

        //Intersects method returns true if this Player is colliding with the bullets, and false otherwise. 
        public bool Intersects(Bullet other)
        {
            if (this.position.Intersects(other.Position))
            {
                lives--;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
