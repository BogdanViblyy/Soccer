using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer
{
    public class Player
    {
        //поля и атрибуты
        public string Name { get; } //имя игрока
        public char Sym { get; }
        public double X { get; private set; } //x кордината игрока
        public double Y { get; private set; } //y кордината игрока
        private double _vx, _vy; //расстрояние мяча и игрока
        public Team? Team { get; set; } = null; //команда где играет игрок

        private const double MaxSpeed = 5;//максимальная скорость
        private const double MaxKickSpeed = 15; //максимальная скорость удара
        private const double BallKickDistance = 3; //расстрояние удара

        private Random _random = new Random(); //случайное значение

        //конструкторы
        public Player(string name, char sym) //зависит от строки и слово приравневается к Name
        {
            Name = name;
            Sym = sym;

        }

        public Player(string name, double x, double y, Team team) //игрок на поле
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        public void SetPosition(double x, double y) //метод- задать кординаты
        {
            X = x;
            Y = y;
        }

        public (double, double) GetAbsolutePosition() //
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        public double GetDistanceToBall() //
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void MoveTowardsBall() //путь к мечу
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio;
            _vy = dy / ratio;
        }

        public void Move() //метод передвижения
        {
            if (Team.GetClosestPlayerToBall() != this) //если ближайшая позиция к мячу не равна нынешней, то расстояние 0
            {
                _vx = 0;
                _vy = 0;


            }

            if (GetDistanceToBall() < BallKickDistance) //если расстояние до мяча меньше дистанции удара, то задать скорость 
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.5)
                    );
                this.Team.Score++;



            }

            var newX = X + _vx;
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2)) //если мяч за полем
            {
                X = newX; //если да то новая позиция 
                Y = newY;

            }
            else
            {
                _vx = _vy = 0;//если нет то он остается там же где и стоял

            }
        }
    }
}
