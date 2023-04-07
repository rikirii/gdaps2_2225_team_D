using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamD_bullet_hell
{
    enum MonitorResolution
    {
        P720 = (720/1080),
        P1080 = 1,
        P1440 = (1440 / 1080)
    }

    /// <summary>
    /// Use this for scaling to resolution
    /// </summary>
    internal class ScreenManager
    {
        //fields
        private int windowWidth;
        private int windowHeight;

        

        public ScreenManager(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }


        public int ReScaleWidth(int num)
        {
            return num * (this.windowWidth / 1920); 

        }

        public int ReScaleHeight(int num)
        {
            return num * (this.windowHeight / 1080);
        }

        public int ReScaleX(int num)
        {
            return num + (this.windowWidth - 1920)/2;
        }

        public int ReScaleY(int num)
        {
            return num + (this.windowHeight - 1080) / 2;
        }

    }
}
