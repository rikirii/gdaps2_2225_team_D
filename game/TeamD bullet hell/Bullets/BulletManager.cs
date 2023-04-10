using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamD_bullet_hell.Bullets
{
    /// <summary>
    /// 
    /// </summary>
    internal class BulletManager
    {
        //random
        private Random rng;

        //Current game state
        internal GameState currentGameState;

        //graphicsdevicemanager ref from game1
        internal GraphicsDeviceManager _graphics;

        //screen size
        internal int windowWidth;
        internal int windowHeight;

        //bullets assests
        internal Dictionary<Entity, Texture2D> entityAssests;


        //this store the bullet of all the level into different list
        private List<List<Bullet>> levelBulletList;

        //time managment? Gametime
        internal float currentGameTime;

        //Random varibale for a random spwan
        private Random r;

        //2D array for the bullets to go in
        private Bullet[,] bulletArray;

        private List<string> fileNameList;
        private string fileName;

        //to test out the system so be set to 1 
        private int lvlCount;

        //to keep track when the bullet list is used up
        private int bulletCount;

        //to track and determine which/when to reset the bullet in the list.
        private int bulletUsed;
        private List<Bullet> prevBulletList;
        

        //if current list has finished
        private bool listFinished;

        /// <summary>
        /// get the bullet of all the level
        /// </summary>
        public List<List<Bullet>> LevelBulletList
        {
            get { return levelBulletList; }
        }

        public List<Bullet> CurrentBulletList
        {
            get;set;
        }

        /// <summary>
        /// Get reference to screen manager to use rescale
        /// </summary>
        public ScreenManager ScreenMgr
        {
            get; set;

        }


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

        public BulletManager(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Dictionary<Entity, Texture2D> entityAssests)
        {
            this._graphics = graphics;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.entityAssests = entityAssests;
            this.listFinished = false;
            this.bulletCount = 0;

            this.prevBulletList = null;

            this.rng = new Random();
  
            levelBulletList = new List<List<Bullet>>();
            r = new Random();
            LoadBulletFile(entityAssests[0]);
        }


        /// <summary>
        /// reset the bullet 
        /// </summary>
        public void Reset(Texture2D texture)
        {
            //temp
            // bulletList = test.BulletList;
            //HardCodeBulletTest test = new HardCodeBulletTest(texture, windowWidth, windowHeight);
            
            this.bulletArray = new Bullet[11, 11];
            levelBulletList = new List<List<Bullet>>();
            currentGameTime = 0;
            LoadBulletFile(texture);

        }


        /// <summary>
        /// The load bullet file will load in all the level (from 1-5 for now) at once and save each level into LevelBulletList
        /// ex LevelBulletList[0] contain the level 1 buullet list 
        /// LevelBulletList[1] contain the level 2 bullet list
        /// </summary>
        /// <param name="texture"></param>
        public void LoadBulletFile(Texture2D texture)
        {
            //temp
            //HardCodeBulletTest test = new HardCodeBulletTest(texture, windowWidth, windowHeight);
            //bulletList = test.BulletList;
            StreamReader input = null;
            List<Bullet> tempBulletList = new List<Bullet>();

            //the variable that is going to be used to creat a bullet
            float angle;
            double velocity;
            double deltaSpawTime;
            int position;
            double spawnTime = 0;


            //only 5 level for now
            for (int levelCount=0;levelCount<5;levelCount++)
            {
                //reset the list and the spawnTime
                tempBulletList = (new List<Bullet>());
                levelBulletList.Add(new List<Bullet>());
                spawnTime = 0;
                try
                {
                    //readin the file fallowing the naming convention
                    input = new StreamReader($"../../../level{levelCount+1}.csv");

                   

                    //create a string to bring the data in and loop while the line has data 
                    string line = null;

                    //reset the temp Bullet List after finish reading this level
                    tempBulletList = new List<Bullet>();

                    //read to the end
                    while (!input.EndOfStream)
                    {
                        //read a line every single row
                        line = input.ReadLine();
                        System.Diagnostics.Debug.WriteLine(line);
                        //this will replace all the space in the file
                        line = Regex.Replace(line, @"\s+", "");
                        //reset the variable we are going to use
                        position = 20;
                        angle = 0;
                        velocity = 0;
                        deltaSpawTime = 0;

                        //make a string array for each row
                        string[] row = line.Split(',');

                        //the first three number in the file will set the angle velocity and difference in Spawn time
                        //if it is not a valid number or null set it to defalut (all three variable)
                        if (!float.TryParse(row[0], out angle) || row[0] == null)
                        {
                            angle = 90;
                        }


                        if (!double.TryParse(row[1], out velocity) || row[1] == null)
                        {
                            velocity = 10.0f;
                        }

                        if (!double.TryParse(row[2], out deltaSpawTime) || row[2] == null)
                        {
                            deltaSpawTime = 0;
                        }



                        for (int j = 3; j < 14; j++)
                        {

                            //if the element in the string array is an X, then make a new bullet at that spot in the array
                            if (char.Parse(row[j]) == 'X')
                            {
                                //IF YOU WANT TO CHANGE WHERE THE BULLET CAN SPAWN IT WILL BE HERE:
                                //IN NEW RECTANGLE, THE X AND Y VARIABLES

                                tempBulletList.Add(new Bullet(angle, new Rectangle(position, 0,
                                    100, 100), texture, velocity, spawnTime = spawnTime + deltaSpawTime, windowWidth, windowHeight));
                                //System.Diagnostics.Debug.WriteLine(deltaSpawTime);
                            }
                            else if (char.Parse(row[j]) == '-')
                            {
                                // do not do any thing to the list
                            }
                            //the bullet will be even spreat onto the map
                            position += 1800 / 11;
                        }
                        spawnTime += 0.5f;

                    }
                }
                catch (Exception e)
                {
                    //print this bullet to show we cant find the file
                    //also the bullet here will prevent the list to be null and cause problem
                    //reset the list to get ride of previous data
                    tempBulletList = (new List<Bullet>());
                    tempBulletList.Add(new Bullet(90, new Rectangle(windowWidth/2, windowHeight/2, 100, 100), texture, 10, 0, windowWidth, windowHeight));
                   
                    System.Diagnostics.Debug.WriteLine("Cant find the file!" + e.Message);
                }

                //save the current list into the list of 
                levelBulletList[levelCount] = (tempBulletList);




                if (input != null)
                {
                    input.Close();
                }
            }   
        }
        //bulletList.Add(new Bullet(  (float)((Math.PI / 180) * 90), new Rectangle(1000, 50, 50, 50), texture, 1.0, 0, windowWidth, windowHeight));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="levelNumber"></param>
        public void Update(GameTime gameTime)
        {
            if (bulletCount == 0 )
            {
                //get the bullet list for the level

                this.lvlCount = rng.Next(0, this.levelBulletList.Count - 1);

                this.CurrentBulletList = levelBulletList[this.lvlCount];

                this.bulletCount = CurrentBulletList.Count;

                
            }
            

            switch (currentGameState)
            {
                case GameState.Levels:
                    break;

                case GameState.Infinity:

                    //for the timer of bullet to see is it the time to spawn the bullet
                    //remenber to reset the time after each game ! No code for that right now
                    currentGameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds);

                    for ( int bullet = 0; bullet < this.CurrentBulletList.Count; bullet ++)
                    {
                        CurrentBulletList[bullet].Update(currentGameTime);
                        
                        if(CurrentBulletList[bullet].OutScreen)
                        {
                            bulletCount--;
                            bulletUsed++;
                        }

                        //if (prevBulletList != null)
                        //{
                        //    prevBulletList[bullet].OutScreen = false;
                        //}
                    }

                    if (bulletCount == 0)
                    {
                        prevBulletList = this.CurrentBulletList;
                    }

                    break;



            }


        }

        /// <summary>
        /// dont need to start with 0 the level with start with 1
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="levelNumber"></param>
        public void Draw(SpriteBatch spriteBatch)
        {

            //get the bullet list for the level
            //List<Bullet> tempBulletList = new List<Bullet>();
            //tempBulletList = LevelBulletList[lvlCount-1];

            switch (currentGameState)
            {

                case GameState.Levels:
                    break;

                case GameState.Infinity:

                    foreach (Bullet bullet in this.CurrentBulletList)
                    {
                        bullet.Draw(spriteBatch);
                    }


                    break;

                default:
                    break;
            }

        }
    }
}


