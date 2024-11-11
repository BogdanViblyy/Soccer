using System;
using System.Collections.Generic;
using System.Threading;

namespace Soccer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            List<(string, ConsoleColor)> teams = new List<(string, ConsoleColor)>
            {
                ("Real Madrid", ConsoleColor.White),
                ("Barcelona", ConsoleColor.DarkBlue),
                ("Manchester United", ConsoleColor.Red),
                ("Manchester City", ConsoleColor.Cyan),
                ("Juventus", ConsoleColor.Black),
                ("Borussia Dortmund", ConsoleColor.Yellow)
            };

            Console.WriteLine("Select two different teams for the game:");

            Team team1 = SelectTeam(teams, "first");
            Team team2;

            do
            {
                team2 = SelectTeam(teams, "second");
                if (team2.Name == team1.Name)
                {
                    Console.WriteLine("Please select a different team for the second choice.");
                }
            } while (team2.Name == team1.Name);

            Console.WriteLine("Choose a stadium type:");
            Console.WriteLine("1. Indoor Stadium (40 x 20 meters)");
            Console.WriteLine("2. Outdoor Stadium (60 x 40 meters)");
            Console.WriteLine("3. FIFA Standard Stadium (105 x 68 meters)");

            int width = 0;
            int height = 0;

            int stadiumChoice;
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    stadiumChoice = int.Parse(Console.ReadLine());
                    Console.ResetColor();

                    switch (stadiumChoice)
                    {
                        case 1:
                            width = 40;
                            height = 10;
                            Console.WriteLine("Indoor Stadium selected.");
                            break;
                        case 2:
                            width = 60;
                            height = 15;
                            Console.WriteLine("Outdoor Stadium selected.");
                            break;
                        case 3:
                            width = 80;
                            height = 20;
                            Console.WriteLine("FIFA Standard Stadium selected.");
                            break;
                        default:
                            throw new Exception("Invalid selection. Please choose 1, 2, or 3.");
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }          
            Console.WriteLine("Game duration in seconds:");
            Console.ForegroundColor = ConsoleColor.Blue;
            int duration = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.WriteLine("Game speed (1-3): ");
            Console.ForegroundColor = ConsoleColor.Blue;
            int speed = int.Parse(Console.ReadLine());
            Console.ResetColor();

            int delay = speed switch
            {
                1 => 1000,
                2 => 800,
                3 => 600          
            };

            Console.WriteLine("Press ENTER to start the game.");
            Console.ReadLine();

            Stadium stadium = new Stadium(width, height);
            Game game = new Game(team1, team2, stadium);

            game.Start();

            int CalculateGameTime() => duration * 1000 / delay;

            for (int i = 0; i < CalculateGameTime(); i++)
            {
                Console.Clear();

                Draw draw = new Draw();
                Console.ForegroundColor = ConsoleColor.Yellow;
                draw.DrawStadium(stadium.Width, stadium.Height);
                Console.ResetColor();

                game.Move();

                draw.DrawTekst(width+2, 0, "Score (Number of Ball Hits):");
                Console.ForegroundColor = team1.Color;
                draw.DrawTekst(width+2, 1, $"{team1.Name}: {team1.Score}");
                Console.ForegroundColor = team2.Color;
                draw.DrawTekst(width+2, 2, $"{team2.Name}: {team2.Score}");
                Console.ResetColor();

                Thread.Sleep(delay);
            }

            Console.Clear();
            Console.WriteLine("Game over");
            //Console.WriteLine("Winner:");
            //Console.WriteLine(winner);
            Console.ReadLine();
        }

        static Team SelectTeam(List<(string, ConsoleColor)> teams, string teamOrder)
        {
            Console.WriteLine($"Choose the {teamOrder} team:");
            for (int i = 0; i < teams.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {teams[i].Item1}");
            }

            int choice;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                while (true)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine()) - 1;
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please, enter the number");
                    }
                }
                Console.ResetColor();
            } while (choice < 0 || choice >= teams.Count);

            var (name, color) = teams[choice];
            Team selectedTeam = new Team(name, color);

            for (int i = 1; i <= 11; i++)
            {
                Player player = new Player($"{name} Player {i}", color == ConsoleColor.White ? '$' : '#');
                selectedTeam.AddPlayer(player);
            }

            return selectedTeam;
        }
    }
}
