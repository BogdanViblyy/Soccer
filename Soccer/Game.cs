using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer
{
    public class Game
    {
        public Team HomeTeam { get; } //команда 1
        public Team AwayTeam { get; } //команда 2
        public Stadium Stadium { get; } //стадион
        public Ball Ball { get; private set; } //мяч


        public Game(Team homeTeam, Team awayTeam, Stadium stadium) //конструктор 
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this;
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this); //мяч по центру 
            HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height); //делим поле пополам
            AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);


        }
        private (double, double) GetPositionForAwayTeam(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y); //расстояние от команды
        }

        public (double, double) GetPositionForTeam(Team team, double x, double y) //задать позиции игрокам
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
        }

        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y);
        }

        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == HomeTeam) //если команда 1
            {
                Ball.SetSpeed(vx, vy); //первая половина поля
            }
            else //если вторая комнда
            {
                Ball.SetSpeed(-vx, -vy); //2 половина
            }
        }

        public void Move() //движение 
        {
            HomeTeam.Move(); //командлы 1
            AwayTeam.Move(); //2
            Ball.Move(); //мяча
            Draw draw = new Draw();
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var player in HomeTeam.Players)
            {

                draw.DrawPoint(((int)player.X), ((int)player.Y), player.Sym);

            }
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var player in AwayTeam.Players)
            {

                draw.DrawPoint(((int)player.X), ((int)player.Y), player.Sym);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            draw.DrawPoint(((int)Ball.X), ((int)Ball.Y), '0');
            Console.ResetColor();
        }
    }
}
