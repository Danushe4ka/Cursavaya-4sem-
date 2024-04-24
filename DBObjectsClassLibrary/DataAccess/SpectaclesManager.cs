using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Spectacles;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Класс-менеджер спектаклей
    /// </summary>
    public class SpectaclesManager : CommonManager<Spectacle>
    {
        public SpectaclesManager():base() { }
        public override List<Spectacle> Read()
        {
            _command.Parameters.Clear();
            List<Spectacle> spectacles = new List<Spectacle>();
            _command.CommandText = "select * from SpectaclesView order by @spectacleName;";
            NpgsqlParameter spectacleParam = new NpgsqlParameter("@spectacleName", "SpectacleName");
            _command.Parameters.Add(spectacleParam);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    switch (reader.GetString(3))
                    {
                        case "Трагедия":
                            spectacles.Add(new TragedySpectacle(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2)));
                            break;
                        case "Комедия":
                            spectacles.Add(new ComedySpectacle(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2)));
                            break;
                        case "Драма":
                            spectacles.Add(new DramaSpectacle(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2)));
                            break;
                        case "Роман":
                            spectacles.Add(new NovelSpectacle(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2)));
                            break;
                        default:
                            throw new Exception("DB reader malfunction!");
                    }
                }
            reader.Close();
            return spectacles;
        }
        public override void Create(Spectacle spectacle)
        {
            _command.Parameters.Clear();
            string command = $"insert into Spectacles(SpectacleName, SpectacleAuthor, GenreId, SpectacleDate) values(@nameParam, @authorParam, @genreParam, @dateParam);";
            _command.CommandText = command;
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam",spectacle.SpectacleName);
            NpgsqlParameter authorParam = new NpgsqlParameter("@authorParam", spectacle.SpectacleAuthor);
            NpgsqlParameter genreParam;
            switch(spectacle.Genre)
            {
                case "Трагедия":
                    genreParam = new NpgsqlParameter("@genreParam", 1);
                    break;
                case "Комедия":
                    genreParam = new NpgsqlParameter("@genreParam", 2);
                    break;
                case "Драма":
                    genreParam = new NpgsqlParameter("@genreParam", 3);
                    break;
                case "Роман":
                    genreParam = new NpgsqlParameter("@genreParam", 4);
                    break;
                default:
                    throw new Exception("DB reader malfunction!");

            }
             NpgsqlParameter dateParam = new NpgsqlParameter("@dateParam", spectacle.SpectacleDate);
            _command.Parameters.Add(nameParam);
            _command.Parameters.Add(authorParam);
            _command.Parameters.Add(genreParam);
            _command.Parameters.Add(dateParam);
            _command.ExecuteNonQuery();
        }
        public override void Update(Spectacle spectacle)
        {
            _command.Parameters.Clear();
            string command = $"update Spectacles set SpectacleName=@nameParam, SpectacleAuthor=@authorParam,SpectacleGenre=@genreParam,SpectacleDate=@dateParam ";
            command += $"where SpectacleDate = @dateParam";
            _command.CommandText = command;
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam", spectacle.SpectacleName);
            NpgsqlParameter authorParam = new NpgsqlParameter("@authorParam", spectacle.SpectacleAuthor);
            NpgsqlParameter genreParam;
            switch (spectacle.Genre)
            {
                case "Трагедия":
                    genreParam = new NpgsqlParameter("@genreParam", 1);
                    break;
                case "Комедия":
                    genreParam = new NpgsqlParameter("@genreParam", 2);
                    break;
                case "Драма":
                    genreParam = new NpgsqlParameter("@genreParam", 3);
                    break;
                case "Роман":
                    genreParam = new NpgsqlParameter("@genreParam", 4);
                    break;
                default:
                    throw new Exception("DB reader malfunction!");

            }
            NpgsqlParameter dateParam = new NpgsqlParameter("@dateParam", spectacle.SpectacleDate);
            _command.Parameters.Add(nameParam);
            _command.Parameters.Add(authorParam);
            _command.Parameters.Add(genreParam);
            _command.Parameters.Add(dateParam);
            _command.ExecuteNonQuery();
        }
        public override void Delete(Spectacle spectacle)
        {
            _command.Parameters.Clear();
            string command = $"delete from Spectacles where SpectacleDate = @dateParam;";
            _command.CommandText = command;
            NpgsqlParameter dateParam = new NpgsqlParameter("@dateParam", spectacle.SpectacleDate);
            _command.Parameters.Add(dateParam);
            _command.ExecuteNonQuery();
        }
    }
}