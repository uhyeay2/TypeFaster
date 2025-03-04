using TypeFaster.ConsoleApp.ExtensionMethods;

namespace TypeFaster.ConsoleApp.Display.Components
{
    /// <summary>
    /// This DisplayComponent can be used to create a DisplayedBlock of text that fits within a set width and enforces Text-Wrapping.
    /// </summary>
    public class DisplayedParagraph : IDisplayComponent
    {
        #region Private Fields

        private readonly int _width;

        private readonly DisplayedBlock _contents;

        private DisplayedLine? _currentLine = null;

        #endregion

        #region Constructor

        public DisplayedParagraph(int width, int positionX = 0, int positionY = 0)
        {
            _contents = new DisplayedBlock(positionX, positionY);
            _width = width;
        }

        #endregion

        #region Public Properties

        public int Width => _width;

        public int PositionX => _contents.PositionX;

        public int PositionY => _contents.PositionY;

        public DisplayedBlock Contents => _contents;

        #endregion

        #region Public Methods

        /// <summary>
        /// Add a string to the contents of the Paragraph with the colors provided. 
        /// The string has TextWrapping applied to ensure it stays within the width of the Paragraph.
        /// If a space cannot be found when applying the TextWrap, then you can optionally add a hyphen.
        /// </summary>
        /// <param name="text">The text to add to the Paragraph.</param>
        /// <param name="fontColor">The color to use for the font when the text is written to the console.</param>
        /// <param name="backgroundColor">The color to use for the background when the text is written to the console.</param>
        /// <param name="hyphenate">If true, a hyphen will be added when a space cannot be found while applying TextWrap.</param>
        /// <returns>The DisplayedParagraph that the text was added to.</returns>
        public DisplayedParagraph Add(string text, ConsoleColor fontColor, ConsoleColor backgroundColor, bool hyphenate = false)
        {
            if (string.IsNullOrEmpty(text))
            {
                return this;
            }

            if (_currentLine?.Width >= Width)
            {
                _currentLine = null;
            }

            // If text fits into CurrentLine without exceeding the max width then add the text to currentLine.
            if (text.Length + _currentLine?.Width <= Width)
            {
                if (_currentLine == null)
                {
                    _currentLine = new DisplayedLine(text, fontColor, backgroundColor);
                    _contents.Add(_currentLine);
                }
                else
                {
                    _currentLine.AddText(text, fontColor, backgroundColor);
                }

                return this;
            }

            // If currentLine is null then TextWrap the string and add each line
            if (_currentLine == null)
            {
                var lines = text.TextWrap(Width, hyphenate);

                for (int i = 0; i < lines.Count(); i++)
                {
                    var line = lines.ElementAt(i);

                    if (i != lines.Count() - 1)
                    {
                        _contents.Add(new DisplayedLine(line.PadRight(Width), fontColor, backgroundColor));
                    }
                    else
                    {
                        // Add the last line without any padding
                        _currentLine = new DisplayedLine(line, fontColor, backgroundColor);
                        _contents.Add(_currentLine);
                    }
                }

                return this;
            }

            // If currentLine was not null, need to finish the currentLine then we can TextWrap the rest.
            var widthRemainingForCurrentLine = Width - _currentLine.Width;

            // If the remaining width is less than or equal to three characters, just add whitespace
            if (widthRemainingForCurrentLine <= 3)
            {
                _currentLine.AddText(new string(' ', widthRemainingForCurrentLine), fontColor, backgroundColor);

                // Use recursion to re-call this method and add the text.
                return Add(text, fontColor, backgroundColor, hyphenate);
            }

            var indexOfLastSpace = text.LastIndexOf(' ', 0, widthRemainingForCurrentLine);

            string textToFinishRemainingLine;

            if (indexOfLastSpace > 0)
            {
                textToFinishRemainingLine = text.Substring(0, indexOfLastSpace);
                text = text.Remove(0, indexOfLastSpace);
            }
            else
            {
                if (hyphenate)
                {
                    textToFinishRemainingLine = text.Substring(0, widthRemainingForCurrentLine - 1) + "-";
                    text = text.Remove(0, widthRemainingForCurrentLine - 1);
                }
                else
                {
                    textToFinishRemainingLine = text.Substring(0, widthRemainingForCurrentLine);
                    text = text.Remove(0, widthRemainingForCurrentLine);
                }
            }

            _currentLine.AddText(textToFinishRemainingLine, fontColor, backgroundColor);

            // Use recursion to re-call this method and add the remaining text.
            return Add(text, fontColor, backgroundColor, hyphenate);
        }

        /// <summary>
        /// Ensures that the Paragraph's currentLine is ended (adds whitespace to reach full width) and resets the Paragraph's currentLine to NULL.
        /// </summary>
        /// <param name="backgroundColor">The background color to use for any whitespace needed to make the Paragraphs currentLine full width.</param>
        /// <returns>The DisplayedParagraph that the line was ended for.</returns>
        public DisplayedParagraph EndLine(ConsoleColor backgroundColor)
        {
            if (_currentLine != null && _currentLine.Width < Width)
            {
                _currentLine.AddText(new string(' ', Width - _currentLine.Width), backgroundColor, backgroundColor);
            }

            _currentLine = null;

            return this;
        }

        /// <summary>
        /// Ends the current line if needed and adds a line of whitespace.
        /// </summary>
        /// <param name="backgroundColor">The background color to use for the whitespace.</param>
        /// <returns>The DisplayedParagraph that the line was added to.</returns>
        public DisplayedParagraph AddLine(ConsoleColor backgroundColor) => 
            EndLine(backgroundColor).Add(new string(' ', Width), backgroundColor, backgroundColor);

        #endregion

        #region IDisplayComponent Implementation

        public IEnumerable<DisplayedLine> GetDisplayedLines() => _contents.GetDisplayedLines();

        #endregion
    }
}
