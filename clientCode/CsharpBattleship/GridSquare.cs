using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace CsharpBattleship
{
    class GridSquare
    {
        public Point min;
        public Point max;

        public int size;
        //public int rotation;//going to have to account for rotation when clearing and doing stuff

        //change these to an enum or something
        public bool ok;
        public bool set; // true means a ship resides in this square


        //true means this spot has been shot at and can no longer be selected
        public bool shotAt;

        // need a way to keep track of other squares that are attatched 
        // this could be handeled in the main part
        public Ship ship;

        private int shipCode;

        public GridSquare(Point _min, Point _max)
        {
            min = _min;
            max = _max;
            ok = false;
            set = false;
            shotAt = false;
            ship = new Ship();
            size = -1;// -1 means size is undefined
        }

        //use this to set the status
        private enum status
        {
            //status could be empty, unhit, hit
        }

        public void setShipCode (int code)
        {
            shipCode = code;
            Console.WriteLine("Set a ship with a code of " + shipCode);
        }

        public int getShipCode()
        {
            return shipCode;
        }

    }
}
