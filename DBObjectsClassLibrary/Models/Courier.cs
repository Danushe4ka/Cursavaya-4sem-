using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс курьера
    /// </summary>
    public class Courier:BaseUser
    {
        public Courier(int userId, string userName, string password) : base(userId, 3, userName, password) { }
        public override string Role { get => "Courier"; }
    }
}
