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

        //properties
        public bool IsClicked
        {
            get { return isClicked; }
        }

        public Rectangle Position
        {
            get 
            { 
                return this.position;
            }
        }

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
                else
                {
                    this.isClicked = false;
                }
                
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
