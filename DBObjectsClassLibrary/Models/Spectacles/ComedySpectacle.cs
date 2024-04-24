using System;

namespace DBObjectsClassLibrary.Models.Spectacles
{
    /// <summary>
    /// Класс спектакля жанра комедия
    /// </summary>
    public class ComedySpectacle:Spectacle
    {
        public ComedySpectacle(string spectacleName, string spectacleAuthor, DateTime spectacleDate) : base(spectacleName, spectacleAuthor, spectacleDate) { }
        public override string Genre { get => "Комедия"; }
    }
}
