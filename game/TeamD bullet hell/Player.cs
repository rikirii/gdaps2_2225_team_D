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

        protected int speed = 10;

        //player stats
        private int lives;

        public Rectangle Position
        {
            get { return position; }
        }

        public int X
        {
            get { return position.X; }
            set
            {
                this.position.X = value;
            }
        }

        public int Y
        {
            get { return position.Y; }
            set { this.position.Y = value; }
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

            this.lives = 1;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKBState = Keyboard.GetState();

            //press shift for slow mothion 
            if (currentKBState.IsKeyDown(Keys.LeftShift)|| currentKBState.IsKeyDown(Keys.RightShift))
            {
                speed = 5;
            }
            else
            {
                speed = 10;
            }

            //movement if statements
            if (currentKBState.IsKeyDown(Keys.Up) || currentKBState.IsKeyDown(Keys.W) )
            {
                this.position.Y -= speed;
            }
            if (currentKBState.IsKeyDown(Keys.Down) || currentKBState.IsKeyDown(Keys.S) )
            {
                this.position.Y += speed;
            }

            if (currentKBState.IsKeyDown(Keys.Left) || currentKBState.IsKeyDown(Keys.A) )
            {
                this.position.X -= speed;
            }
            if (currentKBState.IsKeyDown(Keys.Right) || currentKBState.IsKeyDown(Keys.D) )
            {
                this.position.X += speed;
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
