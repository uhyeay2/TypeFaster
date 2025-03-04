using System.Diagnostics;
using TypeFaster.ConsoleApp.Abstraction;
using TypeFaster.ConsoleApp.Display;
using TypeFaster.ConsoleApp.Display.Components;
using TypeFaster.ConsoleApp.ExtensionMethods;

namespace TypeFaster.ConsoleApp.Screens
{
    public class OpeningScreen : GameScreen
    {
        private readonly ConsoleColor _fontColor = ConsoleColor.DarkRed;
        private readonly ConsoleColor _fontBackground = ConsoleColor.Black;

        private readonly ConsoleColor _backgroundColor = ConsoleColor.DarkGreen;

        private readonly Stopwatch _stopwatch = new();
        private readonly int _millisecondsBetweenRefresh = 222;

        public override void PresentScreen()
        {
            var xDirection = 2;
            var yDirection = 1;

            var boxWidth = 30;
            var boxHeight = 7;

            var movingBox = new DisplayedParagraph(boxWidth)
                .Add("Welcome!".PadToCenter(boxWidth), _fontColor, _fontBackground)
                .AddLine(_fontBackground)
                .Add("Press any key to continue!".PadToCenter(boxWidth), _fontColor, _fontBackground);

            movingBox.Contents.CenterVertical(7, _fontBackground);

            var eraser = new DisplayedBlock().Add(DisplayedFill.Lines(_backgroundColor, boxWidth, boxHeight));

            UserInterface.Display(new DisplayedFill(backgroundColor: _backgroundColor));
            UserInterface.Display(movingBox);
            
            _stopwatch.Start();

            while (!Console.KeyAvailable)  
            {
                if (_stopwatch.Elapsed.TotalMilliseconds >= _millisecondsBetweenRefresh)
                {
                    _stopwatch.Restart();

                    if (movingBox.PositionX + movingBox.Width >= Console.WindowWidth)
                    {
                        xDirection = -2;
                    }
                    else if (movingBox.PositionX <= 0)
                    {
                        xDirection = 2;
                    }

                    if (movingBox.PositionY + movingBox.Contents.Height >= Console.WindowHeight)
                    {
                        yDirection = -1; 
                    }
                    else if (movingBox.PositionY <= 0)
                    {
                        yDirection = 1;
                    }

                    eraser.UpdatePosition(movingBox.PositionX, movingBox.PositionY);

                    UserInterface.Display(eraser);

                    movingBox.Contents.UpdatePosition(movingBox.PositionX + xDirection, movingBox.PositionY + yDirection);

                    UserInterface.Display(movingBox);
                }
            }

            Console.ReadKey(false);
            Console.Clear();
        }

        public override GameScreen GetNextScreen()
        {
            return new ClosingScreen();
        }
    }
}
