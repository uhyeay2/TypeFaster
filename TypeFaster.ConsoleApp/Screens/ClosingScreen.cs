using TypeFaster.ConsoleApp.Abstraction;
using TypeFaster.ConsoleApp.Display;
using TypeFaster.ConsoleApp.Display.Components;

namespace TypeFaster.ConsoleApp.Screens
{
    internal class ClosingScreen : GameScreen
    {
        private readonly ConsoleColor _backgroundColor = ConsoleColor.Black;

        private readonly ConsoleColor _fontColor = ConsoleColor.Red;

        public override void PresentScreen()
        {
            var message = new DisplayedLine("Thanks for playing!", _fontColor, _backgroundColor);

            var xPosition = (Console.WindowWidth - message.Width) / 2;
            var yPosition = Console.WindowHeight / 2;

            message.UpdatePosition(xPosition, yPosition);

            UserInterface.Display(new DisplayedFill(backgroundColor: ConsoleColor.Black));

            UserInterface.Display(message);

            Console.ReadKey();            
        }
        
        public override GameScreen GetNextScreen()
        {
            return this;
        }
    }
}
