using Ikari.Data.Models.Interfaces;

namespace Ikari.Data.Models.ViewModels.DataGridModels {
    /// <summary>
    /// Модель пользователя для отображения в datagrid
    /// </summary>
    public class UserDataGridModel : IKey {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
