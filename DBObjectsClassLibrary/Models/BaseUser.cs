using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public abstract class BaseUser
    {
        int _userId;
        short _roleId;
        string _userName;
        string _password;
        /// <summary>
        /// Конструктор класса BaseUser
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="roleId">Id роли пользователя</param>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public BaseUser(int userId, short roleId, string userName, string password)
        {
            _userId = userId;
            _roleId = roleId;
            _userName = userName;
            _password = password;
        }
        /// <summary>
        /// Свойство для получения Id пользователя
        /// </summary>
        public int UserId { get =>  _userId; }
        /// <summary>
        /// Свойство для получения Id роли пользователя
        /// </summary>
        public int RoleId { get => _roleId; }
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
