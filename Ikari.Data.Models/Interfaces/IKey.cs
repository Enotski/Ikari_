namespace Ikari.Data.Models.Interfaces {
    /// <summary>
    /// Интерфейс сущmности с ключем в качестве Guid
    /// </summary>
    public interface IKey {
        Guid Id { get; set; }
    }
}
