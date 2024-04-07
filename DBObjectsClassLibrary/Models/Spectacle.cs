using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс спектакля
    /// </summary>
    public class Spectacle
    {
        int _spectacleId; 
        string _spectacleName;
        string _spectacleAuthor;
        string _spectacleGenre;
        DateTime _spectacleDate;
        /// <summary>
        /// Конструктор класса Spectacle
        /// </summary>
        /// <param name="spectacleId">Id спектакля</param>
        /// <param name="spectacleName">Название спектакля</param>
        /// <param name="spectacleAuthor">Автор спектакля</param>
        /// <param name="spectacleGenre">Жанр спектакля</param>
        /// <param name="spectacleDate">Дата показа спектакля</param>
        public Spectacle(int spectacleId, string spectacleName, string spectacleAuthor, string spectacleGenre, DateTime spectacleDate)
        {
            _spectacleId = spectacleId;
            _spectacleName = spectacleName;
            _spectacleAuthor = spectacleAuthor;
            _spectacleGenre = spectacleGenre;
            _spectacleDate = spectacleDate;
        }
        /// <summary>
        /// Свойство для получения Id спекталя
        /// </summary>
        public int SpectacleId { get => _spectacleId; }
        /// <summary>
        /// Свойство для получения названия спекталя
        /// </summary>
        public string SpectacleName {  get => _spectacleName; }
        /// <summary>
        /// Свойство для получения автора спекталя
        /// </summary>
        public string SpectacleAuthor { get => _spectacleAuthor; }
        /// <summary>
        /// Свойство для получения жанра спекталя
        /// </summary>
        public string SpectacleGenre { get => _spectacleGenre; }
        /// <summary>
        /// Свойство для получения даты показа спекталя
        /// </summary>
        public DateTime SpectacleDate { get => _spectacleDate; }
    }
}
