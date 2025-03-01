namespace TypeFaster.ConsoleApp.Abstraction
{
    /// <summary>
    /// The GameScreen represents a 'Screen' that is presented to the user via the Console
    /// </summary>
    public abstract class GameScreen
    {
        /// <summary>
        /// This method defines what happens when the screen is presented. 
        /// When this method ends the GetNextScreen() will be called to determine what screen is presented next.
        /// </summary>
        public abstract void PresentScreen();

        /// <summary>
        /// This method defines what GameScreen the user should be presented next.
        /// This method is called after PresentScreen() ends.
        /// </summary>
        /// <returns>GameScreen to be presented after the current GameScreen.</returns>
        public abstract GameScreen GetNextScreen();
    }
}
