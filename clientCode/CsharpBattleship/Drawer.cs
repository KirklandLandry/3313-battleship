using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;//for panel
using System.Drawing;//for graphics

namespace CsharpBattleship
{
    class Drawer
    {
        private Graphics g;
        public DrawerUtensils with;//makes syntax easy and looks nice. for brushes and pens
        private int xGridSquareSize;
        private int yGridSquareSize;



        public Drawer(Panel panel, int _xGridSquareSize, int _yGridSquareSize)
        {
            g = panel.CreateGraphics();
            xGridSquareSize = _xGridSquareSize;
            yGridSquareSize = _yGridSquareSize;
            with = new DrawerUtensils();
        }

        public void Ellipse(Pen pen, GridSquare currentGridsquare, int yOffset, int xOffset)
        {
            g.DrawEllipse(pen, (currentGridsquare.min.X + (xGridSquareSize * xOffset) + 1), (currentGridsquare.min.Y + (yGridSquareSize * yOffset) + 1), xGridSquareSize - 2, yGridSquareSize - 2);
        }
        public void FilledEllipse(Brush brush, GridSquare currentGridsquare, int yOffset, int xOffset)
        {
            g.FillEllipse(brush, (currentGridsquare.min.X + (xGridSquareSize * xOffset) + 1), (currentGridsquare.min.Y + (yGridSquareSize * yOffset) + 1), xGridSquareSize - 2, yGridSquareSize - 2);
        }
        public void FilledRectangle(Brush brush, GridSquare currentGridsquare, int yOffset, int xOffset)
        {
            g.FillRectangle(brush, (currentGridsquare.min.X + (xGridSquareSize*xOffset)+ 1), (currentGridsquare.min.Y + (yGridSquareSize * yOffset) + 1), xGridSquareSize - 1, yGridSquareSize - 1);
        }

        // These 2 are specifically for drawing the grid
        public void VerticalLine(Pen pen, Point panelSize, int offset)
        {
            g.DrawLine(pen, new Point(offset * (panelSize.X / 10), 0), new Point(offset * (panelSize.X / 10), panelSize.Y));
        }
        public void HorizontalLine(Pen pen, Point panelSize, int offset)
        {
            g.DrawLine(pen, new Point(0, offset * (panelSize.Y / 10)), new Point(panelSize.X, offset * (panelSize.Y / 10)));
        }
    }
}
