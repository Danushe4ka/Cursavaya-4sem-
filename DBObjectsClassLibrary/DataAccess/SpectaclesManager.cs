using DBObjectsClassLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

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
            _command.CommandText = "select * from Spectacles order by @spectacleId;";
            NpgsqlParameter spectacleParam = new NpgsqlParameter("@spectacleId", "SpectacleId");
            _command.Parameters.Add(spectacleParam);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    spectacles.Add(new Spectacle(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4)));
            reader.Close();
            return spectacles;
        }
        public override void Create(Spectacle spectacle)
        {
            _command.Parameters.Clear();
            string command = $"insert into Spectacles(SpectacleName, SpectacleAuthor, SpectacleGenre, SpectacleDate) values(@nameParam, @authorParam, @genreParam, @dateParam);";
            _command.CommandText = command;
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam",spectacle.SpectacleName);
            NpgsqlParameter authorParam = new NpgsqlParameter("@authorParam", spectacle.SpectacleAuthor);
            NpgsqlParameter genreParam = new NpgsqlParameter("@genreParam", spectacle.SpectacleGenre);
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
            command += $"where SpectacleId = @idParam";
            _command.CommandText = command;
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam", spectacle.SpectacleName);
            NpgsqlParameter authorParam = new NpgsqlParameter("@authorParam", spectacle.SpectacleAuthor);
            NpgsqlParameter genreParam = new NpgsqlParameter("@genreParam", spectacle.SpectacleGenre);
            NpgsqlParameter dateParam = new NpgsqlParameter("@dateParam", spectacle.SpectacleDate);
            NpgsqlParameter idParam = new NpgsqlParameter("@idParam", spectacle.SpectacleId);
            _command.Parameters.Add(nameParam);
            _command.Parameters.Add(authorParam);
            _command.Parameters.Add(genreParam);
            _command.Parameters.Add(dateParam);
            _command.Parameters.Add(idParam);
            _command.ExecuteNonQuery();
        }
        public override void Delete(Spectacle spectacle)
        {
            _command.Parameters.Clear();
            string command = $"delete from Spectacles where SpectacleId = @idParam;";
            _command.CommandText = command;
            NpgsqlParameter idParam = new NpgsqlParameter("@idParam", spectacle.SpectacleId);
            _command.Parameters.Add(idParam);
            _command.ExecuteNonQuery();
        }
    }
}