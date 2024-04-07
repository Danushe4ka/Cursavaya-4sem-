using DBObjectsClassLibrary.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.DataAccess
{
    /// <summary>
    /// Класс-менеджер пользователей
    /// </summary>
    public class UsersManager:CommonManager<BaseUser>
    {
        public override List<BaseUser> Read()
        {
            List<BaseUser> users = new List<BaseUser>();
            _command.CommandText = "select * from Users order by UserId;";
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    switch(reader.GetInt16(1))
                    {
                        case 1:
                            users.Add(new Admin(reader.GetInt32(0), reader.GetString(2), reader.GetString(3)));
                            break;
                        case 2:
                            users.Add(new RegUser(reader.GetInt32(0), reader.GetString(2), reader.GetString(3)));
                            break;
                        case 3:
                            users.Add(new Courier(reader.GetInt32(0), reader.GetString(2), reader.GetString(3)));
                            break;
                        default:
                            throw new Exception("DB reader malfunction!");
                    }
                }
            reader.Close();
            return users;
        }
        public override void Create(BaseUser user)
        {
            string command = $"insert into Users(RoleId, Username, Password) values ({user.RoleId},'{user.UserName}','{user.Password}');";
            _command.CommandText = command;
            _command.ExecuteNonQuery();
        }
        public override void Update(BaseUser user)
        {
            if (user.RoleId != 1)
            {
                string command = $"update Users set Username='{user.UserName}', Password='{user.Password}' ";
                command += $"where UserId = {user.UserId}";
                _command.CommandText = command;
                _command.ExecuteNonQuery();
            }
            else
                throw new Exception("Admin role cannot be changed!");
        }
        public override void Delete(BaseUser user)
        {
            if (user.RoleId != 1)
            {
                string command = $"delete from Users where UserId = {user.UserId};";
                _command.CommandText = command;
                _command.ExecuteNonQuery();
            }
            else
                throw new Exception("Admin role cannot be changed!");
        }
    }
}
