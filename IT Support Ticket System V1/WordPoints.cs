
////////////////////////////////////
//Developer & Owner: Matthew Hyndman
////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Support_Ticket_System_V1
{
    class WordPoints
    {
        private string word;
        private int points;

        //Deafult Constructor
        public WordPoints()
        {
            this.word = "defult";
            this.points = 0;
        }

        //Custom Constructor
        public WordPoints(string word)
        {
            this.word = word;
            this.points = 1;
        }

        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        //increment points value by 1
        public void Increment()
        {
            this.points++;
        }
    }
}
