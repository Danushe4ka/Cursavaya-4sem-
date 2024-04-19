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
using DBObjectsClassLibrary.Utility;

namespace Tetstststs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Ticket> tickets= new TicketsManager().Read();
            foreach (Ticket ticket in tickets)
            {
                Console.WriteLine($"{ticket.User.UserName} | {ticket.Spectacle.SpectacleName} | {ticket.Type} | {ticket.Cost} | {ticket.PlaceAmount}");  
            }
            Console.WriteLine();
            List<Spectacle> spectacles = new SpectaclesManager().Read();
            foreach (Spectacle spectacle in spectacles)
            {
                Console.WriteLine($"{spectacle.SpectacleName} | {spectacle.Genre} | {spectacle.SpectacleAuthor} | {spectacle.SpectacleDate}");
            }
            Console.WriteLine();
            ReportBuilder reportBuilder = new ReportBuilder();
            Console.WriteLine(reportBuilder.GetSalesReportBySpectacle());
            Console.ReadKey();
        }
    }
}
