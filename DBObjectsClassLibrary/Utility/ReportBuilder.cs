using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace DBObjectsClassLibrary.Utility
{
    /// <summary>
    /// Класс вывода отчётов о спектакле
    /// </summary>
    public class ReportBuilder
    {
        SpectaclesManager _sm = new SpectaclesManager();
        TicketsManager _tm = new TicketsManager();  
        /// <summary>
        /// Метод создания полного отчёта о продажам
        /// </summary>
        /// <returns>Полный отчёт по продажам в виде строки</returns>
        public string GetTotalSalesReport()
        {
            List<Spectacle> spectacles = _sm.Read();
            List<Ticket> tickets = _tm.Read();
            double totalSales = 0;
            string report = $"Общая продажа билетов составляет за {spectacles.Count} спектаклей составляет: ";
            foreach (Ticket ticket in tickets)
            {
                totalSales += ticket.Cost;
            }
            report += totalSales + $" д.е. Билетов продано: {tickets.Count}";
            return report;
        }
        /// <summary>
        /// Метод создания отчёта по продажам за каждый тип билета
        /// </summary>
        /// <returns>Отчёт по продажам за каждый тип билета в виде строки</returns>
        public string GetSalesReportByTicketType()
        {
            List<Ticket> tickets = _tm.Read();
            List<string> ticketTypes = new List<string>() { "Партер", "Амфитеатр", "Бельэтаж"};
            string report = "";
            foreach(string ticketType in ticketTypes)
            {
                double price = 0;
                double amount = 0;
                foreach (Ticket ticket in tickets.Where(t => t.Type == ticketType))
                {
                    price += ticket.Cost;
                    amount++;
                }
                report += $"Продано {amount} билетов типа '{ticketType}' на сумму {price} д.е.\n";
            }
            return report;
        }
        /// <summary>
        /// Метод создания отчёта по продажам за каждый спектакль
        /// </summary>
        /// <returns>Отчёт по продажам за каждый спектакль в виде строки</returns>
        public string GetSalesReportBySpectacle()
        {
            List<Ticket> tickets = _tm.Read();
            List<Spectacle> spectacles = _sm.Read();
            string report = "";
            foreach(Spectacle spectacle in spectacles)
            {
                double price = 0;
                double amount = 0;
                foreach (Ticket ticket in tickets.Where(t => t.Spectacle.SpectacleDate == spectacle.SpectacleDate))
                {
                    price += ticket.Cost;
                    amount++;
                }
                if(amount > 0)
                    report += $"Продано {amount} билета(ов) на спектакль '{spectacle.SpectacleName}' на сумму {price} д.е.\n";
            }
            return report;
        }
        
    }   
}
