namespace TypeFaster.ConsoleApp.Display.Components
{
    /// <summary>
    /// This DisplayComponent can be used to write a box of repeated characters to a screen.
    /// The PositionX and PositionY defines the top-left position of the box.
    /// The Width defines how far right the character is repeated from the PositionX.
    /// The Height defines how far down the line of characters is repeated.
    /// The FontColor and BackgroundColor determine what colors to use.
    /// </summary>
    public class DisplayedFill : IDisplayComponent
    {
        #region Constructors

        /// <summary>
        /// This constructor is used to define all the properties for the DisplayedFill to create.
        /// </summary>
        /// <param name="character">The character to repeat inside the box.</param>
        /// <param name="positionX">The position of the left side of the box.</param>
        /// <param name="positionY">The position of the top side of the box.</param>
        /// <param name="width">How many characters wide the box should be.</param>
        /// <param name="height">How far down the line of characters should be repeated.</param>
        /// <param name="fontColor">The font color that should be used for the characters.</param>
        /// <param name="backgroundColor">The background color that should be used.</param>
        public DisplayedFill(char character, int positionX, int positionY, int width, int height, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            Character = character;
            PositionX = positionX;
            PositionY = positionY;
            Width = width;
            Height = height;
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// This constructor is used to define a fill that covers the whole Console. The position will default to (0, 0). 
        /// The Width will default to the Console.WindowWidth and the Height will default to Console.WindowHeight.
        /// </summary>
        /// <param name="character">The character to repeat inside the box.</param>
        /// <param name="fontColor">The font color that should be used for the characters.</param>
        /// <param name="backgroundColor">The background color that should be used.</param>
        public DisplayedFill(char character = ' ', ConsoleColor fontColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black)
            : this(character, 0, 0, Console.WindowWidth, Console.WindowHeight, fontColor, backgroundColor)
        {

        }

        #endregion

        #region Public Properties

        public char Character { get; set; }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ConsoleColor FontColor { get; set; }

        public ConsoleColor BackgroundColor { get; set; }

        #endregion

        #region Public Static Methods

        public static List<DisplayedLine> Lines(ConsoleColor backgroundColor, int width, int height, int positionX = 0, int positionY = 0, char character = ' ', ConsoleColor fontColor = ConsoleColor.White) =>
            Lines(character, positionX, positionY, width, height, fontColor, backgroundColor);

        public static List<DisplayedLine> Lines(char character, int positionX, int positionY, int width, int height, ConsoleColor fontColor, ConsoleColor backgroundColor)
        {
            var lines = new List<DisplayedLine>();

            var text = new string(character, width);

            for (int i = 0; i < height; i++)
            {
                var displayedText = new DisplayedText(text, positionX, positionY + i, fontColor, backgroundColor);

                lines.Add(new DisplayedLine(displayedText));
            }

            return lines;
        }

        #endregion

        #region IDisplayComponent Implementation

        public DisplayedContent GetDisplayedContent() =>
            new(Lines(Character, PositionX, PositionY, Width, Height, FontColor, BackgroundColor));

        #endregion
    }
}
