using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perosnal_Budget_Tracker
{
    public class Transaction // Class to represent a financial transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }

        public void ShowInfo()  // Method to display transaction info with color coding
        {
            if (Amount < 0) // Red for expenses
                Console.ForegroundColor = ConsoleColor.Red;
            else // Green for income
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{Date}: {Description} | {Amount} kr | {Category}");
            Console.ResetColor();
        }
    }
}