using Perosnal_Budget_Tracker;

namespace PersonalBudgetTracker
{
    class Program
    {
        static void Main(string[] args) // Huvudmetod
        {
            // Skapa BudgetManager-instans
            BudgetManager budgetManager = new BudgetManager(); // Hanterar transaktioner
            bool running = true; // Kontrollvariabel för meny-loop

            // Meny-loop
            while (running) // Huvudmeny
            {
                Console.WriteLine("\n╔══════════════════════════╗");
                Console.WriteLine("║ PERSONAL BUDGET TRACKER ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.WriteLine("1. Lägg till transaktion");
                Console.WriteLine("2. Visa alla transaktioner");
                Console.WriteLine("3. Visa total balans");
                Console.WriteLine("4. Visa transaktioner per kategori (Bonus)");
                Console.WriteLine("5. Filtrera efter kategori");
                Console.WriteLine("6. Sortera efter datum");
                Console.WriteLine("7. Visa statistik");
                Console.WriteLine("8. Ta bort transaktion");
                Console.WriteLine("9. Ta bort transaktioner per kategori (Bonus)");
                Console.WriteLine("10. Avsluta");
                Console.Write("Välj ett alternativ (1-10): ");
                

                string choice = Console.ReadLine(); // Läs användarens val

                switch (choice) // Hantera menyval
                {
                    case "1": // Lägg till transaktion
                        AddTransaction(budgetManager);
                        break;
                    
                    case "2": // Visa alla transaktioner
                        budgetManager.ShowAll();
                        break;
                    
                    case "3": // Visa total balans
                        decimal balance = budgetManager.CalculateBalance();
                        Console.WriteLine($"\nTotal balans: {balance} kr");
                        break;
                   
                    case "4": // Visa transaktioner per kategori
                        budgetManager.ShowByCategory();
                        break;
                    
                    case "5": // Filtrera efter kategori
                        Console.Write("Ange kategori att filtrera: ");
                        string category = Console.ReadLine(); // Kategori som sträng
                        budgetManager.FilterByCategory(category); // Filtrera transaktioner
                        break;
                    
                    case "6": // Sortera efter datum
                        budgetManager.SortByDate();
                        break;
                    
                    case "7":
                        budgetManager.ShowStatistics();
                        break;

                    case "8": // Ta bort transaktion
                        DeleteTransaction(budgetManager);
                        break;
                    
                    case "9": // Ta bort transaktioner per kategori
                        budgetManager.DeleteByCategory();
                        Console.WriteLine("Alla transaktioner i vald kategori har tagits bort.");
                        break;
                    
                    case "10": // Avsluta programmet
                        running = false;
                        Console.WriteLine("Avslutar programmet. Tack!");
                        break;

                    default:
                        Console.WriteLine("✗ Ogiltigt val. Ange 1-10.");
                        break;
                }
            }
        }

        // Metod för att lägga till ny transaktion
        static void AddTransaction(BudgetManager budgetManager)
        {
            Console.Write("\nBeskrivning: ");
            string description = Console.ReadLine(); // Beskrivning som sträng

            decimal amount; // Belopp som decimal

            Console.Write("Belopp (positivt = inkomst, negativt = utgift): ");
           
            while (!decimal.TryParse(Console.ReadLine(), out amount)) // Validera inmatning
            {
                Console.Write("Ogiltigt belopp! Ange ett numeriskt värde: ");
            }


            Console.Write("Kategori: ");
            string category = Console.ReadLine(); // Kategori som sträng

            Console.Write("Datum (t.ex. 2025-10-10): ");
            string date = Console.ReadLine(); // Datum som sträng

            // Skapa och lägg till Transaction
            Transaction tx = new Transaction
            {
                Description = description,
                Amount = amount,
                Category = category,
                Date = date
            };
            budgetManager.AddTransaction(tx); // Lägg till transaktionen
        }

        // Metod för att ta bort transaktion
        static void DeleteTransaction(BudgetManager budgetManager)
        {
            budgetManager.ShowAll(); // Visa alla transaktioner först
            Console.Write("\nAnge postens nummer att ta bort: ");
            
            int index; // Index för transaktion

            if (int.TryParse(Console.ReadLine(), out index)) // Validera inmatning
            {
                budgetManager.DeleteTransaction(index); // Ta bort transaktionen
            }
            else // Ogiltig inmatning
            {
                Console.WriteLine("✗ Ogiltigt nummer.");
            }
        }
    }
}