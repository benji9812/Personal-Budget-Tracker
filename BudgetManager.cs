using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perosnal_Budget_Tracker
{
    public class BudgetManager
    {
        private List<Transaction> transactions = new();

        public void AddTransaction(Transaction tx)
        {
            transactions.Add(tx);
        }

        public void ShowAll()
        {
            if (transactions.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Inga transaktioner registrerade.[/]");
                return;
            }

            var table = new Table().Border(TableBorder.Rounded)
                .AddColumn("[bold]Nr[/]")
                .AddColumn("[bold]Datum[/]")
                .AddColumn("[bold]Beskrivning[/]")
                .AddColumn("[bold]Belopp[/]")
                .AddColumn("[bold]Kategori[/]");

            int count = 1;
            foreach (var tx in transactions)
            {
                var color = tx.Amount < 0 ? "red" : "green";
                table.AddRow($"{count}", $"{tx.Date}", $"{tx.Description}", $"[{color}]{tx.Amount}[/]", $"{tx.Category}");
                count++;
            }

            AnsiConsole.Write(table);
        }

        public decimal CalculateBalance()
        {
            return transactions.Sum(t => t.Amount);
        }

        public void DeleteTransaction(int index)
        {
            // Spectre-tabbeller använder 1-baserad, men kod från input är det också nu
            if (index >= 1 && index <= transactions.Count)
            {
                transactions.RemoveAt(index - 1);
                AnsiConsole.MarkupLine("[red]Transaktion borttagen![/]");
            }
            else
                AnsiConsole.MarkupLine("[yellow]Index utanför gräns![/]");
        }

        public void ShowByCategory()
        {
            var grouped = transactions.GroupBy(t => t.Category);

            foreach (var group in grouped)
            {
                AnsiConsole.MarkupLine($"\n[bold]{group.Key}[/]");
                var table = new Table().AddColumn("Datum").AddColumn("Beskrivning").AddColumn("Belopp");
                foreach (var tx in group)
                {
                    var color = tx.Amount < 0 ? "red" : "green";
                    table.AddRow($"{tx.Date}", $"{tx.Description}", $"[{color}]{tx.Amount}[/]");
                }
                AnsiConsole.Write(table);
            }
        }

        public void DeleteByCategory(string category)
        {
            int countBefore = transactions.Count;
            transactions.RemoveAll(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            int countAfter = transactions.Count;
            int deleted = countBefore - countAfter;

            AnsiConsole.MarkupLine($"[red]{deleted} transaktion(er) i kategorin '{category}' har tagits bort.[/]");
        }

        public void FilterByCategory(string category)
        {
            var filtered = transactions.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            AnsiConsole.MarkupLine($"\nTransaktioner i kategori '[bold]{category}[/]':");

            if (filtered.Count == 0)
                AnsiConsole.MarkupLine("[yellow]Inga transaktioner hittades.[/]");
            else
            {
                var table = new Table().AddColumn("Datum").AddColumn("Beskrivning").AddColumn("Belopp");
                foreach (var tx in filtered)
                {
                    var color = tx.Amount < 0 ? "red" : "green";
                    table.AddRow($"{tx.Date}", $"{tx.Description}", $"[{color}]{tx.Amount}[/]");
                }
                AnsiConsole.Write(table);
            }
            AnsiConsole.MarkupLine($"Totalt [bold]{filtered.Count}[/] transaktion(er) i kategorin '[bold]{category}[/]'.");
        }

        public void SortByDate()
        {
            var sorted = transactions.OrderBy(t => t.Date).ToList();
            AnsiConsole.MarkupLine("\nTransaktioner sorterade efter datum:");
            var table = new Table().AddColumn("Datum").AddColumn("Beskrivning").AddColumn("Belopp").AddColumn("Kategori");
            foreach (var tx in sorted)
            {
                var color = tx.Amount < 0 ? "red" : "green";
                table.AddRow($"{tx.Date}", $"{tx.Description}", $"[{color}]{tx.Amount}[/]", $"{tx.Category}");
            }
            AnsiConsole.Write(table);
        }

        public void ShowStatistics()
        {
            int count = transactions.Count;
            decimal totalIncome = transactions.Where(t => t.Amount >= 0).Sum(t => t.Amount);
            decimal totalExpense = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);

            AnsiConsole.MarkupLine($"\n[bold]STATISTIK:[/]");
            AnsiConsole.MarkupLine($"Antal transaktioner: [bold]{count}[/]");
            AnsiConsole.MarkupLine($"Total inkomst: [green]{totalIncome}[/] kr");
            AnsiConsole.MarkupLine($"Total utgift: [red]{totalExpense}[/] kr");
            AnsiConsole.MarkupLine($"Netto: [bold]{totalIncome + totalExpense}[/] kr");
        }
    }
}

