using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;//for pens and brushes

namespace CsharpBattleship
{
    class DrawerUtensils
    {
        // having all the pens in one place saves the trouble of recreating them all the time
        // consider moving them to their own class called Drawing ustensils and have the instance variable called using
        // so it would write like draw.with.whiteBrush or something
        // yeah, I did that.
        public System.Drawing.SolidBrush whiteBrush = new System.Drawing.SolidBrush(SystemColors.Control);
        public System.Drawing.SolidBrush greenBrush = new System.Drawing.SolidBrush(Color.Green);
        public System.Drawing.SolidBrush redBrush = new System.Drawing.SolidBrush(Color.Red);

        public Pen redPen = new Pen(Color.Red);
        public Pen greenPen = new Pen(Color.Green);
        public Pen blackPen = new Pen(Color.Black);
    }
}
