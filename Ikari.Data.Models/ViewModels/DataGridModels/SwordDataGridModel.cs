using Ikari.Data.Models.Interfaces;

namespace Ikari.Data.Models.ViewModels.DataGridModels {
    /// <summary>
    /// Модель меча для отображения в datagrid
    /// </summary>
    public class SwordDataGridModel : IKey {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Damage { get; set; }
    }
}
