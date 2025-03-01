using TypeFaster.ConsoleApp.Abstraction;
using TypeFaster.ConsoleApp.Screens;

Console.CursorVisible = false;

GameScreen currentScreen = new OpeningScreen();

while (currentScreen is not ClosingScreen)
{
    currentScreen.PresentScreen();

    currentScreen = currentScreen.GetNextScreen();
}

currentScreen.PresentScreen();

Console.Clear();
Console.ResetColor();