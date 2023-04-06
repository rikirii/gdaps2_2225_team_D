using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

        public BulletManager(GraphicsDeviceManager graphics, int windowWidth, int windowHeight, Dictionary<Entity, Texture2D> entityAssests )
        {
            this._graphics = graphics;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.entityAssests = entityAssests;

            //temp set bulletlist to empty
            this.bulletList = new List<Bullet>();
        }

        /// <summary>
        /// you can do any loading bullet here
        /// </summary>
        /// <param name="texture"></param>
        public void LoadBulletFile( Texture2D texture )
        {
            //temp
            HardCodeBulletTest test = new HardCodeBulletTest(texture);
            bulletList = test.BulletList;

            //bulletList.Add(new Bullet(  (float)((Math.PI / 180) * 90), new Rectangle(1000, 50, 50, 50), texture, 1.0, 0, windowWidth, windowHeight));
        }


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

                    foreach (Bullet bullet in bulletList)
                    {
                        bullet.Update(currentGameTime);
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


                    break;

                default:
                    break;
            }
            
        }


    }
}
