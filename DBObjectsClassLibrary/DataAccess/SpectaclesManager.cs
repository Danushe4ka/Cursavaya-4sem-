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
            List<Spectacle> spectacles = new List<Spectacle>();
            _command.CommandText = "select * from Spectacles order by SpectacleId;";
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    spectacles.Add(new Spectacle(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4)));
            reader.Close();
            return spectacles;
        }
        public override void Create(Spectacle spectacle)
        {
            string command = $"insert into Spectacles(SpectacleName, SpectacleAuthor, SpectacleGenre, SpectacleDate) values('{spectacle.SpectacleName}','{spectacle.SpectacleAuthor}','{spectacle.SpectacleGenre}','{spectacle.SpectacleDate}');";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
        public override void Update(Spectacle spectacle)
        {
            string command = $"update Spectacles set SpectacleName='{spectacle.SpectacleName}', SpectacleAuthor='{spectacle.SpectacleAuthor}',SpectacleGenre='{spectacle.SpectacleGenre}',SpectacleDate='{spectacle.SpectacleDate}' ";
            command += $"where SpectacleId = {spectacle.SpectacleId}";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
        public override void Delete(Spectacle spectacle)
        {
            string command = $"delete from Spectacles where SpectacleId = {spectacle.SpectacleId};";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
    }
}