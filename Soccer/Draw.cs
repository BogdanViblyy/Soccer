using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer
{
    public class Draw
    {
        public void DrawPoint(int x, int y, char sym)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        public void DrawStadium(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {

                Console.SetCursorPosition(i, 0);
                Console.Write('=');

            }

            for (int i = 0; i < height; i++)
            {

                Console.SetCursorPosition(0, i);
                Console.Write('|');

            }
            for (int i = 0; i < width; i++)
            {

                Console.SetCursorPosition(i, height);
                Console.Write('=');

            }

            for (int i = 0; i < height; i++)
            {

                Console.SetCursorPosition(width, i);
                Console.Write('|');

            }

        }
        public void DrawTekst(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }




    }

}
