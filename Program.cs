using Perosnal_Budget_Tracker;
using Spectre.Console;
using System;

class Program // Huvudprogram
{
    static readonly string[] categories = new string[] // Fördefinierade kategorier
    {
        "Lön",
        "Mat",
        "Transport",
        "Hyra",
        "Nöje",
        "Övrigt"
    };

    static void Main(string[] args) // Huvudmetod
    {
        BudgetManager budgetManager = new BudgetManager(); // Skapa instans av BudgetManager
        bool running = true; // Huvudloop för menyn

        while (running) // Huvudmenyloop
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new Panel("[bold yellow]PERSONAL BUDGET TRACKER[/]") { Padding = new Padding(1, 1) });

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[cyan]Välj ett alternativ:[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Lägg till transaktion",
                        "Visa alla transaktioner",
                        "Visa total balans",
                        "Visa transaktioner per kategori (Bonus)",
                        "Filtrera efter kategori",
                        "Sortera efter datum",
                        "Visa statistik",
                        "Ta bort transaktion",
                        "Ta bort transaktioner per kategori (Bonus)",
                        "Avsluta"
                    })
            );

            switch (choice) // Hantera menyval
            {
                case "Lägg till transaktion": // Lägg till ny transaktion
                    AddTransaction(budgetManager);
                    break;
                case "Visa alla transaktioner": // Visa alla transaktioner
                    budgetManager.ShowAll();
                    break;
                case "Visa total balans": // Visa total balans
                    decimal balance = budgetManager.CalculateBalance();
                    AnsiConsole.MarkupLine($"\n[bold]Total balans:[/] [green]{balance}[/] kr");
                    break;
                case "Visa transaktioner per kategori (Bonus)": // Visa per kategori
                    budgetManager.ShowByCategory();
                    break;
                case "Filtrera efter kategori": // Filtrera per kategori
                    var filterCat = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Välj kategori att filtrera")
                            .AddChoices(categories)
                    );
                    budgetManager.FilterByCategory(filterCat);
                    break;
                case "Sortera efter datum": // Sortera efter datum
                    budgetManager.SortByDate();
                    break;
                case "Visa statistik": // Visa statistik
                    budgetManager.ShowStatistics();
                    break;
                case "Ta bort transaktion": // Ta bort en transaktion
                    budgetManager.ShowAll();
                    int index = AnsiConsole.Ask<int>("Ange numret för transaktionen att ta bort:");
                    budgetManager.DeleteTransaction(index);
                    break;
                case "Ta bort transaktioner per kategori (Bonus)": // Ta bort per kategori
                    var deleteCat = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Välj kategori att ta bort:")
                            .AddChoices(categories)
                    );
                    budgetManager.DeleteByCategory(deleteCat);
                    AnsiConsole.MarkupLine("[red]Alla transaktioner i vald kategori har tagits bort.[/]");
                    break;
                case "Avsluta": // Avsluta programmet
                    running = false;
                    AnsiConsole.MarkupLine("[bold green]Avslutar programmet. Tack![/]");
                    break;
            }

            if (running) AnsiConsole.MarkupLine("\n[grey]Tryck valfri tangent för att återgå till menyn...[/]"); // Vänta på användarens input innan nästa loop
            if (running) Console.ReadKey(); // Vänta på tangenttryckning
        }
    }

    static void AddTransaction(BudgetManager budgetManager) // Lägg till transaktion
    {
        string typ = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Typ av transaktion")
                .AddChoices("Inkomst", "Utgift"));

        string description = AnsiConsole.Ask<string>("[yellow]Beskrivning:[/]");
        decimal amount = AnsiConsole.Ask<decimal>("[yellow]Belopp (positivt):[/]");

        string category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Välj kategori")
                .AddChoices(categories));

        string date = AnsiConsole.Ask<string>("Datum (t.ex. 2025-10-10):");

        if (typ == "Utgift")
            amount = -Math.Abs(amount);

        Transaction tx = new Transaction // Skapa ny transaktion
        {
            Description = description,
            Amount = amount,
            Category = category,
            Date = date
        };
        budgetManager.AddTransaction(tx);
        AnsiConsole.MarkupLine("[green]Transaktion tillagd![/]");
    }
}
