
namespace TypeFaster.ConsoleApp.Display.Components
{
    /// <summary>
    /// This class represents a block (list) of DisplayedLines with each line being one row lower on the console.
    /// </summary>
    public class DisplayedBlock : IDisplayComponent
    {
        #region Private Fields

        private readonly List<DisplayedLine> _displayedLines = [];

        private int _positionX;

        private int _positionY;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of DisplayedBlock that starts the the X and Y Position provided.
        /// </summary>
        /// <param name="positionX">The starting (left) X position for the DisplayBlock.</param>
        /// <param name="positionY">The starting (top) Y position for the DisplayBlock.</param>
        public DisplayedBlock(int positionX = 0, int positionY = 0)
        {
            _positionX = positionX;
            _positionY = positionY;
        }

        /// <summary>
        /// Create a new instance of DisplayedBlock that uses the DisplayedLine's X and Y Position as its starting point.
        /// Adds the DisplayedLine as the top line of the DisplayedBlock.
        /// </summary>
        /// <param name="displayedLine">The DisplayedLine put at the top of the DisplayedBlock and use as the starting position.</param>
        public DisplayedBlock(DisplayedLine displayedLine)
            :this(displayedLine.PositionX, displayedLine.PositionY) =>
                _displayedLines.Add(displayedLine);

        #endregion

        #region Public Accessors

        public int Height => _displayedLines.Count;

        public int MaxWidth => _displayedLines.Max(_ => _.Width);

        public int PositionX => _positionX;

        public int PositionY => _positionY;

        #endregion

        #region Public Methods - Add DisplayedContent

        /// <summary>
        /// Add DisplayedLines at the top of the DisplayedBlock. Resets Y Position of all displayed lines after inserting lines.
        /// </summary>
        /// <param name="displayedLines">The DisplayedLines to insert at the top of the DisplayedBlock</param>
        /// <returns>The DisplayBlock that the DisplayedLines were added to.</returns>
        public DisplayedBlock Prepend(IEnumerable<DisplayedLine> displayedLines)
        {
            _displayedLines.InsertRange(0, displayedLines);

            UpdatePositionY(_positionY);

            return this;
        }

        /// <summary>
        /// Add DisplayedLines to the bottom of the DisplayedBlock. 
        /// Resets X Position of each Line to match the X Position of the DisplayedBlock
        /// Resets Y Position of each line to be the next row below the previous DisplayedLine.
        /// </summary>
        /// <param name="displayedLines">The DisplayedLines to add to the bottom of the DisplayBlock</param>
        /// <returns>The DisplayBlock that the DisplayedLines were added to.</returns>
        public DisplayedBlock Add(IEnumerable<DisplayedLine> displayedLines)
        {
            foreach (var line in displayedLines)
            {
                line.UpdatePosition(_positionX, _positionY + Height);

                _displayedLines.Add(line);
            }

            return this;
        }

        /// <summary>
        /// Adds DisplayedLine(s) to the bottom of the DisplayedBlock.
        /// Resets X Position of each Line to match the X Position of the DisplayedBlock
        /// Resets Y Position of each line to be the next row below the previous DisplayedLine.
        /// </summary>
        /// <param name="line">The DisplayedLine to add to the bottom of the DisplayedBlock.</param>
        /// <returns>The DisplayBlock that the DisplayedLines were added to.</returns>
        public DisplayedBlock Add(params DisplayedLine[] displayedLines)
        {
            foreach (var line in displayedLines)
            {
                line.UpdatePosition(_positionX, _positionY + Height);

                _displayedLines.Add(line);
            }

            return this;
        }

        /// <summary>
        /// Centers the DisplayedBlock vertically to the targetHeight provided by adding DisplayedLine(s) of whitespace to
        /// the top and bottom of the DisplayedBlock. The lines will be the same width as the MaxWidth of the DisplayBlock.
        /// </summary>
        /// <param name="targetHeight">The height that the DisplayedBlock should be after centering is completed.</param>
        /// <param name="backgroundColor">The background color for the DisplayedBlock</param>
        /// <returns>The DisplayBlock that the DisplayedLines were added to.</returns>
        public DisplayedBlock CenterVertical(int targetHeight, ConsoleColor backgroundColor)
        {
            if (targetHeight <= Height)
            {
                return this;
            }

            var topPadding = (targetHeight - Height) / 2;

            Prepend(DisplayedFill.Lines(backgroundColor, MaxWidth, topPadding));

            var bottomPadding = targetHeight - Height;

            if (bottomPadding > 0)
            {
                Add(DisplayedFill.Lines(backgroundColor, MaxWidth, bottomPadding));
            }

            return this;
        }

        /// <summary>
        /// Centers the DisplayedBlock vertically and horizontally by adding DisplayedLine(s) of whitespace to
        /// the top and bottom of the DisplayedBlock to reach the targetHeight, and Centers the text of each line
        /// to the targetWidth.
        /// </summary>
        /// <param name="targetWidth">The width that the DisplayedLines inside the DisplayedBlock should be centered to.</param>
        /// <param name="targetHeight">The height that the DisplayedBlock should be after centering is completed.</param>
        /// <param name="backgroundColor">The background color for whitespace that is added around the DisplayedBlock.</param>
        /// <returns></returns>
        public DisplayedBlock Center(int targetWidth, int targetHeight, ConsoleColor backgroundColor)
        {
            CenterVertical(targetHeight, backgroundColor);

            foreach (var line in _displayedLines)
            {
                line.CenterText(targetWidth, backgroundColor);
            }
            
            return this;
        }

        #endregion

        #region Public Methods - Update Position

        /// <summary>
        /// Update the top left X Position and Y Position for the DisplayedBlock.
        /// </summary>
        /// <param name="positionX">The starting X Position for each line in the DisplayedBlock.</param>
        /// <param name="positionY">The top Y Position for the Displayed Block</param>
        public void UpdatePosition(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;

            for (int i = 0; i < _displayedLines.Count; i++)
            {
                var line = _displayedLines[i];

                line.UpdatePositionY(_positionY + i);
                line.UpdatePositionX(positionX);

            }
            foreach (var line in _displayedLines)
            {
                line.UpdatePositionX(positionX);
            }

        }

        /// <summary>
        /// Updates the starting X Positon for each DisplayedLine in the DisplayedBlock.
        /// </summary>
        /// <param name="positionX"></param>
        public void UpdatePositionX(int positionX)
        {
            _positionX = positionX;

            foreach (var line in _displayedLines)
            {
                line.UpdatePositionX(positionX);
            }
        }

        /// <summary>
        /// Updates the starting Y Position for each DisplayedLine in the DisplayedBlock.
        /// </summary>
        /// <param name="positionY"></param>
        public void UpdatePositionY(int positionY)
        {
            _positionY = positionY;

            for (int i = 0; i < _displayedLines.Count; i++)
            {
                _displayedLines[i].UpdatePositionY(_positionY + i);
            }
        }

        #endregion

        #region IDisplayComponent Implementation

        public IEnumerable<DisplayedLine> GetDisplayedLines() => _displayedLines;
        
        #endregion
    }
}
