using Spectre.Console;
using System;

namespace Perosnal_Budget_Tracker
{
    public class Transaction // Class to represent a financial transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }

        // Visa info om transaktion, Spectre-markup med färg
        public void ShowInfo()
        {
            var color = Amount < 0 ? "red" : "green";
            AnsiConsole.MarkupLine(
                $"[grey]{Date}[/]: [bold]{Description}[/] | [{color}]{Amount}[/] kr | [blue]{Category}[/]");
        }
    }
}
