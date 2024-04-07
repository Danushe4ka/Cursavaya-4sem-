using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UsersManager usersManager = new UsersManager();
            Console.WriteLine(usersManager.Read()[0].UserId);
            Console.ReadKey();
        }
    }
}
