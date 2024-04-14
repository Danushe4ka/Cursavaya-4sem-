﻿using DBObjectsClassLibrary.Models;
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
            _command.Parameters.Clear();
            List<BaseUser> users = new List<BaseUser>();
            _command.CommandText = "select * from Users order by @userId;";
            NpgsqlParameter userParam = new NpgsqlParameter("@userId", "UserId");
            _command.Parameters.Add(userParam);
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
            _command.Parameters.Clear();
            string command = $"insert into Users(RoleId, Username, Password) values (@roleParam, @nameParam, @passParam);";
            _command.CommandText = command;
            NpgsqlParameter roleParam = new NpgsqlParameter("@roleParam", user.RoleId);
            NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam", user.UserName);
            NpgsqlParameter passParam = new NpgsqlParameter("@passParam", user.Password);
            _command.Parameters.Add(roleParam);
            _command.Parameters.Add(nameParam);
            _command.Parameters.Add(passParam);
            _command.ExecuteNonQuery();
        }
        public override void Update(BaseUser user)
        {
            if (user.RoleId != 1)
            {
                _command.Parameters.Clear();
                string command = $"update Users set Username=@nameParam, Password=@passParam ";
                command += $"where UserId = @idParam";
                _command.CommandText = command;
                NpgsqlParameter idParam = new NpgsqlParameter("@idParam", user.UserId);
                NpgsqlParameter nameParam = new NpgsqlParameter("@nameParam", user.UserName);
                NpgsqlParameter passParam = new NpgsqlParameter("@passParam", user.Password);
                _command.Parameters.Add(idParam);
                _command.Parameters.Add(nameParam);
                _command.Parameters.Add(passParam);
                _command.ExecuteNonQuery();
            }
            else
                throw new Exception("Admin role cannot be changed!");
        }
        public override void Delete(BaseUser user)
        {
            if (user.RoleId != 1)
            {
                _command.Parameters.Clear();
                string command = $"delete from Users where UserId = @idParam;";
                _command.CommandText = command;
                NpgsqlParameter idParam = new NpgsqlParameter("@idParam", user.UserId);
                _command.Parameters.Add(idParam);
                _command.ExecuteNonQuery();
            }
            else
                throw new Exception("Admin role cannot be changed!");
        }
    }
}
