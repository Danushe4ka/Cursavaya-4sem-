
namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс админа
    /// </summary>
    public class Admin:BaseUser
    {
        public Admin(string userName, string password):base(userName, password) { }
        public override string Role { get => "Admin"; }
    }
}
