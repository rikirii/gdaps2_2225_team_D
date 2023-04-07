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
        protected Texture2D buttonOutline;
        private bool isClicked;

        //god mode button variables
        private bool isGodModeButton;
        private bool isGodMode;
        private Texture2D godModeImgOff;
        private Texture2D godModeImgOn;

        //variables for the reference of code from PE MG button
        private Vector2 textLoc;
        private Texture2D buttonImg;
        private Color textColor;
        private string text;
        protected bool enabled = true;
        protected SpriteFont font;


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
            get { return buttonOutline; }
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

        /// <summary>
        /// only god mode button. 
        /// no other button should use this
        /// </summary>
        public bool IsGodMode
        {
            get
            {
                return this.isGodMode;
            }
            set
            {
                this.isGodMode = value;
            }
        }

        //events
        public event OnButtonClickDelegate OnLeftButtonClick;

        /// <summary>
        /// Button constructor.
        /// code has been copied from PE - MG button (starter code by Prof. Mesh)
        /// https://docs.google.com/document/d/1HUmX3GKyeuwmuKJEw8MpYIbgyowxO8pX0fEqeqGoPAg/edit
        /// </summary>
        /// <param name="device"></param>
        /// <param name="position">Where to draw the button's top left corner</param>
        public Button(GraphicsDevice device, Rectangle position, String text, SpriteFont font, Color color, Texture2D outline, bool isGodModeButton)
        {
            this.position = position;           
            this.buttonOutline = outline;

            this.isGodMode = false;
            this.isGodModeButton = isGodModeButton;

            ///code by Prof.Mesh from PE MG button starts here==============
            this.font = font;
            this.text = text;

            // Figure out where on the button to draw it
            Vector2 textSize = font.MeasureString(text);
            textLoc = new Vector2(
                (position.X + position.Width / 2) - textSize.X / 2,
                (position.Y + position.Height / 2) - textSize.Y / 2
            );

            this.textColor = Color.White;

            if (isGodModeButton)
            {
                // Make a custom 2d texture for the god button itself
                //requires two to switch between off and on state texture

                godModeImgOff = new Texture2D(device, position.Width, position.Height, false, SurfaceFormat.Color);
                int[] colorData = new int[godModeImgOff.Width * godModeImgOff.Height];
                Array.Fill<int>(colorData, (int)(Color.Red).PackedValue);
                godModeImgOff.SetData<Int32>(colorData, 0, colorData.Length);
  
                godModeImgOn = new Texture2D(device, position.Width, position.Height, false, SurfaceFormat.Color);
                colorData = new int[godModeImgOn.Width * godModeImgOn.Height];
                Array.Fill<int>(colorData, (int)(Color.Green).PackedValue);
                godModeImgOn.SetData<Int32>(colorData, 0, colorData.Length);
            }
            else
            {
                // Make a custom 2d texture for the button itself
                buttonImg = new Texture2D(device, position.Width, position.Height, false, SurfaceFormat.Color);
                int[] colorData = new int[buttonImg.Width * buttonImg.Height];
                Array.Fill<int>(colorData, (int)color.PackedValue);
                buttonImg.SetData<Int32>(colorData, 0, colorData.Length);
            }

            

            ///end=============
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
            if (isGodModeButton)
            {
                if (isGodMode)
                {
                    // Draw the button itself
                    spriteBatch.Draw(this.godModeImgOn, position, Color.White);
                }
                else
                {
                    // Draw the button itself
                    spriteBatch.Draw(this.godModeImgOff, position, Color.White);
                }
            }
            else
            {
                // Draw the button itself
                spriteBatch.Draw(buttonImg, position, Color.Black);
            }
            

            // Draw button text over the button
            spriteBatch.DrawString(font, text, textLoc, textColor);

            MouseState mouseState  = Mouse.GetState();

            if (this.position.Contains(mouseState.Position))
            {
                spriteBatch.Draw(buttonOutline, position, Color.White);
            }

            
        }

    }
}
