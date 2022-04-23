using FMCore.Engine.ConsoleDrawing;
using FMCore.Interfaces;

namespace FMCore.Models.Borders
{
    public abstract class Border
    {
        #region Конструкторы
        public Border(ILogger logger, ConsoleDrawer drawer, uint height, uint width)
        {
            this.logger = logger;
            Height = height;
            Width = width;
            this.drawer = drawer;
        }
        #endregion

        #region Поля
        protected ILogger logger;
        protected ConsoleDrawer drawer;
        protected enum Symbols
        {
            TopLeftCorner = '\u2554',
            TopRightCorner = '\u2557',
            TopCenter = '\u2566',
            Vertical = '\u2551',
            Horizontal = '\u2550',
            CenterLeftCorner = '\u2560',
            CenterRightCorner = '\u2563',
            BottomLeftCorner = '\u255a',
            BottomRightCorner = '\u255d',
            BottomCenter =  '\u2569'
        }
        #endregion

        #region Свойства
        protected uint Height { get; set; }
        protected uint Width { get; set; }
        protected (uint x, uint y) StartCoordinates { get; set; }
        #endregion


        #region Методы
        public abstract void Draw();
        #endregion
    }
}
