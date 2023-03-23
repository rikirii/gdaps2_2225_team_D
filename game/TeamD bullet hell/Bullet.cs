using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        public Bullet(Rectangle positionAndSize, Texture2D textureOfBullet, double velocity,float directionInDegrees, int windowHeight, int windowWidth)
        {
            this.positionAndSize = positionAndSize;
            this.textureOfBullet = textureOfBullet;

            this.velocity = velocity;

            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            shouldRemove = false;

            //convert the angle to radius for vector math NOOOOOOO-------
            angle = MathHelper.ToRadians(directionInDegrees);
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
