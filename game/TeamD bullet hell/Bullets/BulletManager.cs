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

        internal int bulletSizeX;
        internal int bulletSizeY;

        //bullets assests
        internal Dictionary<Entity, Texture2D> entityAssests;


        //this store the bullet of all the level into different list
        private List<List<Bullet>> levelBulletList;
        private List<Bullet> currentBulletList;

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

        private bool changeState;


        /// <summary>
        /// get the bullet of all the level
        /// </summary>
        public List<List<Bullet>> LevelBulletList
        {
            get { return levelBulletList; }
        }

        public List<Bullet> CurrentBulletList
        {
            get{ return currentBulletList; }
            set{ currentBulletList = value; }
        }

        /// <summary>
        /// Get reference to screen manager to use rescale
        /// </summary>
        public ScreenManager ScreenMgr
        {
            get; set;

        }


        public int Level
        {
            get;set;
        }
        public bool ChangeState
        {
            get { return changeState; }
            set { changeState=value;}
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
            this.bulletCount = 0;

            this.bulletSizeX = 70;
            this.bulletSizeY = 70;


            this.rng = new Random();
  
            levelBulletList = new List<List<Bullet>>();
            r = new Random();
            LoadBulletFile(entityAssests[Entity.Bullet]);
        }
        /// <summary>
        /// it reurn true whne there is no bullet on screne
        /// </summary>
        public bool NoBulletOnScreen(List<Bullet> bulletList)
        {
            //watch out! the number of bullet list will change so we need a total bullet NUmber
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update(currentGameTime);
                // if the bullet is no longer on screen
                if (CurrentBulletList[i].OutScreen && !bulletList[i].UpDateTheBall)
                {
                    bulletList.Remove(bulletList[i]);
                    bulletUsed++;
                    //System.Diagnostics.Debug.WriteLine(bulletCount);
                }
            }
            //if the bullet are all used then reset the level
            if (bulletUsed >= bulletCount)
            {                
                bulletUsed = 0;
                currentGameTime = 0;
                return true;
            }
            return false; 
        }
        /// <summary>
        /// reset the bullet 
        /// </summary>
        public void Reset(Texture2D texture)
        {
            //Some crazy level
            // bulletList = test.BulletList;
            //HardCodeBulletTest test = new HardCodeBulletTest(texture, windowWidth, windowHeight);
            //reset
            this.bulletArray = new Bullet[11, 11];
            levelBulletList = new List<List<Bullet>>();
            currentGameTime = 0;
            bulletCount = 0;
            bulletUsed = 0;
            
            CurrentBulletList = null;
            LoadBulletFile(texture);

        }
        /// <summary>
        /// tpye in the level ex.1 you will get the bullet for level one
        /// </summary>
        /// <param name="level"></param>
        public void LoadLevelMode(int level)
        {
            //reset
            bulletCount = 0;
            CurrentBulletList = null;
            Reset(entityAssests[Entity.Bullet]);

            //load in current level
            CurrentBulletList = LevelBulletList[(level)];
            bulletCount = CurrentBulletList.Count;
        }
        
        /// <summary>
        /// help to reload the level when the level was played during infinty mode 
        /// </summary>
        public void LoadLevelForInfinity()
        {
            //reset
            bulletCount = 0;
            CurrentBulletList = null;
            Reset(entityAssests[Entity.Bullet]); 


            int currentLevel = r.Next(0, lvlCount);
            CurrentBulletList = levelBulletList[currentLevel];
            bulletCount = CurrentBulletList.Count;
            //check the miss file to prevent only one bullet appear in the infinity mode
            CheckMissingLevelFile();
        }

        /// <summary>
        /// It will kick the level with the single special bullet when we cant find the file
        /// </summary>
        /// <returns></returns>
        public void CheckMissingLevelFile()
        {
            int count = levelBulletList.Count();
            for (int i=0; i < count;i++)
            {
                //if the file have the special bullet it is mean we cant find that level file
                if (levelBulletList[i][0] == new Bullet(90, new Rectangle(windowWidth / 2, 20, bulletSizeX, bulletSizeY), entityAssests[Entity.Bullet], 10, 0, windowWidth, windowHeight))
                {
                    levelBulletList.RemoveAt(i);
                    i--;
                }
            }
            //update the counts
            this.lvlCount = levelBulletList.Count;
        }

        /// <summary>
        /// The load bullet file will load in all the level (from 1-5 for now) at once and save each level into LevelBulletList
        /// ex LevelBulletList[0] contain the level 1 buullet list 
        /// LevelBulletList[1] contain the level 2 bullet list
        /// </summary>
        /// <param name="texture"></param>
        public void LoadBulletFile(Texture2D texture)
        {
            
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
                this.lvlCount= levelCount;
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
                                    bulletSizeX, bulletSizeY), texture, velocity, spawnTime = spawnTime + deltaSpawTime, windowWidth, windowHeight));
                                //System.Diagnostics.Debug.WriteLine(deltaSpawTime);
                            }

                            //else if (char.Parse(row[j]) == '-')
                            //{
                            //    // do not do any thing to the list
                            //}
                            //(we put this line here to make understanding code easier)

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
                    tempBulletList.Add(new Bullet(90, new Rectangle(windowWidth/2, 20, bulletSizeX, bulletSizeY), texture, 10, 0, windowWidth, windowHeight));
                   
                    //System.Diagnostics.Debug.WriteLine("Cant find the file!" + e.Message);
                }

                //save the current list into the list of 
                levelBulletList[levelCount] = (tempBulletList);




                if (input != null)
                {
                    input.Close();
                }
            }
            /////////////////////////////////////////////////////////////////////////// uncomment this for fun
            // //for some fun!!!!!
            //HardCodeBulletTest hardCodeBulletTest = new HardCodeBulletTest(texture, windowWidth, windowHeight);
            //levelBulletList[0] = hardCodeBulletTest.BulletList;


             
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="levelNumber"></param>
        public void Update(GameTime gameTime)
        {
           
            

            switch (currentGameState)
            {
                case GameState.Levels:

                    //use NobulletOnScreen to help you find the situation when there is no bullet 

                    // use NoBulletOnScreen to load the bullet level

                    //for the timer of bullet to see is it the time to spawn the bullet
                    //remenber to reset the time after each game ! No code for that right now
                    currentGameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds);

                    //System.Diagnostics.Debug.WriteLine(bulletUsed);

                    //if nothing is in the bullet list now load in level 1
                    if (CurrentBulletList == null)
                    {
                        LoadLevelMode(this.Level - 1);
                    }
                    //cant use for each here other wise is one of the bullet move out of the screen it will keep add the bullet Used
                    

                   if(NoBulletOnScreen(CurrentBulletList))
                   {
                       changeState = true;
                       
                   }

                    break;

                case GameState.Infinity:

                    //for the timer of bullet to see is it the time to spawn the bullet
                    //remenber to reset the time after each game ! No code for that right now
                    currentGameTime += (float)(gameTime.ElapsedGameTime.TotalSeconds);

                    //if nothing is in the bullet list now load in level 1
                    if(CurrentBulletList==null)
                    {
                        bulletCount = levelBulletList[0].Count;
                        CurrentBulletList = levelBulletList[0];
                    }
                    //cant use for each here other wise is one of the bullet move out of the screen it will keep add the bullet Used
                    if(NoBulletOnScreen(CurrentBulletList))
                    {
                        LoadLevelForInfinity();
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

            switch (currentGameState)
            {

                case GameState.Levels:

                    foreach (Bullet bullet in this.CurrentBulletList)
                    {
                        bullet.Draw(spriteBatch);
                    }

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


