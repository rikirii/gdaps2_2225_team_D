using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell
{
    internal class Bullet
    {
        //screen size
        private int windowHeight;
        private int windowWidth;

        private Texture2D textureOfBullet;

        //the size and the postion 
        private Rectangle positionAndSize;

        private double velocity;

        //when shouldRemove = true remove the bullet
        private bool shouldRemove;

        // direction represented by angle in radians
        private double angle = 0;


        /// <summary>
        /// Pls use float for the direction because the method that convert degree into radius only take in float
        /// </summary>
        /// <param name="positionAndSize"></param>
        /// <param name="textureOfBullet"></param>
        /// <param name="velocity"></param>
        /// <param name="directionInDegrees"></param>
        /// <param name="windowHeight"></param>
        /// <param name="windowWidth"></param>
        public Bullet(float directionInDegrees, Texture2D texture)
        {
            this.textureOfBullet = texture;
            //Commenting all of this out in order to try putting the file IO in the constructor
            //It can be changed later if need be - Jarin 
            //this.positionAndSize = positionAndSize;
            //this.textureOfBullet = textureOfBullet;
            //this.velocity = velocity;
            //this.windowHeight = windowHeight;
            //this.windowWidth = windowWidth;

            //Initalizing a stream reader 
            StreamReader input = null;
            try
            {
                //and declaring it in the try block
                input = new StreamReader("../../../BulletData.txt");

                //create a string to bring the data in and loop while the line has data 
                string line = null;
                while ((line = input.ReadLine()) != null)
                {
                    //split the data in the string by a comma 
                    string[] data = line.Split(',');

                    //Make a new Rectangle based on the dimensions in the file (first 4 numbers)
                    positionAndSize = new Rectangle(int.Parse(data[0]), int.Parse(data[1]),
                        int.Parse(data[2]), int.Parse(data[3]));

                    //Make the velocity based on the velocity given in the file (5th number)
                    velocity = double.Parse(data[4]);

                    //take the angle number (the last number in the file)
                    //and make it the degrees variable 
                    directionInDegrees = float.Parse(data[5]);

                    //convert the angle to radius for vector math NOOOOOOO-------
                    angle = MathHelper.ToRadians(directionInDegrees);

                    //whatever this is lol
                    shouldRemove = false;

                }

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Uh oh: " + e.Message);
            }

        }

        public void Update(GameTime gameTime)
        {
            //get the delta time
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            // calculate the velocity vector using the angle
            Vector2 velocityVector = new Vector2((float)(velocity * Math.Cos(angle)), (float)(velocity * Math.Sin(angle)));

            //change the position over the time depend on the speed
            positionAndSize.X += (int)(velocityVector.X * deltaTime);
            positionAndSize.Y += (int)(velocityVector.Y * deltaTime);


            //mark the bullet to be removed if it move out side the screen
            if (positionAndSize.X < -positionAndSize.Width || positionAndSize.X > windowHeight ||
                positionAndSize.Y < -positionAndSize.Height || positionAndSize.Y > windowWidth)
            {
                shouldRemove = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureOfBullet, positionAndSize, Color.White);
        }

        /// <summary>
        /// return true when the bullet need to be removed
        /// </summary>
        /// <returns></returns>
        public bool ShouldRemove()
        {
            return shouldRemove;
        }

    }
}
