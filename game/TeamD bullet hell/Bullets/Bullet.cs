using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public Rectangle positionAndSize;

        private double velocity;
        private float directionInDegrees;

        private double spawnTime;
        private double spawnTimer;

        //when shouldRemove = true remove the bullet
        private bool upDateTheBall;
        public bool OutScreen { get; set; }

        // direction represented by angle in radians
        private double angleInRadians = 0;

        //private string type;
        


        /// <summary>
        /// Pls use float for the direction because the method that convert degree into radius only take in float
        /// </summary>
        /// <param name="positionAndSize"></param>
        /// <param name="textureOfBullet"></param>
        /// <param name="velocity"></param>
        /// <param name="directionInDegrees"></param>
        /// <param name="windowHeight"></param>
        /// <param name="windowWidth"></param>
        public Bullet(float directionInDegrees, Rectangle positionAndSize, Texture2D textureOfBullet, double velocity,double spawnTime, int windowWidth, int windowHeight)
        {

            //Commenting all of this out in order to try putting the file IO in the constructor
            //It can be changed later if need be - Jarin 
            this.directionInDegrees = directionInDegrees;
            this.positionAndSize = positionAndSize;
            this.textureOfBullet = textureOfBullet;
            this.velocity = velocity;
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.spawnTime = spawnTime;
            

            //spawntimer will be set to time of how many time the player enter the game


            //convert the angle to radius for vector math NOOOOOOO-------
            angleInRadians = MathHelper.ToRadians(directionInDegrees);

            //when this is true remove the bullet
            upDateTheBall = false;
            OutScreen = false;
        }

        public void Update(float currentGameTime)
        {
            spawnTimer = currentGameTime;

            // whe it reach the spawn time and the bullet is in the screen
            if (upDateTheBall== false&& OutScreen == false)
            {
                //when reach the spawn time 
                if(spawnTimer>= spawnTime)
                {
                    upDateTheBall = true;
                }
            }
            else if (upDateTheBall == true)
            {
                /////////////////////////////////////////////
                ///Some Problem here with the correct angle calculation
                Vector2 velocityVector = new Vector2((float)(velocity * Math.Cos(angleInRadians)), (float)(velocity * Math.Sin(angleInRadians)));

                //change the position over the time depend on the speed
                positionAndSize.X += (int)(velocityVector.X);
                positionAndSize.Y += (int)(velocityVector.Y);


                //mark the bullet to be removed if it move out side the screen
                if (positionAndSize.X < -200 || (positionAndSize.X + positionAndSize.Width) > windowWidth+100 ||
                    positionAndSize.Y < -200 || (positionAndSize.Y + positionAndSize.Height) > windowWidth+100)
                {
                    OutScreen = true;
                    upDateTheBall = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //only draw the ball when is update is ture
            if (upDateTheBall == true)
            {
                spriteBatch.Draw(textureOfBullet, positionAndSize, Color.White);
            }
        }

        
    }
}
