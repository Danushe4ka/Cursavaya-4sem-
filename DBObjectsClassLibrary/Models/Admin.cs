using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс админа
    /// </summary>
    public class Admin:BaseUser
    {
        public Admin(int userId, string userName, string password):base(userId, 1, userName, password) { }
        public override string Role { get => "Admin"; }
    }
}
