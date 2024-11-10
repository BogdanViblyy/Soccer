using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer
{
    public class Ball
    {

        public double X { get; private set; }
        public double Y { get; private set; }
        private double _vx, _vy;//скорость мяча


        private Game _game; //связка с игрой

        public Ball(double x, double y, Game game)//констуртор 
        {
            _game = game;
            X = x;
            Y = y;
        }

        public void SetSpeed(double vx, double vy) //задаем скорость
        {
            _vx = vx;
            _vy = vy;
        }

        public void Move() //дживежие 
        {
            double newX = X + _vx; //новые кординаты(к текущей прибавляем)
            double newY = Y + _vy;
            if (_game.Stadium.IsIn(newX, newY)) //задаем новые кординаты если мяч в поле

            {
                X = newX; //значения присваиваются  к xy
                Y = newY;

            }
            else
            {
                _vx = 0; //если улетел за поле, то кординаты мяча 0
                _vy = 0;
                _game.Start();
                Draw draw = new Draw();


                Console.ForegroundColor = ConsoleColor.Red;
                draw.DrawTekst(_game.Stadium.GetStadiumWidth() + 20, 3, "Aut, pall keskele!");
                Console.ResetColor();

            }
        }

    }
}
