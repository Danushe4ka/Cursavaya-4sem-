using System;

namespace DBObjectsClassLibrary.Models.Spectacles
{
    /// <summary>
    /// Класс спектакля жанра трагедия
    /// </summary>
    public class TragedySpectacle:Spectacle
    {
        public TragedySpectacle(string spectacleName, string spectacleAuthor, DateTime spectacleDate) : base(spectacleName, spectacleAuthor, spectacleDate) { }
        public override string Genre { get => "Трагедия"; }
    }
}
