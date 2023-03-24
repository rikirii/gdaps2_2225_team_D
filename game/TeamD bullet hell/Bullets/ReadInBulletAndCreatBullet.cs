﻿using Microsoft.Xna.Framework;
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

        //when shouldRemove = true remove the bullet
        private bool shouldRemove;

        // direction represented by angle in radians
        private double angle = 0;

        public ReadInBulletAndCreatBullet()
        {
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


                    ///////////////////////////////////////////////////////////////////////////
                    ///creat the bullet with the data we get from the file here and load it into the list

                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Uh oh: " + e.Message);
            }

        }
    }
}