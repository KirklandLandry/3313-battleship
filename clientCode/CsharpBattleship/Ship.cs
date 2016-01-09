using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpBattleship
{
    class Ship
    {
        // LOGIC //
        // each gridsquare will contain a ship initialized to all false
        // when placing a ship each part of the ship will be initialized with the appropriate
        // information ie:
        // a ship of 3 tiles placed vertically will initialize to
        // 1st part: down = true
        // 2nd part: down = true, up = true
        // 3rd part: up = true
      



        public bool up;
        public bool down;
        public bool left;
        public bool right;

        public Ship()
        {
            up = false;
            down = false;
            left = false;
            right = false;
        }

    }
}
