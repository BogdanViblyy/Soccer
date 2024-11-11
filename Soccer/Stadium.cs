using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer
{
    public class Stadium

    {

        public int Width { get; }

        public int Height { get; }

        public Stadium(int width, int height) 
        {
            Width = width; 
            Height = height; 
        }



        public bool IsIn(double x, double y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
        public int GetStadiumWidth()
        {
            return Width;
        }

    }
}
