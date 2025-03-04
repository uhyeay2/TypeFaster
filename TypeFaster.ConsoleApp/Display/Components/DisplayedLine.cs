namespace TypeFaster.ConsoleApp.Display.Components
{
    /// <summary>
    /// This class represents a Line of DisplayedText that shares the same Y axis and has no spacing between each text.
    /// </summary>
    public class DisplayedLine : IDisplayComponent
    {
        #region Private Fields

        private readonly List<DisplayedText> _contents = [];

        private int _positionX;
        
        private int _positionY;

        private int _width;

        #endregion

        #region Constructors

        public DisplayedLine(string text, ConsoleColor fontColor, ConsoleColor backgroundColor, int positionX = 0, int positionY = 0)
            : this(new DisplayedText(text, positionX, positionY, fontColor, backgroundColor))
        {
            
        }

        public DisplayedLine(DisplayedText displayedText)
        {
            _positionX = displayedText.PositionX;
            _positionY = displayedText.PositionY;
            _width = displayedText.Value.Length;
            _contents.Add(displayedText);
        }

        #endregion

        #region Public Accessors

        public int PositionX => _positionX;

        public int PositionY => _positionY;

        public int Width => _width;

        public IReadOnlyList<DisplayedText> Contents => _contents;

        #endregion

        #region Public Methods - Update Position

        /// <summary>
        /// Update both the starting position of the DisplayedLine.
        /// </summary>
        /// <param name="positionX">The starting X position for the DisplayedLine.</param>
        /// <param name="positionY">The starting Y position for the DisplayedLine.</param>
        public void UpdatePosition(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
            _width = 0;

            foreach (var displayedText in _contents)
            {
                displayedText.PositionY = _positionY;
                displayedText.PositionX = _positionX + Width;

                _width += displayedText.Value.Length;
            }
        }

        /// <summary>
        /// Update the starting X position for the DisplayedLine.
        /// </summary>
        /// <param name="positionX">The starting X position for the DisplayedLine.</param>
        public void UpdatePositionX(int positionX)
        {
            _positionX = positionX;
            _width = 0;

            foreach (var displayedText in _contents)
            {
                displayedText.PositionX = _positionX + Width;
                
                _width += displayedText.Value.Length;
            }
        }

        /// <summary>
        /// Update the starting Y position for the DisplayedLine.
        /// </summary>
        /// <param name="positionY">The starting Y position for the DisplayedLine.</param>
        public void UpdatePositionY(int positionY)
        {
            _positionY = positionY;

            foreach (var displayedText in _contents)
            {
                displayedText.PositionY = positionY;
            }
        }

        #endregion

        #region Public Methods - Adding Text

        /// <summary>
        /// Add text to the end of the line.
        /// </summary>
        /// <param name="text">The string of text to add to the DisplayedLine.</param>
        /// <param name="fontColor">The font color to use when writing the string of text.</param>
        /// <param name="backgroundColor">The backgroundColor to use when writing the string of text.</param>
        /// <returns>The DisplayedLine that the text is being added to.</returns>
        public DisplayedLine AddText(string text, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            if (string.IsNullOrEmpty(text))
            {
                return this;
            }

            _contents.Add(new DisplayedText(text, PositionX + Width, PositionY, fontColor, backgroundColor));
            _width += text.Length;

            return this;
        }

        public DisplayedLine PrependText(string text, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            if (string.IsNullOrEmpty(text))
            {
                return this;
            }

            _contents.Insert(0, new DisplayedText(text, _positionX, _positionY, fontColor, backgroundColor));
            _width += text.Length;

            UpdatePositionX(_positionX);

            return this;
        }

        public DisplayedLine CenterText(int targetWidth, ConsoleColor backgroundColor)
        {
            if (targetWidth <= Width)
            {
                return this;
            }

            var leftPadding = (targetWidth - Width) / 2;

            PrependText(new string(' ', leftPadding), backgroundColor, backgroundColor);

            var rightPadding = targetWidth - Width;

            if (rightPadding > 0)
            {
                AddText(new string(' ', rightPadding), backgroundColor, backgroundColor);
            }

            return this;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Return a new DisplayedLine of whitespace.
        /// </summary>
        /// <param name="color">The background color for the spacing.</param>
        /// <param name="width">The width of the spacing.</param>
        /// <param name="xPosition">The starting X position for the DisplayedLine.</param>
        /// <param name="yPosition">The Y position for the DisplayedLine.</param>
        /// <returns></returns>
        public static DisplayedLine Spacing(ConsoleColor color, int width = 1, int xPosition = 0, int yPosition = 0) => 
            new( new DisplayedText(new string(' ', width), xPosition, yPosition, color, color));

        #endregion

        #region IDisplayComponent Implementation

        public IEnumerable<DisplayedLine> GetDisplayedLines() => [this];
        
        #endregion
    }
}
