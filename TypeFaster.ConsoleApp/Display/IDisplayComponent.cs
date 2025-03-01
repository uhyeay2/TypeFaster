namespace TypeFaster.ConsoleApp.Display
{
    /// <summary>
    /// This interface defines the contract for an object that creates DisplayedContent that will be written to the Console.
    /// </summary>
    public interface IDisplayComponent
    {
        /// <summary>
        /// Returns DisplayedContent that will be written to the Console.
        /// </summary>
        public DisplayedContent GetDisplayedContent();
    }
}
