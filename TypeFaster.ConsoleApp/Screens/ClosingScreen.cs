using TypeFaster.ConsoleApp.Abstraction;

namespace TypeFaster.ConsoleApp.Screens
{
    internal class ClosingScreen : GameScreen
    {
        public override void PresentScreen()
        {
            Console.WriteLine("See you next time!");
            Console.WriteLine("Press any key to exit the game.");

            Console.ReadKey();
            Console.Clear();
        }
        
        public override GameScreen GetNextScreen()
        {
            return this;
        }
    }
}
