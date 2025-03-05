using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CloudPOS.Models.ViewModels
{
    public class SaleViewModel
    {
        public DateTime Week { get; set; }
        public Decimal CashAmount { get; set; }

        // Change SaleDate to DateTime type
        public DateTime SaleDate { get; set; }
        public Dictionary<string,Decimal> DailySale { get; set; }

       public DateTime WeekStart
        {
            get
            {
                var diff = SaleDate.DayOfWeek - DayOfWeek.Monday;

                if (diff < 0) diff += 7;
                return SaleDate.AddDays(-diff).Date;
            }
        }

        public DateTime Weekend 
        {
            get
            {
                return WeekStart.AddDays(6);
            }
        }

    }
}
