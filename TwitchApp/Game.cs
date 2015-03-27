using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchApp
{
    public class Game
    {
        private String mGame;

        public String GameName
        {
            get { return mGame; }
            set { mGame = value; }
        }
        private int mViewers;

        public int Viewers
        {
            get { return mViewers; }
            set { mViewers = value; }
        }
        private int mChannels;

        public int Channels
        {
            get { return mChannels; }
            set { mChannels = value; }
        }
        private String mLogo;
        public String Logo
        {
            get { return mLogo; }
            set { mLogo = value; }
        }
        public Game(String gameName, int viewers, int channels,String logo)
        {
            mGame = gameName;
            mViewers = viewers;
            mChannels = channels;
            mLogo = logo;
        }
    }
}
