
namespace DBObjectsClassLibrary
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public abstract class BaseUser
    {
        string _userName;
        string _password;
        /// <summary>
        /// Конструктор класса BaseUser
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public BaseUser(string userName, string password)
        {
            _userName = userName;
            _password = password;
        }
        /// <summary>
        /// Свойство для получения имени пользователя
        /// </summary>
        public string UserName { get => _userName; }
        /// <summary>
        /// Свойство для получения пароля пользователя
        /// </summary>
        public string Password { get => _password; }
        /// <summary>
        /// Свойство для получения роли пользователя
        /// </summary>
        public abstract string Role { get; }

    }
}
