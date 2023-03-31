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

        ////////////////////////////
        ///test code for the bullet pattern 
        private int windowHeight = 1080;
        private int windowWidth = 1920;

        private Texture2D textureOfBullet;
        private List<Bullet> bulletList = new List<Bullet>();

        private Rectangle a;


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
            for (int i = 0; i < 100; i++)
            {
                bulletList.Add(bulletss1 = new Bullet(degree += 20, new Rectangle(1000, 400, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
                bulletList.Add(bulletss1 = new Bullet(degree2 += 20, new Rectangle(1000, 400, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
                bulletList.Add(bulletss1 = new Bullet(degree3 += 20, new Rectangle(1000, 400, 100, 100), texture, 10, time += 0.05f, windowWidth, windowHeight));
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