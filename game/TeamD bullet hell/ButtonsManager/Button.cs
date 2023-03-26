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
    public delegate void OnButtonClickDelegate();

    internal class Button
    {
        //buttons fields
        protected MouseState prevMouseState;
        protected Rectangle position; //button position and size
        protected Texture2D buttonImage;
        private bool isClicked;

        /// <summary>
        /// bool: is the button clicked?
        /// </summary>
        public bool IsClicked
        {
            get { return isClicked; }
        }

        /// <summary>
        /// reference of button texture stored inside
        /// </summary>
        public Texture2D ButtonImage
        {
            get { return buttonImage; }
        }

        /// <summary>
        /// Button's position as rectangle
        /// </summary>
        public Rectangle Position
        {
            get 
            { 
                return this.position;
            }
        }

        /// <summary>
        /// Button's X position (top left corner of rec)
        /// </summary>
        public int X
        {
            get
            {
                return this.position.X;
            }
            set
            {
                this.position.X = value;
            }
        }

        /// <summary>
        /// Button's Y position (top left corner of rec)
        /// </summary>
        public int Y
        {
            get
            {
                return this.position.Y;
            }
            set
            {
                this.position.Y = value;
            }
        }

        //****NOTE: Idk if I will use this. Temp Placement -RY***
        //public int Width
        //{
        //    get
        //    {
        //        return this.position.Width;
        //    }
        //    set
        //    {
        //        this.position.Width = value;
        //    }
        //}

        //public int Height
        //{
        //    get
        //    {
        //        return this.position.Height;
        //    }
        //    set
        //    {
        //        this.position.Height = value;
        //    }
        //}


        //events
        public event OnButtonClickDelegate OnLeftButtonClick;

        /// <summary>
        /// Button constructor
        /// </summary>
        /// <param name="device"></param>
        /// <param name="position">Where to draw the button's top left corner</param>
        public Button(GraphicsDevice device, Rectangle position, Texture2D texture)
        {
            this.position = position;           
            this.buttonImage = texture;

        }

        /// <summary>
        /// This code is referenced/taken from PE - MG button, Event and delegate
        /// </summary>
        /// <param name="gameTime">Unused, but requried to implement abstract class</param>
        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            

            if (mouseState.LeftButton == ButtonState.Released &&
                prevMouseState.LeftButton == ButtonState.Pressed &&
                this.position.Contains(mouseState.Position) )
            {
                if (OnLeftButtonClick != null)
                {
                    this.isClicked = true;
                    OnLeftButtonClick();
                }  
            }
            else
            {
                this.isClicked = false;
            }


            prevMouseState = mouseState;

        }

        /// <summary>
        /// Override GameObject Draw() to draw the button
        /// referenced from PE - MG button, Event and delegate
        /// </summary>
        /// <param name="spriteBatch">The spritebatch that's used to draw this button.
        /// Assumes .Begin() is called already and .End() will be called afterwards</param>
        public void Draw(SpriteBatch spriteBatch)
        {

            MouseState mouseState  = Mouse.GetState();

            if (this.position.Contains(mouseState.Position))
            {
                spriteBatch.Draw(buttonImage, position, Color.White);
            } 
        }

    }
}
