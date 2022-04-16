using FMCore.Interfaces;

namespace FMCore.Models.Borders
{
    public abstract class Border
    {
        #region Конструкторы
        public Border(ILogger logger, uint height, uint width)
        {
            this.logger = logger;
            Height = height;
            Width = width;
        }
        #endregion

        #region Поля
        ILogger logger;
        protected enum Symbols
        {
            TopLeftCorner = '\u2554',
            TopRightCorner = '\u2557',
            Vertical = '\u2551',
            Horizontal = '\u2550',
            CenterLeftCorner = '\u2560',
            CenterRightCorner = '\u2563',
            BottomLeftCorner = '\u255a',
            BottomRightCorner = '\u255d',
        }
        #endregion

        #region Свойства
        protected uint Height { get; set; }
        protected uint Width { get; set; }
        #endregion


        #region Методы
        public abstract void Draw();
        #endregion
    }
}
