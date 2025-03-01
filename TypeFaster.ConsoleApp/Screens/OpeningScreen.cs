using System.Diagnostics;
using TypeFaster.ConsoleApp.Abstraction;
using TypeFaster.ConsoleApp.Display;
using TypeFaster.ConsoleApp.Display.Components;

namespace TypeFaster.ConsoleApp.Screens
{
    public class OpeningScreen : GameScreen
    {
        private readonly ConsoleColor _fontColor = ConsoleColor.DarkRed;
        private readonly ConsoleColor _fontBackground = ConsoleColor.Black;

        private readonly ConsoleColor _backgroundColor = ConsoleColor.DarkGreen;

        private readonly Stopwatch _stopwatch = new();
        private readonly int _millisecondsBetweenRefresh = 130;

        public override void PresentScreen()
        {
            _stopwatch.Start();

            UserInterface.Display(new DisplayedFill(backgroundColor: _backgroundColor));

            var xPosition = 0;
            var yPosition = 0;

            var xDirection = 1;
            var yDirection = 1;

            var boxWidth = 30;

            var movingBox = new DisplayedBlock(
                new DisplayedLine("           Welcome!           ", _fontColor, _fontBackground)
            ).Prepend(
                DisplayedFill.Lines(_fontBackground, boxWidth, height: 2)
            ).Add(
                new DisplayedLine("  Press any key to continue!  ", _fontColor, _fontBackground)
            )
            .Add(
                DisplayedFill.Lines(_fontBackground, boxWidth, height: 2)
            );

            var eraser = new DisplayedFill(' ', xPosition, yPosition, movingBox.Width, movingBox.Height, _fontColor, _backgroundColor);

            UserInterface.Display(movingBox);
            
            while (!Console.KeyAvailable)  
            {
                if (_stopwatch.Elapsed.TotalMilliseconds >= _millisecondsBetweenRefresh)
                {
                    _stopwatch.Restart();

                    if (xPosition + movingBox.Width >= Console.WindowWidth)
                    {
                        xDirection = -1;
                    }
                    else if (xPosition <= 0)
                    {
                        xDirection = 1;
                    }

                    if (yPosition + movingBox.Height >= Console.WindowHeight)
                    {
                        yDirection = -1;
                    }
                    else if (yPosition <= 0)
                    {
                        yDirection = 1;
                    }

                    eraser.PositionX = xPosition;
                    eraser.PositionY = yPosition;

                    UserInterface.Display(eraser);
                    
                    xPosition = xPosition + xDirection;
                    yPosition = yPosition + yDirection;                    

                    movingBox.UpdatePosition(xPosition, yPosition);
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
