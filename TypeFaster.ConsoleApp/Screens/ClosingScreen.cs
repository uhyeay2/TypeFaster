using TypeFaster.ConsoleApp.Abstraction;
using TypeFaster.ConsoleApp.Display;
using TypeFaster.ConsoleApp.Display.Components;
using TypeFaster.ConsoleApp.ExtensionMethods;

namespace TypeFaster.ConsoleApp.Screens
{
    internal class ClosingScreen : GameScreen
    {
        private readonly ConsoleColor _backgroundColor = ConsoleColor.DarkGreen;

        private readonly ConsoleColor _fontBackgroundColor = ConsoleColor.Black;

        private readonly ConsoleColor _fontColor = ConsoleColor.Red;

        public override void PresentScreen()
        {
            var lineWidth = 30;

            var paragraph = new DisplayedParagraph(lineWidth)
                .Add("Thank you for playing!".PadToCenter(lineWidth), _fontColor, _fontBackgroundColor)
                .AddLine(_fontBackgroundColor)
                .Add("I hope you enjoyed! Come back and play again soon!", _fontColor, _fontBackgroundColor)
                .Contents.Center(
                    targetWidth: lineWidth + 4, 
                    targetHeight: 6, 
                    _fontBackgroundColor
                );

            var xPosition = (Console.WindowWidth - lineWidth) / 2;
            var yPosition = (Console.WindowHeight - paragraph.Height ) / 2;

            paragraph.UpdatePosition(xPosition, yPosition);

            UserInterface.Display(new DisplayedFill(backgroundColor: _backgroundColor));

            UserInterface.Display(paragraph);

            Console.ReadKey();            
        }
        
        public override GameScreen GetNextScreen()
        {
            return this;
        }
    }
}
