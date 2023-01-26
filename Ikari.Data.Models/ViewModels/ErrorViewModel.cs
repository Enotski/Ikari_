namespace Ikari.Data.Models.ViewModels {
	/// <summary>
	/// Вью-модель исключения
	/// </summary>
	public class ErrorViewModel {
		public string? RequestId { get; set; }
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
