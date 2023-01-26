using Ikari.Data.Models.Interfaces;

namespace Ikari.Data.Models.ViewModels.DataGridModels {
    /// <summary>
    /// Модель брони для отображения в datagrid
    /// </summary>
    public class ArmourDataGridModel : IKey {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
