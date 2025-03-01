namespace TypeFaster.ConsoleApp.Display
{
    /// <summary>
    /// This class represents a string that will be written to the console.
    /// PositionX and PositionY represent the starting positon that the string will be written at.
    /// </summary>
    public class DisplayedText
    {
        #region Constructor

        /// <summary>
        /// Create a string that will be written to the console.
        /// PositionX and PositionY represent the starting positon that the string will be written at.
        /// </summary>
        /// <param name="value">The string that will be written to the console.</param>
        /// <param name="positionX">The starting X positon to write the string at.</param>
        /// <param name="positionY">The Y position to write the string at.</param>
        /// <param name="fontColor">The font color to use when writing the string.</param>
        /// <param name="backgroundColor">The background color to use when writing the string.</param>
        public DisplayedText(string value, int positionX = 0, int positionY = 0, ConsoleColor fontColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Value = value;
            PositionX = positionX;
            PositionY = positionY;
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        #endregion

        #region Public Properties

        public string Value { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public ConsoleColor FontColor { get; set; }

        public ConsoleColor BackgroundColor { get; set; }
        
        #endregion
    }
}
