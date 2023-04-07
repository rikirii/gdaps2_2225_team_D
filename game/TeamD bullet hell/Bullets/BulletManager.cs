using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamD_bullet_hell.Bullets
{
    /// <summary>
    /// 
    /// </summary>
    internal class BulletManager
    {


        //Current game state
        internal GameState currentGameState;

        //graphicsdevicemanager ref from game1
        internal GraphicsDeviceManager _graphics;

        //screen size
        internal int windowWidth;
        internal int windowHeight;

        //bullets assests
        internal Dictionary<Entity, Texture2D> entityAssests;

        //BulletList variable for storing bullets
        internal List<Bullet> bulletList;

        //time managment? Gametime
        internal float currentGameTime;

        //Random varibale for a random spwan
        private Random r;

        //2D array for the bullets to go in
        private Bullet[,] bulletArray;

        private string filename;

        /// <summary>
        /// property to update currentGameState
        /// </summary>
        public GameState CurrentGameState
        {
            set
            {
                currentGameState = value;
            }
        }

        public List<Bullet> BulletList
        {
            get { return bulletList; }
        }

        public BulletManager(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Dictionary<Entity, Texture2D> entityAssests)
        {
            this._graphics = graphics;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.entityAssests = entityAssests;
            this.filename = "Bullet Pattern";

           
            //temp set bulletlist to empty
            this.bulletList = new List<Bullet>();
            bulletArray = new Bullet[11, 11];
            r = new Random();
            LoadBulletFile(entityAssests[0], filename);


        }


        /// <summary>
        /// reset the bullet 
        /// </summary>
        public void Reset(Texture2D texture)
        {
            //temp
            HardCodeBulletTest test = new HardCodeBulletTest(texture);
            bulletList = test.BulletList;
            currentGameTime = 0;
            LoadBulletFile(texture, filename);

        }


        /// <summary>
        /// you can do any loading bullet here
        /// </summary>
        /// <param name="texture"></param>
        public void LoadBulletFile(Texture2D texture, string filename)
        {
            //temp
            HardCodeBulletTest test = new HardCodeBulletTest(texture);
            bulletList = test.BulletList;

            float spawnTime = 1;

            StreamReader input = null;
            try
            {
                //and declaring it in the try block
                input = new StreamReader("../../../" + filename + ".csv");

                //create a string to bring the data in and loop while the line has data 
                string line = null;

                for (int i = 0; i < 11; i++)
                {
                    //read a line every single row
                    line = input.ReadLine();

                    //make a string array for each row
                    string[] row = line.Split(',');

                    for (int j = 0; j < 11; j++)
                    {
                        //if the element in the string array is an X, then make a new bullet at that spot in the array
                        if (char.Parse(row[j]) == 'X')
                        {
                            //IF YOU WANT TO CHANGE WHERE THE BULLET CAN SPAWN IT WILL BE HERE:
                            //IN NEW RECTANGLE, THE X AND Y VARIABLES
                            bulletArray[i, j] = new Bullet(r.Next(0, 180), new Rectangle(r.Next(450, 900), r.Next(200, 900),
                                75, 75), texture, 20, spawnTime += 0.5f, 1920, 1080);
                        }
                        else if (char.Parse(row[j]) == '-')
                        {
                            //if not that spot is null
                            bulletArray[i, j] = null;
                        }


                    }

                }


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Uh oh: " + e.Message);
            }

            input.Close();


        }

        //bulletList.Add(new Bullet(  (float)((Math.PI / 180) * 90), new Rectangle(1000, 50, 50, 50), texture, 1.0, 0, windowWidth, windowHeight));



        public void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Levels:
                    break;

                case GameState.Infinity:

                    //for the timer of bullet to see is it the time to spawn the bullet
                    //remenber to reset the time after each game ! No code for that right now
                    currentGameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds);
                    System.Diagnostics.Debug.WriteLine(currentGameTime);
                    foreach (Bullet bullet in bulletList)
                    {
                        bullet.Update(currentGameTime);
                    }

                    for (int i = 0; i < bulletArray.GetLength(0); i++)
                    {
                        //Random velocity for the bullets to move in
                        int xMove = r.Next(2, 7);
                        int yMove = r.Next(2, 7);

                        for (int j = 0; j < bulletArray.GetLength(1); j++)
                        {
                            if (bulletArray[i, j] != null)
                            {
                                //if it is on the left half of the screen, move in the + direction
                                //if on the right, move in the - direction
                                if (bulletArray[i, j].Position.X > (windowWidth / 2))
                                {
                                    bulletArray[i, j].PositionX += xMove;

                                }
                                else
                                {
                                    bulletArray[i, j].PositionX -= xMove;
                                }

                                //if it is on the top half of the screen, move down direction
                                //if on the bottom, move up
                                if (bulletArray[i, j].Position.Y > (windowHeight / 2))
                                {
                                    bulletArray[i, j].PositionY += yMove;

                                }
                                else
                                {
                                    bulletArray[i, j].PositionY -= yMove;

                                }


                            }
                        }
                    }
                    break;



            }


        }


        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentGameState)
            {

                case GameState.Levels:
                    break;

                case GameState.Infinity:

                    foreach (Bullet bullet in bulletList)
                    {
                        bullet.Draw(spriteBatch);
                    }

                    //Draws the bullets based on the 2D array from the file
                    for (int i = 0; i < bulletArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < bulletArray.GetLength(1); j++)
                        {
                            if (bulletArray[i, j] != null)
                            {
                                bulletArray[i, j].Draw(spriteBatch);

                            }


                        }

                    }


                    break;

                default:
                    break;
            }

        }
    }
}

       



