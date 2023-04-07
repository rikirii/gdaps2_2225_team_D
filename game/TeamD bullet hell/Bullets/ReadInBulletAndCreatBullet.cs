using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell.Bullets
{
    internal class ReadInBulletAndCreatBullet
    {
        private List<Bullet> bulletList;

        //screen size
        private int windowHeight;
        private int windowWidth;

        private Texture2D textureOfBullet;

        //the size and the postion 
        private Rectangle positionAndSize;

        private double velocity;
        private float directionInDegrees;
        private double timeAppear;

        int xPosition = 100;
        int yPosition = 100;
        Bullet b;

        Bullet[,] bulletArray;
       

        //when shouldRemove = true remove the bullet
        private bool shouldRemove;

        // direction represented by angle in radians
        private double angle = 0;

        public ReadInBulletAndCreatBullet()
        {
            /*
            //Initalizing a stream reader 
            StreamReader input = null;
            try
            {
                //and declaring it in the try block
                input = new StreamReader("../../../Bullet Pattern.csv");

                //create a string to bring the data in and loop while the line has data 
                string line = null;
                while ((line = input.ReadLine()) != null)
                {
                    //split the data in the string by a comma 
                    string[] data = line.Split(',');

                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] == "X")
                        {
                            b = new Bullet(90, new Rectangle(xPosition, yPosition, 100,100), )

                        }
                    }


                    ///////////////////////////////////////////////////////////////////////////
                    ///creat the bullet with the data we get from the file here and load it into the list

                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Uh oh: " + e.Message);
            }
            */
        }
    }
}
