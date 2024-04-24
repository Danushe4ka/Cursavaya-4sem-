using System;

namespace DBObjectsClassLibrary.Models.Spectacles
{
    /// <summary>
    /// Класс спектакля жанра роман
    /// </summary>
    public class NovelSpectacle:Spectacle
    {
        public NovelSpectacle(string spectacleName, string spectacleAuthor, DateTime spectacleDate) : base(spectacleName, spectacleAuthor, spectacleDate) { }
        public override string Genre { get => "Роман"; }
    }
}
