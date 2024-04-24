
namespace DBObjectsClassLibrary.Models
{
    /// <summary>
    /// Класс зарегистрированного пользователя
    /// </summary>
    public class RegUser:BaseUser
    {
        public RegUser(string userName, string password) : base(userName, password) { }
        public override string Role { get => "RegUser"; }
    }
}
