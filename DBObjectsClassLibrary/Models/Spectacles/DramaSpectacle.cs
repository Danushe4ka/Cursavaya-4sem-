using System;

namespace DBObjectsClassLibrary.Models.Spectacles
{
    /// <summary>
    /// Класс спектакля жанра драма
    /// </summary>
    public class DramaSpectacle:Spectacle
    {
        public DramaSpectacle(string spectacleName, string spectacleAuthor, DateTime spectacleDate) : base(spectacleName, spectacleAuthor, spectacleDate) { }
        public override string Genre { get => "Драма"; }
    }
}
