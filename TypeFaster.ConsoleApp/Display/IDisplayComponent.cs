using TypeFaster.ConsoleApp.Display.Components;

namespace TypeFaster.ConsoleApp.Display
{
    /// <summary>
    /// This interface defines the contract for an object that creates DisplayedLines that will be written to the Console.
    /// </summary>
    public interface IDisplayComponent
    {
        /// <summary>
        /// Returns the DisplayedLines that will be written to the Console.
        /// </summary>
        public IEnumerable<DisplayedLine> GetDisplayedLines();
    }
}
