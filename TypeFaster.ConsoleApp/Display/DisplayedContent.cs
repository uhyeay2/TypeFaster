using TypeFaster.ConsoleApp.Display.Components;

namespace TypeFaster.ConsoleApp.Display
{
    /// <summary>
    /// This class represents a collection of DisplayedText that will be written to the console.
    /// </summary>
    public class DisplayedContent
    {
        #region Private Fields

        private readonly List<DisplayedText> _displayedText = [];

        #endregion

        #region Constructors

        public DisplayedContent(List<DisplayedText> displayedText) => _displayedText = displayedText;

        public DisplayedContent(List<DisplayedLine> displayedLines) => Add(displayedLines);

        #endregion

        #region Public Accessor

        public IReadOnlyList<DisplayedText> DisplayedText => _displayedText;

        #endregion

        #region Public Methods - Adding DisplayedContent

        public DisplayedContent Add(params DisplayedText[] text)
        {
            _displayedText.AddRange(text);

            return this;
        }

        public DisplayedContent Add(List<DisplayedLine> displayedLines)
        {
            foreach (var line in displayedLines)
            {
                _displayedText.AddRange(line.Contents);
            }

            return this;
        }

        public DisplayedContent Add(IDisplayComponent component)
        {
            var content = component.GetDisplayedContent();

            _displayedText.AddRange(content.DisplayedText);

            return this;
        }

        #endregion
    }
}
