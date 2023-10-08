using System;

enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "🐍 Snake";

            Menu _menu = new Menu();
            Settings _settings = new Settings(8,4, 2); //larger speed value => faster

            while (true)
            {
                Game game = new Game(_settings.Width, _settings.Height, _settings.Speed);
                int selection = _menu.Display();
                if (selection == 1)
                    game.Run();
                else if (selection == 2)
                {
                    int setSel = 0;
                    _settings.SetReset();
                    while (setSel < 3 || setSel > 4)
                    {
                        setSel = _settings.Display();
                        switch (setSel)
                        {
                            case 1:
                                _settings.SizeSetting();
                                break;
                            case 2:
                                _settings.SpeedSetting();
                                break;
                            case 3:
                                _settings.Reset();
                                break;
                        }
                    }
                }
                else if (selection == 3) break;
            }

            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }
}
