using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchApp
{
    public class Stream
    {
        private Game streamGame;

        public Game StreamGame
        {
            get { return streamGame; }
            set { streamGame = value; }
        }
        private int viewers;

        public int Viewers
        {
            get { return viewers; }
            set { viewers = value; }
        }
        private String preview;

        public String Preview
        {
            get { return preview; }
            set { preview = value; }
        }
        private String url;

        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        private String status;

        public String Status
        {
            get { return status; }
            set { status = value; }
        }


        private String channel;

        public String Channel
        {
            get { return channel; }
            set { channel = value; }
        }


        public Stream(Game sg,int viewcount,String preview,String url,String status,String channel)
        {
            this.streamGame = sg;
            this.viewers = viewcount;
            this.preview = preview;
            this.url = url;
            this.status = status;
        }

    }
}
