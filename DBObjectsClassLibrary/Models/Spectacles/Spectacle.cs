using System;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс спектакля
    /// </summary>
    public abstract class Spectacle
    {
        string _spectacleName;
        string _spectacleAuthor;
        DateTime _spectacleDate;
        /// <summary>
        /// Конструктор класса Spectacle
        /// </summary>
        /// <param name="spectacleName">Название спектакля</param>
        /// <param name="spectacleAuthor">Автор спектакля</param>
        /// <param name="spectacleDate">Дата показа спектакля</param>
        public Spectacle(string spectacleName, string spectacleAuthor, DateTime spectacleDate)
        {
            _spectacleName = spectacleName;
            _spectacleAuthor = spectacleAuthor;
            _spectacleDate = spectacleDate;
        }
        /// <summary>
        /// Свойство для получения названия спекталя
        /// </summary>
        public string SpectacleName {  get => _spectacleName; }
        /// <summary>
        /// Свойство для получения автора спекталя
        /// </summary>
        public string SpectacleAuthor { get => _spectacleAuthor; }
        /// <summary>
        /// Свойство для получения даты показа спекталя
        /// </summary>
        public DateTime SpectacleDate { get => _spectacleDate; }
        /// <summary>
        /// Свойство для получения наименования жанра спектакля
        /// </summary>
        public abstract string Genre { get; }
    }
}
