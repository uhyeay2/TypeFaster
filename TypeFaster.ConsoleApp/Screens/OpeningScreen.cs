using TypeFaster.ConsoleApp.Abstraction;

namespace TypeFaster.ConsoleApp.Screens
{
    public class OpeningScreen : GameScreen
    {
        public override void PresentScreen()
        {
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to go to the next screen!");

            Console.ReadKey();
            Console.Clear();
        }

        public override GameScreen GetNextScreen()
        {
            return new ClosingScreen();
        }
    }
}
