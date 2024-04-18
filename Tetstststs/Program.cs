using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBObjectsClassLibrary;
using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using DBObjectsClassLibrary.Models.Spectacles;

namespace Tetstststs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Ticket> tickets= new TicketsManager().Read();

            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine($"{ticket.User.UserName} | {ticket.Spectacle.SpectacleName} | {ticket.Type} | {ticket.Cost}");  
            }
            Console.ReadKey();
        }
    }
}
