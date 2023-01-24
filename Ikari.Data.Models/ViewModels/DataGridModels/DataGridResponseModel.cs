using Ikari.Data.Models.Interfaces;

namespace Ikari.Data.Models.ViewModels.DataGridModels {
    public class DataGridResponseModel <T> where T : IKey  {
        public IEnumerable<T> data { get; set; }
        public int totalCount { get; set; }
    }
}
