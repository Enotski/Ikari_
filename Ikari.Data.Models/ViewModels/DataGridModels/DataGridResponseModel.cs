using Ikari.Data.Models.Interfaces;

namespace Ikari.Data.Models.ViewModels.DataGridModels {
    /// <summary>
    /// Модель, котоую принимает datagrid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataGridResponseModel <T> where T : IKey  {
        public IEnumerable<T> data { get; set; }
        public int totalCount { get; set; }
    }
}
