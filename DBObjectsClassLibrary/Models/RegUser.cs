﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс зарегистрированного пользователя
    /// </summary>
    public class RegUser:BaseUser
    {
        public RegUser(int userId, string userName, string password) : base(userId, 2, userName, password) { }
        public override string Role { get => "RegUser"; }
    }
}
