using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell.Bullets
{
    internal class HardCodeBulletTest
    {
        ///////////////////////////////////////////////////////////////////////
        //// I am having fun with the bullet pattern here and if it is not working somthing may go wrong in other places
        /// 

        ////////////////// This is currently not been using !


        ////////////////////////////
        ///test code for the bullet pattern 
        private int windowHeight = 1080;
        private int windowWidth = 1920;

        private Texture2D textureOfBullet;
        private List<Bullet> bulletList = new List<Bullet>();

        private Rectangle a;

        private int bulletSizeX = 100;
        private int bulletSizeY = 100;

        //test bullet pattern
        private Bullet bulletss1;

        int degree = 130;
        int degree2 = 130*2;
        int degree3 = 0;
        float time = 0f;
        string type = "placeHolder";
        
       

        int postionX = 70;

        public HardCodeBulletTest(Texture2D texture)
        {
            this.textureOfBullet = texture;

            double velocity = 0;
            int yPosition = 0;
            int xPosition = 0;
            int deltaDegree = 0;

            Random rngNumber = new Random();

            //1 bullet pattern
             velocity = 10;
            degree = 30;
            for (int i = 0; i < 60; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree+=1, new Rectangle(1200, 0, bulletSizeX, bulletSizeY), texture, velocity+=0.1, time += 0.1f, windowWidth, windowHeight));
                time -= 0.05f;
                bulletList.Add(bulletss1 = new Bullet(degree += 1, new Rectangle(700, 0, bulletSizeX, bulletSizeY), texture, velocity += 0.1, time += 0.1f, windowWidth, windowHeight));
            }
            
            time += 1f;
            
            //2 bullet pattern
            velocity = 14;
            yPosition = 0;
            degree = 30;
            deltaDegree = 0;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 5, new Rectangle(200, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            degree = 120;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree -= 5, new Rectangle(400, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            degree = 60; 
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 5, new Rectangle(800, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            degree = 80;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree -= 5, new Rectangle(1000, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            time += 1f;
            
            
            degree = 140;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 5, new Rectangle(1600, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            degree = 112;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree -= 5, new Rectangle(1300, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            degree = 80;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 5, new Rectangle(900, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            velocity = 14;
            
            degree = 72;
            for (int i = 0; i < 5; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree -= 5, new Rectangle(600, 0, bulletSizeX, bulletSizeY), texture, velocity -= 0.2, time += 0.05f, windowWidth, windowHeight));
            }
            
            foreach (Bullet bullet in bulletList)
            {
                if (bullet.OutScreen == true)
                {
                    bulletList.Remove(bullet);
                }
            }
            
            
            
            
            
            
            
            // 3 bullet pattern
            degree = 130;
            degree2 = 130 * 2;
            degree3 = 0;
            
            velocity = 10;
            yPosition = -100;
            for (int i = 0; i < 30; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(1000, yPosition+=10, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
                bulletList.Add(bulletss1 = new Bullet(degree2 += 20, new Rectangle(1000, yPosition += 10, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
                bulletList.Add(bulletss1 = new Bullet(degree3 += 20, new Rectangle(1000, yPosition += 10, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
            
            }
            time -= 2f;
            yPosition = 0;
            for (int i = 0; i < 30; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(400, yPosition += 10, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
                bulletList.Add(bulletss1 = new Bullet(degree2 += 20, new Rectangle(400, yPosition += 10, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
                bulletList.Add(bulletss1 = new Bullet(degree3 += 20, new Rectangle(400, yPosition += 10, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
            }
            foreach (Bullet bullet in bulletList)
            {
                if (bullet.OutScreen == true)
                {
                    bulletList.Remove(bullet);
                }
            }

          //  4 bullet pattern

            xPosition = 0;
            yPosition = -50;
            int tempDegree = 0;
            float deltaTime = 0f;
            for (int i = 0; i < 13; i++)
            {
                xPosition=rngNumber.Next(100, 1800);
                degree = rngNumber.Next(-30, 60);
                tempDegree = degree;
                for (int a = 0; a < 5; a++)
                {
                    degree = tempDegree;
                    bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.05f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.05f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.05f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.05f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.05f, windowWidth, windowHeight));   
                }
                deltaTime += 0.05f;
                time -= deltaTime;
            }
            
            foreach (Bullet bullet in bulletList)
            {
                if (bullet.OutScreen == true)
                {
                    bulletList.Remove(bullet);
                }
            }
            
           // 5 sin wave pattern
            xPosition = -280;
            for (int i = 0; i < 7; i++)
            {
                
                xPosition +=(rngNumber.Next(0,100))+300;
                degree = 90;
                for (int a = 0; a < 40; a++)
                { 
                    bulletList.Add(bulletss1 = new Bullet(degree, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.1f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.1f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.1f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.1f, windowWidth, windowHeight));
                    bulletList.Add(bulletss1 = new Bullet(degree, new Rectangle(xPosition, yPosition, bulletSizeX, bulletSizeY), texture, 10, time += 0.1f, windowWidth, windowHeight));
                }
                
                time -= 9.3f;
            }


        }
        public List<Bullet> BulletList
        {
            get
            {
                return bulletList;
            }
        }

    }
   
    
}