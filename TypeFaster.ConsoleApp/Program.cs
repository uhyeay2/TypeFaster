
using TypeFaster.ConsoleApp.Abstraction;
using TypeFaster.ConsoleApp.Screens;

GameScreen currentScreen = new OpeningScreen();

while (currentScreen is not ClosingScreen)
{
    currentScreen.PresentScreen();

    currentScreen = currentScreen.GetNextScreen();
}

currentScreen.PresentScreen();