using FMCore.Interfaces;

namespace FMCore.Models.Pages
{
    public abstract class Page<KParam>
    {
        #region Конструкторы
        public Page(ILogger logger, uint height, uint width)
        {
            this.logger = logger;
            Height = height;
            Width = width;
        }
        #endregion


        #region Поля
        ILogger logger;
        #endregion


        #region Свойства
        protected uint Height { get; set; }
        protected uint Width { get; set; }
        #endregion


        #region Методы
        public abstract void Print(KParam param, int coloredItemIndex);
        #endregion
    }
}
