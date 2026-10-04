namespace EntityNexus.DomainModel.AbstractClasses.Core
{
    /// <summary>
    /// Абстрактный базовый класс для именованных сущностей.
    /// Обеспечивает обязательное наличие непустого имени при создании и возможность его изменения.
    /// </summary>
    public abstract class AEntityNamed(string name) : AEntity
    {
        /// <summary>
        /// Название сущности.
        /// Не может быть null, пустой строкой или состоять только из пробелов.
        /// </summary>
        /// <exception cref="ArgumentException">Если переданное имя пустое или состоит только из пробелов.</exception>
        public string Name { get; private set; }
            = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Name cannot be empty");

        /// <summary>
        /// Изменяет имя сущности.
        /// </summary>
        /// <param name="name">Новое имя сущности.</param>
        /// <exception cref="ArgumentException">Если новое имя пустое или состоит только из пробелов.</exception>
        public void Rename(string name)
            => Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Name cannot be empty");
    }
}