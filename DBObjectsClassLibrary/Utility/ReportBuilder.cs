using DBObjectsClassLibrary.DataAccess;
using DBObjectsClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Utility
{
    /// <summary>
    /// Класс вывода отчётов о спектакле
    /// </summary>
    public class ReportBuilder
    {
        /*
        /// <summary>
        /// Метод создания полного отчёта о продажам
        /// </summary>
        /// <returns>Полный отчёт по продажам в виде строки</returns>
        public string GetTotalSalesReport()
        {
            TicketsManager tm = new TicketsManager();
            TicketTypesManager ttm = new TicketTypesManager();
            SpectaclesManager sm = new SpectaclesManager();
            List<Spectacle> spectacles = sm.Read();
            List<Ticket> tickets = tm.Read();
            List<TicketType> ticketTypes = ttm.Read();
            double totalSales = 0;
            string report = $"Общая продажа билетов составляет за {spectacles.Count} спектаклей составляет: ";
            foreach (TicketType ticketType in tickets.SelectMany(t => ticketTypes.Where(ticketType => t.TypeId == ticketType.TypeId)))
            {
                totalSales += ticketType.TypeCost;
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
            TicketsManager tm = new TicketsManager();
            TicketTypesManager ttm = new TicketTypesManager();
            List<Ticket> tickets = tm.Read();
            List<TicketType> ticketTypes = ttm.Read();
            string report = "";
            foreach (TicketType ticketType in ticketTypes)
            {
                double price = 0;
                double amount = 0;
                foreach (Ticket ticket in tickets.Where(ticket => ticket.TypeId == ticketType.TypeId))
                {
                    price += ticketType.TypeCost;
                    amount++;
                }
                report += $"Продано {amount} билетов типа '{ticketType.TypeName}' на сумму {price} д.е.\n";
            }
            return report;
        }
        /// <summary>
        /// Метод создания отчёта по продажам за каждый спектакль
        /// </summary>
        /// <returns>Отчёт по продажам за каждый спектакль в виде строки</returns>
        public string GetSalseReportBySpectacle()
        {
            TicketsManager tm = new TicketsManager();
            TicketTypesManager ttm = new TicketTypesManager();
            SpectaclesManager sm = new SpectaclesManager();
            List<Ticket> tickets = tm.Read();
            List<TicketType> ticketTypes = ttm.Read();
            List<Spectacle> spectacles = sm.Read();
            string report = "";
            foreach(Spectacle spectacle in spectacles)
            {
                double price = 0;
                double amount = 0;
                foreach (TicketType ticketType in tickets.SelectMany(t => ticketTypes.Where(ticketType => t.TypeId == ticketType.TypeId && t.SpectacleID == spectacle.SpectacleId)))
                {
                    price += ticketType.TypeCost;
                    amount++;
                }
                if(amount > 0)
                    report += $"Продано {amount} билета(ов) на спектакль '{spectacle.SpectacleName}' на сумму {price} д.е.\n";
            }
            return report;
        }
        */
    }   
}
